using Avalonia.Controls;
using Avalonia.Interactivity;

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
            if (_zoomLevel < 500) // Ограничиваем увеличение до 500%
            {
                _zoomLevel += 10;
                ZoomText.Text = $"{_zoomLevel}%";
            }
        }

        private void OnZoomOutClicked(object sender, RoutedEventArgs e)
        {
            if (_zoomLevel > 10) // Ограничиваем уменьшение до 10%
            {
                _zoomLevel -= 10;
                ZoomText.Text = $"{_zoomLevel}%";
            }
        }
    }
}
