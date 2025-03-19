﻿using System.Collections.Generic;
using System;

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

using GraphicsApp.ViewModels;
using System.IO;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;

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
        private async void OnSelectFolderButtonClick(object sender, RoutedEventArgs e)
        {
            // Ищем окно, в котором находится данный UserControl
            var parentWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
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

            if (result != null && DataContext is SettingsWindowViewModel viewModel)
            {
                var TypeFile = Path.GetExtension(result);
                var NameFile = Path.GetFileName(result);
                if (TypeFile != null && NameFile != null)
                {
                    viewModel.SaveLocation = result;
                }
            }
        }
    }
}