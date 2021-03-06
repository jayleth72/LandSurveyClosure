﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LandSurveyClosure.Model;
using System;
using Xamarin.Forms;

namespace LandSurveyClosure.ViewModel
{
    public class CLosureEntryViewModel : BaseViewModel
    {
        #region Private Variables        
        private int _selectedUnitIndex;         // Selected Index for the Unit Picker, default will be metres.
        private string _degreesInput;
        private string _minuteInput;
        private string _secondInput;
        private string _distanceInput;
        private double _distanceDoubleInput;    // Distance input converted to a double.  This is tested in the IsDataInputOk() method.
        private int _degreeIntInput;            // Degree input converted to a int.  This is tested in the IsDataInputOk() method.
        private int _minuteIntInput;            // Minute input converted to a int.  This is tested in the IsDataInputOk() method.
        private int _secondIntInput;            // Second input converted to a int.  This is tested in the IsDataInputOk() method
		private ObservableCollection<ClosureLine> _dataList = new ObservableCollection<ClosureLine>();      // Stores Bearing and Distance Data input for display in view list
        private ObservableCollection<Coordinate> _coordinatesList = new ObservableCollection<Coordinate>(); // Stores Northing and Eastings from  bearing and distance calculations 
        #endregion


        #region Bound Properties
        /// Gets or sets the index of the unit picker.
        /// </summary>
        /// <value>The index of the unit picker.</value>
        public int DistanceUnitsSelectedIndex
        {
            get { return _selectedUnitIndex; }
            set { SetValue(ref _selectedUnitIndex, value); }
        }

        public string DistanceInput
        {
            get { return _distanceInput; }
            set { SetValue(ref _distanceInput, value); }
        }

        public string DegreesInput
        {
            get { return _degreesInput; }
            set { SetValue(ref _degreesInput, value); }
        }

        public string MinutesInput
        {
            get { return _minuteInput; }
            set { SetValue(ref _minuteInput, value); }
        }

        public string SecondsInput
        {
            get { return _secondInput; }
            set { SetValue(ref _secondInput, value); }
        }

		
		public ObservableCollection<ClosureLine> DataList
		{
			get
			{
				return _dataList;
			}
			set
			{
				_dataList = value;
				
				OnPropertyChanged("DataList");
			}
		}
        #endregion


        #region View Commands for Buttons
        public ICommand AddDistanceBearingCommand { get; private set; }
        public ICommand CalculateClosureCommand { get; private set; }
        public ICommand ClearAllCommand { get; private set; }
        public ICommand DrawCommand { get; private set; }
        public ICommand DeleteClosureLineCommand { get; private set; }
        #endregion


   
        public CLosureEntryViewModel(IPageService pageService) : base(pageService)
        {
            AddDistanceBearingCommand = new Command(AddDistanceBearingToStack);
            CalculateClosureCommand = new Command(CalculateClosure);
			ClearAllCommand = new Command(ClearAll);
			DrawCommand = new Command(async () => await GoToDrawPage());
            DeleteClosureLineCommand = new Command(DeleteLine);
            _selectedUnitIndex = 0;     // Set default unit to metres

            // Add starting coordinate (0, 0) to Coordinates list
            AddStartingCoordinate(0.0, 0.0);    
        }
       

        #region Class Methods
        /// <summary>
        /// Adds the distance bearing to stack for display in the list.
        /// Converts Bearing and Distance to Northing and Easting and stores in a list
        /// </summary>
        private void AddDistanceBearingToStack()
        {
            // Check that Data is present and in correct format, otherwise show error type message
            if (IsDataInputOk())
            {
                // Add Closure Lines to list
                var closureLine = new ClosureLine
                {
                    Distance = _distanceDoubleInput,
                    Degrees = _degreeIntInput,
                    Minutes = _minuteIntInput,
                    Seconds = _secondIntInput,
                    DistanceBearing = ConvertDistanceBearingOutput()
				};

				_dataList.Add(closureLine);

                AddCoordinateToList();

                ClearInput();
            }    
           
        }

