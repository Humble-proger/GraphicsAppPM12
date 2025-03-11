using Avalonia.Media;

namespace Geometry
{

    // интерфейс всех фигур
    public interface IShape
    {
        // ширина описанного прямоугольника
        float BoxWidth { get; }
        // высота описанного прямоугольника
        float BoxHeight { get; }

        // координата х центра фигуры
        float CenterX { get; }
        // координата y центра фигуры
        float CenterY { get; }
        // толщина обводки)
        float StrokeThickness { get; set; }
        // цвет обводки
        IBrush Stroke { get; set; }
        // цвет заливки
        IBrush Fill { get; set; }
        public void Move(float deltaX, float deltaY);
        public void Scale(float ratioX, float ratioY);
        public void Rotate(float angle);
        public string Geometry { get; }
    }
}