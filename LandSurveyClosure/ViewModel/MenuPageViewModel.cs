using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandSurveyClosure.ViewModel
{
    public class MenuPageViewModel : BaseViewModel
    {
		// View Button commands
		public ICommand GoToClosureEntryPageCommand { get; private set; }
	    public ICommand GoToAboutPageCommand { get; private set; }


        public MenuPageViewModel(IPageService pageService) : base(pageService)
        {
			GoToClosureEntryPageCommand = new Command(async () => await GoToClosureEntryPage());       // Navigation for Conversions Page
			GoToAboutPageCommand = new Command(async () => await GoToAboutPage());      // Navigation for AngleAddSubtractPage 
		}

		private async Task GoToClosureEntryPage()
		{
			await _pageService.PushAsync(new Views.ClosureEntryPage());
		}

		private async Task GoToAboutPage()
		{
			await _pageService.PushAsync(new Views.AboutPage());
		}

	}
}
