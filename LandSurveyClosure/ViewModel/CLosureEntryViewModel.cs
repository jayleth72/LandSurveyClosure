using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandSurveyClosure.ViewModel
{
    public class CLosureEntryViewModel : BaseViewModel
    {
        #region Private Variables        
        private int _selectedUnitIndex;     // Selected Index for the Unit Picker, default will be metres
        private int _degreesInput;
        private int _minuteInput;
        private int _secondInput;
        private double _distanceInput;
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

        public double DistanceInput
        {
            get { return _distanceInput; }
            set { SetValue(ref _distanceInput, value); }
        }

        public int DegreesInput
        {
            get { return _degreesInput; }
            set { SetValue(ref _degreesInput, value); }
        }

        public int MinuteInput
        {
            get { return _minuteInput; }
            set { SetValue(ref _minuteInput, value); }
        }

        public int SecondsInput
        {
            get { return _secondInput; }
            set { SetValue(ref _secondInput, value); }
        }
        #endregion


        #region View Commands for Buttons
        public ICommand AddDistanceBearingCommand { get; private set; }
        #endregion


        #region Constructors
        public CLosureEntryViewModel(IPageService pageService) : base(pageService)
        {
            AddDistanceBearingCommand = new Command(AddDistanceBearingToStack);
            _selectedUnitIndex = 0;     // Set default unit to metres
        }
        #endregion

        #region Class Methods
        /// <summary>
        /// Adds the distance bearing to stack for display in the list.
        /// </summary>
        private void AddDistanceBearingToStack()
        {
			// Add Closure Lines to list
        }
        #endregion
    }
     

}
