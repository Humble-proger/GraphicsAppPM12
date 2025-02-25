using Avalonia.Controls.Shapes;
using Avalonia.Media;

class AffineTransformable : IAffineTransformable
{
    public void move(IShape shape, double deltaX, double deltaY)
    {
        if (shape is Shape)
        {
            var aShape = shape as Shape;

            var transform = new TranslateTransform();
            transform.X += deltaX;
            transform.Y += deltaY;

            var transformGroup = aShape.RenderTransform as TransformGroup ?? new TransformGroup();
            transformGroup.Children.Add(transform);
            aShape.RenderTransform = transformGroup;
        }
    }
    public void rotate(IShape shape, double angle, double x, double y)
    {
        if (shape is Shape)
        {
            var aShape = shape as Shape;

            RotateTransform transform = new RotateTransform(angle, x - shape.CenterX, y - shape.CenterY);
            var transformGroup = aShape.RenderTransform as TransformGroup ?? new TransformGroup();
            transformGroup.Children.Add(transform);
            aShape.RenderTransform = transformGroup;
        }
    }
    public void reflection(IShape shape, double x, double y)
    {
        rotate(shape, 180, x, y);
    }
    public void scale(IShape shape, double scaleX, double scaleY, double x, double y)
    {
        if (shape is Shape)
        {
            var aShape = shape as Shape;
            move(shape, (shape.CenterX - x) * scaleX, (shape.CenterY - y) * scaleY);

            var transform = new ScaleTransform(scaleX, scaleY);
            var transformGroup = aShape.RenderTransform as TransformGroup ?? new TransformGroup();
            transformGroup.Children.Add(transform);
            aShape.RenderTransform = transformGroup;
        }
    }
}
