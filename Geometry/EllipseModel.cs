using System.Text.Json.Serialization;
using System.Composition;
using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Geometry {
    [Export(typeof(IShape))]
    [ExportMetadata("LogoButton", ".png")]
    [ExportMetadata("Name", "Ellipse")]
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
        private float _radiusX = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _radiusY = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _angle = 0;

        public float BoxHeight => getBoxHeight();
        public float BoxWidth => getBoxWidth();

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

        private float getBoxWidth()
        {
            return (float) (2.0 * Math.Sqrt(Math.Pow(RadiusX * Math.Cos(Angle * Math.PI / 180.0), 2) + Math.Pow(RadiusY * Math.Sin(Angle * Math.PI / 180.0), 2)));
        }

        private float getBoxHeight()
        {
            return (float) (2.0 * Math.Sqrt(Math.Pow(RadiusX * Math.Sin(Angle * Math.PI / 180.0), 2) + Math.Pow(RadiusY * Math.Cos(Angle * Math.PI / 180.0), 2)));
        }
    }
}