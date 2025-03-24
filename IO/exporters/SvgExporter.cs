using System.Numerics;
using System.Text;

using Geometry;
using Avalonia.Media;

namespace IO
{
    public static class SvgExporter
    {
        private static string ConvertColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }


        public static void Export(string filepath, Vector2 size, IEnumerable<IShape> figures)
        {
            if (size.X <= 0 || size.Y <= 0)
            {
                throw new InvalidOperationException("Canvas dimensions must be positive.");
            }


            var svgBuilder = new StringBuilder();
            svgBuilder.AppendLine($"<svg width=\"{size.X}\" height=\"{size.Y}\" xmlns=\"http://www.w3.org/2000/svg\">");

            foreach (var shape in figures)
            {
                string svgString = $"<path d=\"{shape.Geometry}\" fill=\"{ConvertColorToHex(shape.Fill)}\" stroke=\"{shape.Stroke}\" stroke-width=\"{shape.StrokeThickness}\" />";
                svgBuilder.AppendLine(svgString);
            }

            svgBuilder.AppendLine("</svg>");

            File.WriteAllText(filepath, svgBuilder.ToString());
        }
    }
}