using System;
using System.Collections.Generic;

using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace GraphicsApp.Views
{
    public partial class ToolBars : UserControl
    {
        public ToolBars()
        {
            InitializeComponent();

            // Создаем список фигур и сортируем его по имени
            var shapes = new List<ShapeItem>
            {
                new ShapeItem("Линия", "pictures/ToolBars/line.png"),
                new ShapeItem("Кривая", "pictures/ToolBars/polyline.png"),
                new ShapeItem("Квадрат", "pictures/ToolBars/square.png"),
                new ShapeItem("Треугольник", "pictures/ToolBars/triangle.png"),
                new ShapeItem("Круг", "pictures/ToolBars/circle.png")
            };

            // Устанавливаем DataContext для UserControl
            this.DataContext = this;

            // Устанавливаем ItemsSource для ListBox
            shapesList.ItemsSource = shapes;
        }

        // Переключение видимости Popup
        private void TogglePopup(object sender, RoutedEventArgs e)
        {
            ThicknessPopupControl.OpenPopup();
        }
    }

    public class ShapeItem
    {
        public string Name { get; }
        public string IconPath { get; set; }

        public ShapeItem(string name, string iconPath)
        {
            Name = name;
            IconPath = iconPath;
        }
    }
}