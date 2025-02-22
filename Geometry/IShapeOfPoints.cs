// интерфейс фигур, строящихся по точкам
interface IShapeOfPoints : IShape
{
    // список точек составляющих фигуру
    List<Avalonia.Point> Points { get; set; }

    // метод добавления новых точек
    void addPoint(Avalonia.Point point);
    // метод удаления точек
    void deletePoint(Avalonia.Point point);
}
