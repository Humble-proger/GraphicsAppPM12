using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

class AffineTransformable : IAffineTransformable
{
    public void move(Shape shape, double deltaX, double deltaY)
    {
        Canvas.SetLeft(shape, deltaX);
        Canvas.SetTop(shape, deltaY);
    }
    public void rotate(Shape shape, double angle, double x, double y)
    {
        if (shape is IShape)
        {
            var ishape = shape as IShape;
            var x_ = x - ishape.CenterX;
            var y_ = y - ishape.CenterY;

            RotateTransform rotateTransform = new RotateTransform(angle, x_, y_);
            shape.RenderTransform = rotateTransform;
        }
    }
    public void reflection(Shape shape, double x, double y)
    {
        rotate(shape, 180, x, y);
    }
    public void scale(Shape shape, double scaleX, double scaleY, double x, double y)
    {
        var X = Canvas.GetLeft(shape);
        var Y = Canvas.GetRight(shape);
        shape.Width *= scaleX;
        shape.Height *= scaleY;
        Canvas.SetLeft(shape, x + (X - x) * scaleX);
        Canvas.SetRight(shape, y + (Y - y) * scaleY);
    }
}
