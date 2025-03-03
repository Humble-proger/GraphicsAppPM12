using System;

using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GraphicsApp.Views
{
    public partial class Thickness : UserControl
    {
        public Thickness()
        {
            InitializeComponent();
        }
        // Событие для уведомления об изменении толщины
        public event EventHandler<int> ThicknessChanged;

        // Метод для применения толщины(вызыввется при нажатии на кнопку "применить")
        private void ApplyThickness(object sender, RoutedEventArgs e)
        {
            int thickness = (int) ThicknessSlider.Value;
            ThicknessChanged?.Invoke(this, thickness); // Вызов события
            ThicknessPopup.IsOpen = false; // Закрыть Popup
        }

        // Метод для открытия Popup
        public void OpenPopup()
        {
            ThicknessPopup.IsOpen = true;
        }
    }
}
