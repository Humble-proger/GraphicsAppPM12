using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Composition;
using Geometry;

using System.Collections.ObjectModel;

namespace GraphicsApp.ViewModels
{
    public partial class ModelFactoryViewModel : ViewModelBase
    {
        [ObservableProperty]
        private MainWindowViewModel? _main;
        public required ExportFactory<IShape> Factory { get; init; }
        public ICommand CreateCommand { get; }

        public ModelFactoryViewModel()
        {
            CreateCommand = new RelayCommand(() => Main?.Figures.Add(Create()));
        }

        private ShapeViewModel Create()
        {
            var color = Color.FromRgb((byte) Random.Shared.Next(256), (byte) Random.Shared.Next(256), (byte) Random.Shared.Next(256));
            var name = $"{Factory} {Random.Shared.Next(100)}";
            var model = Factory.CreateExport().Value;
            model.CenterX = Random.Shared.Next(256);
            model.CenterY = Random.Shared.Next(256);
            return new() { Name = name, Color = color, Model = model, Main = Main };
        }
    }
}