﻿using System.Text.Json.Serialization;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Composition;

namespace Geometry
{
    [Export(typeof(IShape))]
    [ExportMetadata("LogoButton", "circle.png")]
    [ExportMetadata("Name", "Circle")]
    public partial class CircleModel : ObservableObject, IShape
    {
        [ObservableProperty]
        private float _strokeThickness = 1;

        [ObservableProperty]
        private Color _stroke = Colors.Black;

        [ObservableProperty]
        private Color _fill = Colors.Black;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _centerX;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _centerY;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _radius = 10;

        [ObservableProperty]
        private float _angle = 0;
        [JsonIgnore]
        public float BoxHeight => getBoxHeight();
        [JsonIgnore]
        public float BoxWidth => getBoxWidth();
        [JsonIgnore]
        public float BoxCenterX => CenterX;
        [JsonIgnore]
        public float BoxCenterY => CenterY;
        [JsonIgnore]
        public string Geometry => getGeometry();

        public void Scale(float ratioX, float ratioY)
        {
            Radius *= ratioX >= ratioY ? ratioX : ratioY;
        }

        public void Move(float deltaX, float deltaY)
        {
            CenterX += deltaX;
            CenterY += deltaY;
        }

        public void Rotate(float angle)
        {
            Angle = (Angle + angle) % 360;
        }

        public string getGeometry()
        {
            var firstX = CenterX + Radius;
            var firstY = CenterY;
            var secondX = CenterX - Radius;
            var secondY = CenterY;

            var angleRad = Angle * Math.PI / 180.0;
            var firstPointX = (float) (CenterX + (firstX - CenterX) * Math.Cos(angleRad) - (firstY - CenterY) * Math.Sin(angleRad));
            var firstPointY = (float) (CenterY + (firstX - CenterX) * Math.Sin(angleRad) + (firstY - CenterY) * Math.Cos(angleRad));

            var secondPointX = (float) (CenterX + (secondX - CenterX) * Math.Cos(angleRad) - (secondY - CenterY) * Math.Sin(angleRad));
            var secondPointY = (float) (CenterY + (secondX - CenterX) * Math.Sin(angleRad) + (secondY - CenterY) * Math.Cos(angleRad));

            return FormattableString.Invariant($"M{firstPointX},{firstPointY} A{Radius},{Radius},{Angle},1,1,{secondPointX},{secondPointY} A{Radius},{Radius},{Angle},1,1,{firstPointX},{firstPointY} z");
        }

        private float getBoxWidth()
        {
            return (float) (2.0 * Math.Sqrt(Math.Pow(Radius * Math.Cos(Angle * Math.PI / 180.0), 2) + Math.Pow(Radius * Math.Sin(Angle * Math.PI / 180.0), 2)));
        }

        private float getBoxHeight()
        {
            return (float) (2.0 * Math.Sqrt(Math.Pow(Radius * Math.Sin(Angle * Math.PI / 180.0), 2) + Math.Pow(Radius * Math.Cos(Angle * Math.PI / 180.0), 2)));
        }
    }
}