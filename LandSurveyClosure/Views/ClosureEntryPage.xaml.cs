using System;
using System.Collections.Generic;
using LandSurveyClosure.ViewModel;
using Xamarin.Forms;

namespace LandSurveyClosure.Views
{
    public partial class ClosureEntryPage : ContentPage
    {
        public ClosureEntryPage()
        {
            InitializeComponent();

            BindingContext = new CLosureEntryViewModel(new PageService());
        }
    }
}
