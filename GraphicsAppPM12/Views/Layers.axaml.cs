using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Metadata;

using GraphicsApp.ViewModels;

namespace GraphicsApp.Views
{
    public partial class Layers : UserControl
    {


        public Layers()
        {
            InitializeComponent();

            DataContext = this; // Устанавливаем контекст данных

        }
        private void OnDeleteButtonClick(object sender, RoutedEventArgs e) {
            if (DataContext is LayersViewModel viewmodel && viewmodel.Main is not null) {
                if (viewmodel.Main.SelectedFigure is not null)
                    viewmodel.Main.SelectedFigure.Remove.Execute(null);
                    viewmodel.Main.SelectedFigure = null;
            }

        }
    }
}