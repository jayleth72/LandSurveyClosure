using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LandSurveyClosure.ViewModel
{
    public class CLosureEntryViewModel : BaseViewModel
    {
		// View Button commands
		public ICommand GoToMainMenuCommand { get; private set; }

        public CLosureEntryViewModel(IPageService pageService) : base(pageService)
        {
           
		}
    }
}
