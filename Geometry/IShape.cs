using Avalonia.Media;

// интерфейс всех фигур
public interface IShape
{
    // название фигуры
    string name { get; set; }
    // координата х центра фигуры
    double CenterX { get; set; }
    // координата y центра фигуры
    double CenterY { get; set; }
    // толщина обводки)
    double StrokeThickness { get; set; }
    // цвет обводки
    IBrush Stroke { get; set; }
    // цвет заливки
    IBrush Fill { get; set; }
    // Метод для смены цвета обводки
    void SetStroke(Color color);
    // Метод для смены цвета
    void SetColor(Color color);
    // Метод для смены толщины контура
    void SetThickness(double thickness);
}