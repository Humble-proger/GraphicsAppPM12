using System;
using System.Windows.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Composition;
using Geometry;
using System.Linq;

namespace GraphicsApp.ViewModels
{
    public partial class ModelFactoryViewModel : ViewModelBase
    {
        [ObservableProperty]
        private MainWindowViewModel? _main;
        public required ExportFactory<IShape, ModelMetadata> Factory { get; init; }
        public ICommand CreateCommand { get; }

        public ModelFactoryViewModel()
        {
            CreateCommand = new RelayCommand<Avalonia.Point>((Avalonia.Point point) => Main?.Figures.Add(Create(point)));
        }

        private ShapeViewModel Create(Avalonia.Point point)
        {
            var colorFill = Main.Toolbarsview.SelectedColor;
            var tickness = Main.Toolbarsview.LineThickness;
            var colorBorder = Main.Toolbarsview.OutlineColor;
            
            var model = Factory.CreateExport().Value;
            var countFigures = Main.Figures.Count(elem => elem.Name.Split()[0] == Factory.Metadata.Name);
            var name = $"{Factory.Metadata.Name} {countFigures + 1}";
            model.Fill = colorFill;
            model.Stroke = colorBorder;
            model.StrokeThickness = (float)tickness;
            model.Move((float) point.X, (float) point.Y);
            model.StrokeThickness = (float)Main.Toolbarsview.LineThickness;

            return new() { Name = name, Model = model, Main = Main };
        }
    }
}