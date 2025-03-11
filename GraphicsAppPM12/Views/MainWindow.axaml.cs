using Avalonia.Controls;
using Avalonia.Interactivity;
using GraphicsApp.ViewModels;

namespace GraphicsApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Устанавливаем DataContext
        }

        private async void OpenSettings(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(); // ������� ��������� SettingsWindow
            await settingsWindow.ShowDialog(this);       // ��������� ��� ���������� ����, this - ��������
        }

    }
}
