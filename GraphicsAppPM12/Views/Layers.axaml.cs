using System.Collections.ObjectModel;
using System.Windows.Input;

using Avalonia.Controls;

namespace GraphicsApp.Views
{
    public partial class Layers : UserControl
    {
        public Layers()
        {
            InitializeComponent();
        }

        private void Image_SizeChanged(object? sender, Avalonia.Controls.SizeChangedEventArgs e)
        {
        }
    }
}
