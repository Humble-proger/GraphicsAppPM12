using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GraphicsApp.Views
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        // Обработчик события нажатия на кнопку "Сохранить как"
        private void OnSaveAsButtonClick(object sender, RoutedEventArgs e)
        {
            Save_AsPopupControl.OpenPopup(); // Открываем Popup
        }
    }
}
