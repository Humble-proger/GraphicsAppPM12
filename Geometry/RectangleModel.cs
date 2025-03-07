using System.Text.Json.Serialization;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Geometry
{
    public partial class RectangleModel : ObservableObject, IShape
    {
        [ObservableProperty]
        private float _strokeThickness = 1;

        [ObservableProperty]
        private IBrush _stroke = new SolidColorBrush(Colors.Black);

        [ObservableProperty]
        private IBrush _fill = new SolidColorBrush(Colors.Black);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _centerX = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _centerY = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _width = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _height = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        private float _angle = 0;

        [JsonIgnore]
        private List<Avalonia.Point> ListOfPoints => GetPoints();

        [JsonIgnore]
        public string Geometry => GetGeometry();

        public void Scale(float ratioX, float ratioY)
        {
            Width *= ratioX;
            Height *= ratioY;
        }

        public void Move(float deltaX, float deltaY)
        {
            CenterX += deltaX;
            CenterY += deltaY;
        }

        public void Rotate(float angle)
        {
            Angle += angle;
            Angle %= 360;
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

        public List<Avalonia.Point> GetPoints()
        {
            List<Avalonia.Point> list = [
                new Avalonia.Point(CenterX - Width / 2, CenterY - Height / 2),
            new Avalonia.Point(CenterX + Width / 2, CenterY - Height / 2),
            new Avalonia.Point(CenterX + Width / 2, CenterY + Height / 2),
            new Avalonia.Point(CenterX - Width / 2, CenterY + Height / 2)
                ];

            var angleRad = Angle * Math.PI / 180.0;

            for (int i = 0; i < 4; i++)
            {
                list[i] = new Avalonia.Point((CenterX + (list[i].X - CenterX) * Math.Cos(angleRad) - (list[i].Y - CenterY) * Math.Sin(angleRad)),
                    (CenterY + (list[i].X - CenterX) * Math.Sin(angleRad) + (list[i].Y - CenterY) * Math.Cos(angleRad)));
            }

            return list;
        }

        private string GetGeometry()
        {
            var a = "";
            if (ListOfPoints.Count > 0)
            {
                a = FormattableString.Invariant($"M{ListOfPoints[0].X},{ListOfPoints[0].Y} ");
            }

            for (int i = 1; i < ListOfPoints.Count; i++)
            {
                a += FormattableString.Invariant($"L{ListOfPoints[i].X},{ListOfPoints[i].Y} ");
            }

            a += "z";
            return a;
        }
    }
}
