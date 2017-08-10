using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LandSurveyClosure.ViewModel
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task PopToRootAsync();
        Task PopAsync();
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task DisplayAlert (string title, string message, string ok);
    }
}
