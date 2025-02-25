// интерфейс преобразования фигур
interface IAffineTransformable
{
    // перемещение объекта, (x, y) - координаты его верхней левой точки
    void move(IShape shape, double deltaX, double deltaY);
    // поворот фигуры относительно центра, degree - угол поворота
    void rotate(IShape shape, double angle, double x, double y);
    // отзеркаливание относительно точки
    public void reflection(IShape shape, double x, double y);
    // изменение размеров фигуры, х и у - какие параметры ширины и высоты установить
    void scale(IShape shape, double scaleX, double scaleY, double x, double y);
}