		private void AddStartingCoordinate(double northing, double easting)
		{
			var startingCoordinate = new Coordinate
			{
				Northing = northing,
				Easting = easting
			};

			_coordinatesList.Add(startingCoordinate);
		}

        /// <summary>
        /// Converts Bearing and Distance to Northing and Easting Coords.
        /// Adds the Coordinate to _coordinatesList.
        /// </summary>
        private void AddCoordinateToList()
        {
            // Check to see if Coordinate List is empty
            if(_coordinatesList.Count == 0)
            {
				// Add starting coordinate (0, 0) to Coordinates list
				AddStartingCoordinate(0.0, 0.0);
			}   

            // Get Previous Northing and Easting and add new Northings and Eastings to get next Coordinates
            Coordinate previous = _coordinatesList.Last();

			// Add Coordinate to list
			var coordinate = new Coordinate
			{
                Northing = previous.Northing + _distanceDoubleInput * Math.Cos(ConvertToRadians()),
                Easting = previous.Easting + _distanceDoubleInput * Math.Sin(ConvertToRadians())
			};

			_coordinatesList.Add(coordinate);
        }

		private double ConvertToRadians()
		{
			// Convert Input to decimal degrees
			double decimalDegrees = _degreeIntInput + (_minuteIntInput / 60) + (_secondIntInput / 3600);

            return Math.PI * (decimalDegrees / 180.0);
		}

        private double RadianToDegree(double bearing)
        {
            return bearing * (180 / System.Math.PI);    
        }

		/// <summary>
		/// Converts the decimal degrees to degrees, minutes and second.
		/// </summary>
		/// <returns>Degrees, minutes & seconds as a string.</returns>
		/// <param name="input">Double User Input</param>
		private string ConvertDecimalToDegMinSec(double input)
		{
			// Get the whole degrees value from the decimal value
			int degrees = (int)input;

			// Get the whole minutes value from the decimal value
			double calcMinutesValue = ((input - (double)degrees) * 60);
			int minutes = (int)calcMinutesValue;

			// Calculate seconds
			double theSeconds = ((calcMinutesValue - (double)minutes) * 60);
			theSeconds = Math.Round(theSeconds, 1, MidpointRounding.AwayFromZero);
			//Round Seconds to one decimal plae

			return degrees.ToString() + "\u00B0 " + minutes.ToString() + "\' " + theSeconds.ToString() + "\"";
		}

        private async void CalculateClosure()
        {
            // Get Last Coordinate and calculate bearing and distance from starting coordinate (0,0)
            Coordinate last = _coordinatesList.Last();
            double closureBearing = Math.Atan(last.Easting/last.Northing);
            double closureDistance = Math.Sqrt((double)((last.Easting * last.Easting) + (last.Northing * last.Northing)));


            // Convert closureBearing to Degrees
            closureBearing = RadianToDegree(closureBearing);

            // Convert Decimal Degrees to Degrees Minutes and Seconds
            string displayClosureBearing = ConvertDecimalToDegMinSec(closureBearing);
                
			// check that at least 2 closure lines/coordinates exist before calculating closure
			if (CheckOkToCalculateClosure())
            {
				// Check bearing and distance from 0, 0 to last point

				//string message = "Closure Distance= " + closureDistance + ", Bearing = " + displayClosureBearing;

				string message = "Noprthing= " + last.Northing + ", Easting = " + last.Easting;
				await _pageService.DisplayAlert("Calculations", message, "Ok");

            }
            else
                await _pageService.DisplayAlert("Data Input Error", "Not Enough Data to Calculate Closure.", "Ok");
        }


