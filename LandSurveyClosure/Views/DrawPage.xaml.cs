using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace LandSurveyClosure.Views
{
    public partial class DrawPage : ContentPage
    {
        public DrawPage()
        {
            InitializeComponent();
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            // get the current surface from the event arg
            var surface = e.Surface;

            // then get the canvas that we can draw on
            var canvas = surface.Canvas;

            // clear the canvas
            canvas.Clear(SKColors.White);
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
        }
    }
}
