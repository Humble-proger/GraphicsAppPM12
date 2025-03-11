using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;

namespace GraphicsApp.Views
{
    public partial class Layers : UserControl, INotifyPropertyChanged
    {
        private bool _isVisibleIcon = true;

        public bool IsVisibleIcon
        {
            get => _isVisibleIcon;
            set
            {
                if (_isVisibleIcon != value)
                {
                    _isVisibleIcon = value;
                    OnPropertyChanged(nameof(IsVisibleIcon));
                }
            }
        }

        
        // Коллекция слоев
        public ObservableCollection<Layer> LayersList { get; set; }

        public Layers()
        {
            InitializeComponent();

            // Инициализация коллекции слоев
            LayersList = new ObservableCollection<Layer>
            {
                new Layer { LayerName = "Слой 1", IsVisibleIcon = true },
                new Layer { LayerName = "Слой 2", IsVisibleIcon = false },
                new Layer { LayerName = "Слой 3", IsVisibleIcon = true }
            };

            DataContext = this; // Устанавливаем контекст данных

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Модель для слоя
        public class Layer
        {
            public string LayerName { get; set; }
            public bool IsVisibleIcon { get; set; }
        }
    }

    
}