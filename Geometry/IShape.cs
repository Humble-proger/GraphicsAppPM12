using Avalonia.Media;

namespace Geometry
{

    // интерфейс всех фигур
    public interface IShape
    {
        // координата х центра фигуры
        float CenterX { get; set; }
        // координата y центра фигуры
        float CenterY { get; set; }
        // толщина обводки)
        float StrokeThickness { get; set; }
        // цвет обводки
        IBrush Stroke { get; set; }
        // цвет заливки
        IBrush Fill { get; set; }
        // Метод для смены цвета обводки
        void SetStroke(Color color);
        // Метод для смены цвета
        void SetColor(Color color);
        // Метод для смены толщины контура
        void SetThickness(float thickness);
        public string Geometry { get; }
    }
}