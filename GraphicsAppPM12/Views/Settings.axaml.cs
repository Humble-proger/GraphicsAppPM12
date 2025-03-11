using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.IO;

namespace GraphicsApp.Views
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }
        
        private async void OnOpenFileButtonClick(object sender, RoutedEventArgs e)
        {
            // Ищем родительское окно, чтобы передать в диалог
            var parentWindow = this.FindAncestorOfType<Window>();
            if (parentWindow == null)
                return;

            var openFileDialog = new OpenFileDialog
            {
                Title = "Открыть файл",
                Directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..")), // Исходная папка — папка с .csproj
                AllowMultiple = false, // Если нужно выбрать только один файл
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter
                    {
                        Name = "PNG, SVG, JSON",
                        Extensions = new List<string> { "png", "svg", "json" }
                    },
                    new FileDialogFilter
                    {
                        Name = "Все файлы",
                        Extensions = new List<string> { "*" }
                    }
                }
            };

            // Показываем диалог и получаем выбранные файлы (массив путей)
            var result = await openFileDialog.ShowAsync(parentWindow);

            // Проверяем, выбрал ли пользователь хотя бы один файл
            if (result != null && result.Length > 0)
            {
                string filePath = result[0];
                // Логика открытия/чтения файла\
            }
        }

        private async void OnSaveAsButtonClick(object sender, RoutedEventArgs e)
        {
            // Ищем окно, в котором находится данный UserControl
            var parentWindow = this.FindAncestorOfType<Window>();
            if (parentWindow == null)
                return; 

            // Создаём диалог "Сохранить как"
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить файл как",
                Directory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..")), // Исходная папка — папка с .csproj
                InitialFileName = "NewFile", // Начальное имя файла
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter
                    {
                        Name = "PNG files",
                        Extensions = new List<string> { "png" }
                    },
                    new FileDialogFilter
                    {
                        Name = "SVG files",
                        Extensions = new List<string> { "svg" }
                    },
                    new FileDialogFilter
                    {
                        Name = "JSON files",
                        Extensions = new List<string> { "json" }
                    }
                }
            };

            // Показываем диалог, передавая ему найденное окно
            var result = await saveFileDialog.ShowAsync(parentWindow);

            if (result != null)
            {
                // Здесь логика сохранения
            }
        }

        private void OpenSettingsWindowButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}