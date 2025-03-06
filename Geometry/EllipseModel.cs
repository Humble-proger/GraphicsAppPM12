using System.Text.Json.Serialization;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Geometry {
    public partial class EllipseModel : ObservableObject, IShape
    {
        [ObservableProperty]
        private float _strokeThickness = 1;

        [ObservableProperty]
        private IBrush _stroke = new SolidColorBrush(Colors.Black);

        [ObservableProperty]
        private IBrush _fill = new SolidColorBrush(Colors.Black);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _centerX;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _centerY;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _radiusX = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _radiusY = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _angle = 0;

        [JsonIgnore]
        public string Geometry => GetGeometry();
        public void Scale(float ratioX, float ratioY)
        {
            RadiusX *= ratioX;
            RadiusY *= ratioY;
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

        public void SetColor(Color color)
        {
            Fill = new SolidColorBrush(color);
        }

        public void SetStroke(Color color)
        {
            Stroke = new SolidColorBrush(color);
        }

        public void SetThickness(float thickness)
        {
            StrokeThickness = thickness;
        }

        public string GetGeometry()
        {
            var firstX = CenterX + RadiusX;
            var firstY = CenterY;
            var secondX = CenterX - RadiusX;
            var secondY = CenterY;

            var angleRad = Angle * Math.PI / 180.0;
            var firstPointX = (float) (CenterX + (firstX - CenterX) * Math.Cos(angleRad) - (firstY - CenterY) * Math.Sin(angleRad));
            var firstPointY = (float) (CenterY + (firstX - CenterX) * Math.Sin(angleRad) + (firstY - CenterY) * Math.Cos(angleRad));

            var secondPointX = (float) (CenterX + (secondX - CenterX) * Math.Cos(angleRad) - (secondY - CenterY) * Math.Sin(angleRad));
            var secondPointY = (float) (CenterY + (secondX - CenterX) * Math.Sin(angleRad) + (secondY - CenterY) * Math.Cos(angleRad));

            return FormattableString.Invariant($"M{firstPointX},{firstPointY} A{RadiusX},{RadiusY},{Angle},1,1,{secondPointX},{secondPointY} A{RadiusX},{RadiusY},{Angle},1,1,{firstPointX},{firstPointY} z");
        }
    }
}