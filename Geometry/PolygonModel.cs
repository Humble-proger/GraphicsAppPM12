using System.Globalization;
using System.Text.Json.Serialization;

using Avalonia.Media;

using CommunityToolkit.Mvvm.ComponentModel;

public partial class PolygonModel : ObservableObject, IShape
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
    private List<Avalonia.Point> _listOfPoints = [];
    public PolygonModel(List<Avalonia.Point> initialPoints)
    {
        _listOfPoints = initialPoints;
        CalculateCenter();
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Geometry))]
    private float _angle = 0;

    [JsonIgnore]
    public string Geometry => getGeometry();

    public void Scale(float ratioX, float ratioY)
    {
        // отражение добавить
        for (int i = 0; i < ListOfPoints.Count; i++)
        {
            ListOfPoints[i] = new Avalonia.Point(CenterX + (ListOfPoints[i].X - CenterX) * ratioX, CenterY + (ListOfPoints[i].Y - CenterY) * ratioY);
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
        var angleRod = angle * Math.PI / 180.0;
        for (int i = 0; i < ListOfPoints.Count; i++)
        {
            ListOfPoints[i] = new Avalonia.Point((float) (CenterX + (ListOfPoints[i].X - CenterX) * Math.Cos(angleRod) - (ListOfPoints[i].Y - CenterY) * Math.Sin(angleRod)),
                (float) (CenterY + (ListOfPoints[i].X - CenterX) * Math.Sin(angleRod) + (ListOfPoints[i].Y - CenterY) * Math.Cos(angleRod)));
        }

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

    public string getGeometry()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var a = "";
        if (ListOfPoints.Count > 0)
        {
            a = $"M{ListOfPoints[0].X},{ListOfPoints[0].Y} ";
        }

        for (int i = 1; i < ListOfPoints.Count; i++)
        {
            a += $"L{ListOfPoints[i].X},{ListOfPoints[i].Y} ";
        }

        a += "z";
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

        CenterX = (float) avgX;
        CenterY = (float) avgY;
    }
}
