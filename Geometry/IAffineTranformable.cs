using Avalonia.Controls.Shapes;

// интерфейс преобразования фигур
interface IAffineTransformable
{
    // перемещение объекта, (x, y) - координаты его верхней левой точки
    void move(Shape shape, double deltaX, double deltaY);
    // поворот фигуры относительно центра, degree - угол поворота
    void rotate(Shape shape, double angle, double x, double y);
    // отзеркаливание относительно точки
    public void reflection(Shape shape, double x, double y);
    // изменение размеров фигуры, х и у - какие параметры ширины и высоты установить
    void scale(Shape shape, double scaleX, double scaleY, double x, double y);
}
