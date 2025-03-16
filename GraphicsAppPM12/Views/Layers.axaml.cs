using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;

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