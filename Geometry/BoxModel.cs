using System.Text.Json.Serialization;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;


namespace Geometry
{
    public partial class BoxModel : ObservableObject, IShape
    {
        [ObservableProperty]
        private float _strokeThickness = 1;

        [ObservableProperty]
        private Avalonia.Collections.AvaloniaList<double> _strokeDashArray = [5, 2];

        [ObservableProperty]
        private Color _stroke = Colors.Gray;
        [ObservableProperty]
        private Color _fill = Colors.Transparent;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _centerX = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _centerY = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _width = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _height = 10;

        [ObservableProperty]
        private float _angle = 0;
        [ObservableProperty]
        public float _boxHeight = 0;
        [ObservableProperty]
        public float _boxWidth = 0;
        [ObservableProperty]
        public float _boxCenterX = 0;
        [ObservableProperty]
        public float _boxCenterY = 0;

        [JsonIgnore]
        public string Geometry => FormattableString.Invariant($"M{CenterX - Width / 2.0},{CenterY - Height / 2.0} h{Width} v{Height} h{-Width} z");

        public void Move(float deltaX, float deltaY)
        {
            CenterX += deltaX;
            CenterY += deltaY;
        }
        public void Scale(float ratioX, float ratioY) 
        {
            Width *= ratioX;
            Height *= ratioY;
        }
        public void Rotate(float angle)
        {
            Angle += angle;
            Angle %= 360;
        }
    }

}
