using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
	public class AboutPageViewModel : BaseViewModel
	{
        private string _aboutText;      // Text to describe JayCad
        private string _lengthConversionHeading;
        private string _lengthConversionText;
		private string _areaConversionHeading;
		private string _areaConversionText;
		private string _decimalAngleConversionHeading;
		private string _decimalAngleConversionConversionText;
		private string _degMinsSecondsConversionHeading;
        private string _degMinsSecondsConversionText;
        private string _angleAddSubtractHeading;
        private string _angleAddSubtractText;
        private string _settingsHeading;
        private string _settingsText;
        private string _legalText;
        private string _legalTextHeading;
        private string _copyright;
		/// <summary>
		/// Gets or sets minutes2 field.
		/// </summary>
		/// <value>minutes2 input.</value>
		public string AboutText
		{
			get { return _aboutText; }
			set { SetValue(ref _aboutText, value); }
		}

		public string LengthConversion
		{
			get { return _lengthConversionHeading; }
			set { SetValue(ref _lengthConversionHeading, value); }
		}

		public string LengthConversionText
		{
			get { return _lengthConversionText; }
			set { SetValue(ref _lengthConversionText, value); }
		}

		public string AreaConversion
		{
			get { return _areaConversionHeading; }
			set { SetValue(ref _areaConversionHeading, value); }
		}

		public string AreaConversionText
		{
			get { return _areaConversionText; }
			set { SetValue(ref _areaConversionText, value); }
		}

		public string DecimalAngleConversion
		{
			get { return _decimalAngleConversionHeading; }
			set { SetValue(ref _decimalAngleConversionHeading, value); }
		}

		public string DecimalAngleConversionText
		{
			get { return _decimalAngleConversionConversionText; }
			set { SetValue(ref _decimalAngleConversionConversionText, value); }
		}

		public string DegMinSecondsConversion
		{
			get { return _degMinsSecondsConversionHeading; }
			set { SetValue(ref _degMinsSecondsConversionHeading, value); }
		}

		public string DegMinSecondsConversionText
		{
			get { return _degMinsSecondsConversionText; }
			set { SetValue(ref _degMinsSecondsConversionText, value); }
		}

        public string AngleAddSubtract
		{
			get { return _angleAddSubtractHeading; }
			set { SetValue(ref _angleAddSubtractHeading, value); }
		}

		public string AngleAddSubtractText
		{
			get { return _angleAddSubtractText; }
			set { SetValue(ref _angleAddSubtractText, value); }
		}

		public string SettingsConversionRounding
		{
			get { return _settingsHeading; }
			set { SetValue(ref _settingsHeading, value); }
		}

		public string SettingsText
		{
			get { return _settingsText; }
			set { SetValue(ref _settingsText, value); }
		}

		public string LegalText
		{
            get { return _legalText; }
			set { SetValue(ref _legalText, value); }
		}

		public string LegalTextHeading
		{
			get { return _legalTextHeading; }
			set { SetValue(ref _legalTextHeading, value); }
		}

		public string Copyright
		{
			get { return _copyright; }
			set { SetValue(ref _copyright, value); }
		}

		public AboutPageViewModel(IPageService pageService) : base(pageService)
		{
            SetTextStrings();
		}

        private void SetTextStrings(){
            _aboutText = "Jaycad is an application designed to be used by Land Surveyors. It contains the following functions.";

            // Initialise Headings
            _lengthConversionHeading = "Length Conversion";
			_areaConversionHeading = "Area Conversion";
            _decimalAngleConversionHeading = "Decimal Bearing Conversion";
            _degMinsSecondsConversionHeading = "Deg/Min/Sec Conversion";
            _angleAddSubtractHeading = "Angle Addition and Subtraction";
            _settingsHeading = "Settings";
			_legalTextHeading = "Disclaimer";
            _copyright = "© 2017 JayLeth Technologie";

            // Initialise length conversion Text
            _lengthConversionText = "Contains Feet to Metres, Metres to Feet, Links to Metres and Links to Metres conversions." +
                "  Inches and Fraction Inches can be specified when converting from Feet to Metres." +
				"  Converting Metres to Feet will convert and display decimal feet." +
                "  Conversion results can be displayed up to 5 decimal figures.  The number of decimal places can be specifed in the Settings/Roundings page." +
                "  Each Conversion is saved to a Stack Page which can be accessed via the Show Stack Button.  A Conversion can be removed from the Stack by swiping left.";

			// Initialise area conversion Text
			_areaConversionText = "Contains Acres to Hectares and Hectares to Acres conversions." +
				"  Roods and Perches can be specified when converting from Acres to Hectares." +
                "  Converting Hectare to Acres will convert and display decimal Acres." +
				"  Conversion results can be displayed up to 5 decimal figures.  The number of decimal places can be specifed in the Settings/Roundings page." +
				"  Each Conversion is saved to a Stack Page which can be accessed via the Show Stack Button.  A Conversion can be removed from the Stack by swiping left.";

            // Initialise Decimal Bearing conversion Text
            _decimalAngleConversionConversionText = "Users can specify a bearing in Degrees (0-359), Minutes (0-59) and Seconds (0-59)." +
                "  Conversion results can be displayed up to 5 decimal figures.  The number of decimal places can be specifed in the Settings/Roundings page.";

			// Initialise Deg/Min/Sed Bearing conversion Text
			_degMinsSecondsConversionText = "Users can specify a bearing in Decimal Format." +
				"  When completing a conversion, Seconds are displayed up to 1 decimal figure.";

            // Initialise Angle Addition and Subtraction Text
            _angleAddSubtractText = "Users can specify an Angle in Degrees (0-359), Minutes (0-59) and Seconds (0-59) for Angular addition and Subtraction.";

			// Initialise Settings Text
			_settingsText = "Users can specify the accuracy of their Conversion results, by selecting the number of decimal places, their results will be displayed in." + 
                "  This is done via the Roundings menu option from the Settings Menu.  Roundings can be specified for Length, Area and Decimal Bearing conversions." +
                "  Accuracy of a conversion can be shown up to 5 decimal places by selecting the corresponding rounding in the Roundings Page." + 
                "  To avoid clutterd displays, trailing zeros are removed from conversion calculations.  For example if a rounding of 5 decimal figure is chosen" +
				" the number 34.85000 will be displayed as 34.85."  +
				"  Default roundings are set to zero when the application is loaded on to your device";

            _legalText = "The information contained in this mobile app (\"JAYCAD\") is for general information purposes only." +
                "  JayLeth Technologies assumes no responsibility for errors or omissions in the contents on this mobile application." +
                "  In no event shall JayLeth Technologies be liable for any special, direct, indirect, consequential, or" +
                " incidental damages or any damages whatsoever, whether in an action of contract, negligence or" +
                " other tort, arising out of or in connection with the use of the Mobile Application, \"JAYCAD\" or the contents of the" +
                " mobile application. JayLeth Technologies reserves the right to make additions, deletions, or modification to the" +
                " contents on the mobile application at any time without prior notice.";
            
			OnPropertyChanged(AboutText);
        }

	}
}
