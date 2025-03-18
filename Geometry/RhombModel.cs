using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Geometry;

public partial class RhombModel : ObservableObject, IShape
{
    [ObservableProperty]
    private float _strokeThickness = 1;

    [ObservableProperty]
    [JsonConverter(typeof(ColorConverter))]
    private Color _stroke = Colors.Black;

    [ObservableProperty]
    [JsonConverter(typeof(ColorConverter))]
    private Color _fill = Colors.Black;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ListOfPoints))]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    [NotifyPropertyChangedFor(nameof(BoxWidth))]
    [NotifyPropertyChangedFor(nameof(BoxHeight))]
    private float _centerX = 10;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ListOfPoints))]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    [NotifyPropertyChangedFor(nameof(BoxWidth))]
    [NotifyPropertyChangedFor(nameof(BoxHeight))]
    private float _centerY = 10;

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

    public float BoxHeight => getBoxHeight();
    public float BoxWidth => getBoxWidth();
    public float BoxCenterX => CenterX;
    public float BoxCenterY => CenterY;

    [JsonIgnore]
    private List<Avalonia.Point> ListOfPoints => getPoints();

    [JsonIgnore]
    public string Geometry => getGeometry();

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

    public List<Avalonia.Point> getPoints()
    {
        List<Avalonia.Point> list = [
            new Avalonia.Point(CenterX, CenterY - Height / 2),
            new Avalonia.Point(CenterX + Width / 2, CenterY),
            new Avalonia.Point(CenterX, CenterY + Height / 2),
            new Avalonia.Point(CenterX - Width / 2, CenterY)
            ];

        var angleRad = Angle * Math.PI / 180.0;

        for (int i = 0; i < 4; i++)
        {
            list[i] = new Avalonia.Point((CenterX + (list[i].X - CenterX) * Math.Cos(angleRad) - (list[i].Y - CenterY) * Math.Sin(angleRad)),
                (CenterY + (list[i].X - CenterX) * Math.Sin(angleRad) + (list[i].Y - CenterY) * Math.Cos(angleRad)));
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
