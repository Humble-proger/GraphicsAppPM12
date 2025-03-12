using System;
using System.Windows.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Composition;
using Geometry;

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
            var color = Main.Toolbarsview.SelectedColor;
            var name = $"{Factory} {Random.Shared.Next(100)}";
            var model = Factory.CreateExport().Value;

            model.Move((float) point.X, (float) point.Y);
            model.StrokeThickness = (float)Main.Toolbarsview.LineThickness;

            return new() { Name = name, Model = model, SelectedColor = color, Main = Main };
        }
    }
}