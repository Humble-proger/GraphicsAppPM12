// интерфейс фигур с названием (все те, что строятся не по отдельным точкам)
interface IShapeNamed : IShape
{
    // ширина
    double Width { get; set; }
    // высота
    double Height { get; set; }
}
