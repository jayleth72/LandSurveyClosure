using System;
using System.Collections.Generic;
using LandSurveyClosure.ViewModel;
using Xamarin.Forms;

namespace LandSurveyClosure.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = new AboutPageViewModel(new PageService());
        }
    }
}