		/// <summary>
		/// // check that at least 2 closure lines, or 3 coordinates exist before calculating closure
		/// </summary>
		/// <returns><c>true</c>, if ok to calculate closure was checked, <c>false</c> otherwise.</returns>
		private bool CheckOkToCalculateClosure()
        {
            if (_coordinatesList.Count > 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Clear all input fields and list display as well as coordinates list.
        /// </summary>
        private void ClearAll()
        {
            ClearInput();
            _dataList.Clear();
            _coordinatesList.Clear();

        }

		/// <summary>
		/// Deletes the line from list when user swipes to right and presses "Delete Line" in the List.
		/// </summary>
		/// <param name="sender">Sender.</param>
		private void DeleteLine(object sender)
		{
			var closureLine = sender as ClosureLine;
			_dataList.Remove(closureLine);
		}

        private async Task GoToDrawPage()
        {
            await _pageService.PushAsync(new Views.DrawPage(_coordinatesList));
        }


        /// <summary>
        /// Takes Distance, Distance Unitsm, Degrees, Minutes and Seconds and converts to a string for display in output list.
        /// </summary>
        /// <returns>The distance bearing output.</returns>
        private string ConvertDistanceBearingOutput()
        {
            string units;
            // Get selected Unit from Unit Picker
            switch (_selectedUnitIndex)
            {
                case 0:
                    units = "metres";
                    break;
				case 1:
					units = "links";
					break;
				case 2:
					units = "feet";
					break;
                default:
                    // make metres the default
                    units = "metres";
                    break;
            }

            return  _distanceDoubleInput + " " + units + ", " + _degreeIntInput + "\u00B0 " + _minuteIntInput + "\' " + _secondIntInput + "\"";
        }

		/// <summary>
		/// Is the data input ok.
		/// Need to check for no data entered in all fields.
		/// Need to check for data entry in distance field.
		/// Need to check for data entry in at least one of bearing fields.
		/// Check format is correct - numerical format and in specified ranges for bearing data
		/// </summary>
		/// <returns><c>true</c>, if data input ok was ised, <c>false</c> otherwise.</returns>
		private bool IsDataInputOk()
        {
            INPUT_VALIDATION_FLAG errorTypeFlag = INPUT_VALIDATION_FLAG.INPUT_OK;
            bool dataOk = true;

            if (NoDataEntered(_distanceInput) && NoDataEnteredAngle(_degreesInput, _minuteInput, _secondInput))
            {
                // No Data entered in both distance and bearing fields
                errorTypeFlag = INPUT_VALIDATION_FLAG.NO_INPUT_ENTERED;
                dataOk = false;
            }
            else if (NoDataEntered(_distanceInput))
            {
                // No Data entered in distance field
                errorTypeFlag = INPUT_VALIDATION_FLAG.NO_DISTANCE_INPUT_ENTERED;
                dataOk = false;
            }
            else if (NonNumericalDoubleDataEntered(_distanceInput, ref _distanceDoubleInput))
            {
                // String Data/Non-Numerical data entered.
                // Distance input is tested here and _distanceDoubleInput is initialised if correct double input.
                errorTypeFlag = INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;
                dataOk = false;
            }
            else if (NoDataEnteredAngle(_degreesInput, _minuteInput, _secondInput))
            {
                // No Bearing Data entered
                errorTypeFlag = INPUT_VALIDATION_FLAG.NO_BEARING_DATA_ENTERED;
                dataOk = false;
            }
            else
            {

                if (!NoDataEntered(_degreesInput))
                {
                    if (NonNumericalDataEntered(_degreesInput, ref _degreeIntInput))
                    {
                        // Incorrect data entered in Degrees field.
                        errorTypeFlag = INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;
                        dataOk = false;
                    }
                    else if (NumberOutOfRange(360, 0, _degreeIntInput))
                    {
                        // Degrees out of range error.
                        errorTypeFlag = INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_DEGREES;
                        dataOk = false;
                    }

                }

                if (!NoDataEntered(_minuteInput))
                {
                    if (NonNumericalDataEntered(_minuteInput, ref _minuteIntInput))
                    {
                        // Incorrect data entered in Minutes field.
                        errorTypeFlag = INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;
                        dataOk = false;
                    }
                    else if (NumberOutOfRange(60, 0, _minuteIntInput))
                    {
                        // Degrees out of range error.
                        errorTypeFlag = INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_MINUTES;
                        dataOk = false;
                    }

                }

                if (!NoDataEntered(_secondInput))
                {
                    if (NonNumericalDataEntered(_secondInput, ref _secondIntInput))
                    {
                        // Incorrect data entered in Secondss field.
                        errorTypeFlag = INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED;
                        dataOk = false;
                    }
                    else if (NumberOutOfRange(60, 0, _secondIntInput))
                    {
                        // Degrees out of range error.
                        errorTypeFlag = INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_SECONDS;
                        dataOk = false;
                    }

                }
            }

            // Display error message if data input error found
            if (!dataOk)
                DisplayErrorMessage(errorTypeFlag);
            
            return dataOk;
        }

        /// <summary>
        /// Displays the error message according to Error type.
        /// </summary>
        /// <param name="errorType">Error type.</param>
        private async void DisplayErrorMessage(INPUT_VALIDATION_FLAG errorType)
        {
            switch (errorType)
            {
                case INPUT_VALIDATION_FLAG.NO_INPUT_ENTERED:
                    await _pageService.DisplayAlert("No Data Entered", "Please enter some data", "Ok");
                    break;
				case INPUT_VALIDATION_FLAG.NO_DISTANCE_INPUT_ENTERED:
					await _pageService.DisplayAlert("No Distance Data Entered", "Please enter some data in the Distance field", "Ok");
					break;
				case INPUT_VALIDATION_FLAG.NO_BEARING_DATA_ENTERED:
					await _pageService.DisplayAlert("No Bearing Data Entered", "Please enter some data in the Degrees, Minutes or Seconds field", "Ok");
					break;
				case INPUT_VALIDATION_FLAG.NON_NUMERICAL_DATA_ENTERED:
					await _pageService.DisplayAlert("Data Entry Error", "Non-numerical data entered, please enter numerical data only", "Ok");
					break;
				case INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_DEGREES:
					await _pageService.DisplayAlert("Out of Range Error", "Please enter a value between 0 and 359 in the degrees field", "Ok");
					break;
				case INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_MINUTES:
					await _pageService.DisplayAlert("Out of Range Error", "Please enter a value between 0 and 59 in the minutes field", "Ok");
					break;
				case INPUT_VALIDATION_FLAG.NUMBER_OUT_OF_RANGE_SECONDS:
					await _pageService.DisplayAlert("Out of Range Error", "Please enter a value between 0 and 59 in the seconds field", "Ok");
					break;
                default:
                    break;
            }
        }


		/// <summary>
		/// Check for no data entry on an angle.
		/// Returns true if no data entered for degrees, minutes or seconds fields.
		/// </summary>
		/// <returns><c>true</c>, if no data entered for an angle, <c>false</c> otherwise.</returns>
		/// <param name="input1">Input1.</param>
		/// <param name="input2">Input2.</param>
		/// <param name="input3">Input3.</param>
		private bool NoDataEnteredAngle(string input1, string input2, string input3)
		{
			if (NoDataEntered(input1) && NoDataEntered(input2) && NoDataEntered(input3))
				return true;
			else
				return false;
		}

        private void ClearInput()
        {
            _distanceInput = "";
            _degreesInput = "";
            _minuteInput = "";
            _secondInput = "";

            _distanceDoubleInput = 0.0;
            _degreeIntInput = 0;
            _secondIntInput = 0;
            _minuteIntInput = 0;

            OnPropertyChanged(DistanceInput);
            OnPropertyChanged(DegreesInput);
			OnPropertyChanged(SecondsInput);
			OnPropertyChanged(MinutesInput);
        }

       #endregion
    }
     

}
