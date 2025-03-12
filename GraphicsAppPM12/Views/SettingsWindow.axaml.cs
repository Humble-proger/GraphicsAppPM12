using Avalonia.Controls;
using Avalonia.Interactivity;

using GraphicsApp.ViewModels;

namespace GraphicsApp.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки OK
        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            // Здесь можно выполнить логику для сохранения изменений
            this.Close(); 
        }
        
        // Обработчик для кнопки Отмена
        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}