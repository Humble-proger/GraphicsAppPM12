using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Metadata;

namespace GraphicsApp.Views
{
    public partial class Layers : UserControl
    {


        public Layers()
        {
            InitializeComponent();

            DataContext = this; // Устанавливаем контекст данных

        }
    }
}