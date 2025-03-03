using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using GraphicsApp.ViewModels;
using System.Collections.Generic;

using Avalonia.Media;

namespace GraphicsApp.Views
{
    public partial class ToolBars : UserControl
    {
        public ToolBars()
        {
            InitializeComponent();
            DataContext = new ToolBarsViewModel(); 
            
        }
        
        // Переключение видимости Popup(относится к толщине)
        private void TogglePopup(object sender, RoutedEventArgs e)
        {
            ThicknessPopupControl.OpenPopup();
        }
        
        private void ScrollListUp(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (ShapeScroll != null)
            {
                ShapeScroll.Offset = new Avalonia.Vector(0, Math.Max(ShapeScroll.Offset.Y - 41, 0));
            }
        }

        private void ScrollListDown(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (ShapeScroll != null)
            {
                double maxOffset = ShapeScroll.Extent.Height - ShapeScroll.Viewport.Height;
                ShapeScroll.Offset = new Avalonia.Vector(0, Math.Min(ShapeScroll.Offset.Y + 41, maxOffset));
            }
        }
        
        private void OnPointerWheelChanged(object? sender, PointerWheelEventArgs e)
        {
            e.Handled = true; // Блокируем событие прокрутки
        }

    }
}