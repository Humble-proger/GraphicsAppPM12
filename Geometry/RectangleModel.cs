﻿using System.Text.Json.Serialization;
using System.Composition;
using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Geometry
{
    [Export(typeof(IShape))]
    [ExportMetadata("LogoButton", "triangle.png")]
    [ExportMetadata("Name", "Rectangle")]
    public partial class RectangleModel : ObservableObject, IShape
    {
        [ObservableProperty]
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
        private float _centerX = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _centerY = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _width = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
        private float _height = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ListOfPoints))]
        [NotifyPropertyChangedFor(nameof(Geometry))]
        [NotifyPropertyChangedFor(nameof(BoxWidth))]
        [NotifyPropertyChangedFor(nameof(BoxHeight))]
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
