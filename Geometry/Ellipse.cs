using Avalonia.Controls;
using Avalonia.Media;

public class Ellipse : Avalonia.Controls.Shapes.Ellipse
{
    public Ellipse(double x, double y, double width, double height, double thickness = 0, string color = "#000000")
    {
        Width = width;
        Height = height;
        if (thickness == 0)
        {
            Fill = new SolidColorBrush(Color.Parse(color));
        }
        Stroke = new SolidColorBrush(Color.Parse(color));
        StrokeThickness = thickness;
        Canvas.SetLeft(this, x);
        Canvas.SetTop(this, y);
    }
}