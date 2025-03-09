using System.Text.Json.Serialization;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Geometry
{

    public partial class BezierCurveModel : ObservableObject, IShape
    {
        [ObservableProperty]
        private float _strokeThickness = 1;

        [ObservableProperty]
        private IBrush _stroke = new SolidColorBrush(Colors.Black);

        [ObservableProperty]
        private IBrush _fill = new SolidColorBrush(Colors.Black);

        // центр, нужный для вычисления поворота и масштабирования
        private float CX;

        private float CY;

        public float CenterX => getCenterX();
        public float CenterY => getCenterY();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private List<Avalonia.Point> _listOfPoints = [];
        public BezierCurveModel(List<Avalonia.Point> initialPoints)
        {
            _listOfPoints = initialPoints;
            CalculateCenter();
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _angle = 0;

        public float BoxHeight => getBoxHeight();
        public float BoxWidth => getBoxWidth();

        [JsonIgnore]
        public string Geometry => getGeometry();

        public void Scale(float ratioX, float ratioY)
        {
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                ListOfPoints[i] = new Avalonia.Point(CX + (ListOfPoints[i].X - CX) * ratioX, CY + (ListOfPoints[i].Y - CY) * ratioY);
            }
        }

        public void Move(float deltaX, float deltaY)
        {
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                ListOfPoints[i] = new Avalonia.Point(ListOfPoints[i].X + deltaX, ListOfPoints[i].Y + deltaY);
            }
            CalculateCenter();
        }

        public void Rotate(float angle)
        {
            var angleRad = angle * Math.PI / 180.0;
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                ListOfPoints[i] = new Avalonia.Point((float) (CX + (ListOfPoints[i].X - CX) * Math.Cos(angleRad) - (ListOfPoints[i].Y - CY) * Math.Sin(angleRad)),
                    (float) (CY + (ListOfPoints[i].X - CX) * Math.Sin(angleRad) + (ListOfPoints[i].Y - CY) * Math.Cos(angleRad)));
            }

            Angle += angle;
            Angle %= 360;
        }

        public string getGeometry()
        {
            var a = "";
            if (ListOfPoints.Count > 0)
            {
                a = FormattableString.Invariant($"M{ListOfPoints[0].X},{ListOfPoints[0].Y} ");
            }

            for (int i = 1; i < ListOfPoints.Count - 1; i += 2)
            {
                a += FormattableString.Invariant($"Q {ListOfPoints[i].X},{ListOfPoints[i].Y} {ListOfPoints[i + 1].X},{ListOfPoints[i + 1].Y}");
            }

            return a;
        }

        public void CalculateCenter()
        {
            var avgX = 0.0;
            var avgY = 0.0;

            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                avgX += ListOfPoints[i].X;
                avgY += ListOfPoints[i].Y;
            }

            if (ListOfPoints.Count > 0)
            {
                avgX /= ListOfPoints.Count;
                avgY /= ListOfPoints.Count;
            }

            CX = (float) avgX;
            CY = (float) avgY;
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

        private float getCenterX()
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

        private float getCenterY()
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
    }
}
