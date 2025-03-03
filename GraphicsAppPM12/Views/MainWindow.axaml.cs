using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GraphicsApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OpenSettings(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(); // ������� ��������� SettingsWindow
            await settingsWindow.ShowDialog(this);       // ��������� ��� ���������� ����, this - ��������
        }

    }
}
