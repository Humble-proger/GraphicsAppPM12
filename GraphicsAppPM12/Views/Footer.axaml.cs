using Avalonia.Controls;
using Avalonia.Interactivity;

using GraphicsApp.ViewModels;

namespace GraphicsApp.Views
{
    public partial class Footer : UserControl
    {
        private int _zoomLevel = 100;

        public Footer()
        {
            InitializeComponent();
        }
        
        private void OnZoomInClicked(object sender, RoutedEventArgs e)
        {
            if (_zoomLevel < 500 && DataContext is FooterViewModel viewModel) // Ограничиваем увеличение до 500%
            {
                _zoomLevel += 10;
                ZoomText.Text = $"{_zoomLevel}%";
                viewModel.Main?.Canvasview.Zoom(0.1);

            }
        }

        private void OnZoomOutClicked(object sender, RoutedEventArgs e)
        {
            if (_zoomLevel > 10 && DataContext is FooterViewModel viewModel) // Ограничиваем уменьшение до 10%
            {
                _zoomLevel -= 10;
                ZoomText.Text = $"{_zoomLevel}%";
                viewModel.Main?.Canvasview.Zoom(-0.1);
            }
        }
    }
}
