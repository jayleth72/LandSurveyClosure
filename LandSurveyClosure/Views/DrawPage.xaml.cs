using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.ObjectModel;
using LandSurveyClosure.Model;

namespace LandSurveyClosure.Views
{
    public partial class DrawPage : ContentPage
    {
        SKPaint blackFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black
        };

        SKPaint blackStrokePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = 1,
            StrokeCap = SKStrokeCap.Round,
            IsAntialias = true
        };

        SKPath polygonPath = new SKPath();

        private ObservableCollection<Coordinate> _coordinateList;       // Holds coordinates used to draw polygon

        public DrawPage(ObservableCollection<Coordinate> coordinateList)
        {
            InitializeComponent();
            _coordinateList = coordinateList;

            // Set coordinates for polygon path
            polygonPath.MoveTo(0, 0);
            foreach(var coordinates in coordinateList)
            {
                polygonPath.LineTo((float)coordinates.Easting, (float)coordinates.Northing);
            } 

            //polygonPath.Close();
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            // Purpose of this method is to re-paint canvas
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.White);

            // width and height of screen
            int width = e.Info.Width;
            int heigtht = e.Info.Height;

            // Set transforms to set startpoint in middle of screen
            canvas.Translate(width/2, width/2);
            canvas.Scale(width / 100f);
                      
            // Draw polygon path
            canvas.Save();
            canvas.DrawPath(polygonPath, blackStrokePaint);
			canvas.Restore();
        }


    }
}
