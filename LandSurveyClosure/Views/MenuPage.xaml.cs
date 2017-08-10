﻿using System;
using System.Collections.Generic;
using LandSurveyClosure.ViewModel;
using Xamarin.Forms;

namespace LandSurveyClosure.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            BindingContext = new MenuPageViewModel(new PageService());
        }
    }
}
