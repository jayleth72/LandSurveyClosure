using System;
using System.Collections.Generic;
using JayCadSurveyXamarin.ViewModel;
using Xamarin.Forms;

namespace JayCadSurveyXamarin.MenuPages
{
    public partial class MainMenuPage : ContentPage
    {
		public MainMenuPage()
		{
			InitializeComponent();

            BindingContext = new MainMenuViewModel(new PageService());
		}
               
    }
}
