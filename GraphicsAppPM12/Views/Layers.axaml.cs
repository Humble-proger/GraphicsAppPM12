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

        public Layers()
        {
            InitializeComponent();
            DataContext = this; // Устанавливаем контекст данных
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}