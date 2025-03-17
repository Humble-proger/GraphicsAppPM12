using System.Composition;
using System.Text.Json.Serialization;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Geometry
{
    [Export(typeof(IShape))]
    [ExportMetadata("LogoButton", "triangle.png")]
    [ExportMetadata("Name", "Triangle")]
    public partial class TriangleModel : ObservableObject, IShape
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _strokeThickness = 1;

        [ObservableProperty]
        private Color _stroke = Colors.Black;

        [ObservableProperty]
        private Color _fill = Colors.Black;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _centerX = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _centerY = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _width = 20;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _height = 20;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _angle = 0;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _scaleX = 1;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        [NotifyPropertyChangedFor(nameof(BoxCenterX))]
        [NotifyPropertyChangedFor(nameof(BoxCenterY))]
        private float _scaleY = 1;

        public float BoxHeight => getBoxHeight() + StrokeThickness + 6;
        public float BoxWidth => getBoxWidth() + StrokeThickness + 6;

        public float BoxCenterX => getCenterBoxX() - BoxWidth / 2;
        public float BoxCenterY => getCenterBoxY() - BoxHeight / 2;

        [JsonIgnore]
        private List<Avalonia.Point> ListOfPoints => getPoints();

        [JsonIgnore]
        public string Geometry => getGeometry();

        public void Scale(float ratioX, float ratioY)
        {
            ScaleX *= ratioX;
            ScaleY *= ratioY;
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

        public List<Avalonia.Point> getPoints()
        {
            List<Avalonia.Point> list = [
                new Avalonia.Point(CenterX - Width / 2, CenterY + Height / 2),
            new Avalonia.Point(CenterX, CenterY - Height / 2),
            new Avalonia.Point(CenterX + Width / 2, CenterY + Height / 2),
            ];

            var angleRad = Angle * Math.PI / 180.0;

            for (int i = 0; i < 3; i++)
            {
                list[i] = new Avalonia.Point((CenterX + ScaleX * ((list[i].X - CenterX) * Math.Cos(angleRad) - (list[i].Y - CenterY) * Math.Sin(angleRad))),
                    (CenterY + ScaleY * ((list[i].X - CenterX) * Math.Sin(angleRad) + (list[i].Y - CenterY) * Math.Cos(angleRad))));
            }

            return list;
        }

        private string getGeometry()
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

        public float getCenterBoxX()
        {

            float maxCoord = float.MinValue;
            float minCoord = float.MaxValue;
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                if (ListOfPoints[i].X > maxCoord)
                {
                    maxCoord = (float) ListOfPoints[i].X;
                }

                if (ListOfPoints[i].X < minCoord)
                {
                    minCoord = (float) ListOfPoints[i].X;
                }
            }
            return (float) (minCoord + (maxCoord - minCoord) / 2.0);
        }
        public float getCenterBoxY()
        {

            float maxCoord = float.MinValue;
            float minCoord = float.MaxValue;
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                if (ListOfPoints[i].Y > maxCoord)
                {
                    maxCoord = (float) ListOfPoints[i].Y;
                }

                if (ListOfPoints[i].Y < minCoord)
                {
                    minCoord = (float) ListOfPoints[i].Y;
                }
            }
            return (float) (minCoord + (maxCoord - minCoord) / 2.0);
        }
        private float getBoxWidth()
        {
            float maxCoord = float.MinValue;
            float minCoord = float.MaxValue;
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                if (ListOfPoints[i].X > maxCoord)
                {
                    maxCoord = (float) ListOfPoints[i].X;
                }

                if (ListOfPoints[i].X < minCoord)
                {
                    minCoord = (float) ListOfPoints[i].X;
                }
            }
            return maxCoord - minCoord;
        }

        private float getBoxHeight()
        {
            float maxCoord = float.MinValue;
            float minCoord = float.MaxValue;
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                if (ListOfPoints[i].Y > maxCoord)
                {
                    maxCoord = (float) ListOfPoints[i].Y;
                }

                if (ListOfPoints[i].Y < minCoord)
                {
                    minCoord = (float) ListOfPoints[i].Y;
                }
            }
            return maxCoord - minCoord;
        }
    }
}