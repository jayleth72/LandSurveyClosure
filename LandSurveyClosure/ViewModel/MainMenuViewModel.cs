using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.ViewModel
{
    public class MainMenuViewModel : BaseViewModel
    {
		// View Button commands
		public ICommand GoToConversionsMenuPageCommand { get; private set; }
		public ICommand GoToAngleAddSubtractPageCommand { get; private set; }
		public ICommand GoToSettingsPageCommand { get; private set; }
		public ICommand GoToAboutPageCommand { get; private set; }

        public MainMenuViewModel(IPageService pageService) : base(pageService)
        {
			GoToConversionsMenuPageCommand = new Command(async () => await GoToConversionsMenuPage());       // Navigation for Conversions Page
			GoToAngleAddSubtractPageCommand = new Command(async () => await GoToAngleAddSubtractPage());      // Navigation for AngleAddSubtractPage 
			GoToSettingsPageCommand = new Command(async () => await GoToSettingsPage());       // Navigation for Conversions Page
			GoToAboutPageCommand = new Command(async () => await GoToAboutPage());      // Navigation for AngleAddSubtractPage 
		}

		private async Task GoToConversionsMenuPage()
		{
			await _pageService.PushAsync(new MenuPages.ConversionsMenuPage());
		}

		private async Task GoToAngleAddSubtractPage()
		{
			await _pageService.PushAsync(new ContentPages.AngleAddSubtract2());
		}

		private async Task GoToAboutPage()
		{
			await _pageService.PushAsync(new ContentPages.AboutPage());
		}

		private async Task GoToSettingsPage()
		{
            await _pageService.PushAsync(new MenuPages.SettingsMenuPage());
		}
	}
}
