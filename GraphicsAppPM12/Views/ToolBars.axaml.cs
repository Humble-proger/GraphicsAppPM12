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
        
        private bool _isClearingSelection = false;
        
        // Обработчик для кнопки "Курсор"
        private void CursorButton_Click(object sender, RoutedEventArgs e)
        {
            CursorButton.IsChecked = true;
            SelectionButton.IsChecked = false;
            PenButton.IsChecked = false;
            FillButton.IsChecked = false;
            DeleteButton.IsChecked = false;
            ThicknessPopupControl.ClosePopup();
            _isClearingSelection = true;
            ShapeList.SelectedItem = null;
            _isClearingSelection = false;
        }

        // Обработчик для кнопки "Выделить"
        private void SelectionButton_Click(object sender, RoutedEventArgs e)
        {
            SelectionButton.IsChecked = true;
            CursorButton.IsChecked = false;
            PenButton.IsChecked = false;
            FillButton.IsChecked = false;
            DeleteButton.IsChecked = false;
            ThicknessPopupControl.ClosePopup();
            _isClearingSelection = true;
            ShapeList.SelectedItem = null;
            _isClearingSelection = false;
        }
        
        // Обработчик для кнопки "Перо"
        private void PenButton_Click(object sender, RoutedEventArgs e)
        {
            PenButton.IsChecked = true;
            CursorButton.IsChecked = false;
            SelectionButton.IsChecked = false;
            FillButton.IsChecked = false;
            DeleteButton.IsChecked = false;
            ThicknessPopupControl.ClosePopup();
            _isClearingSelection = true;
            ShapeList.SelectedItem = null;
            _isClearingSelection = false;
        }
        
        // Обработчик для кнопки "Заливка"
        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            FillButton.IsChecked = true;
            CursorButton.IsChecked = false;
            SelectionButton.IsChecked = false;
            PenButton.IsChecked = false;
            DeleteButton.IsChecked = false;
            ThicknessPopupControl.ClosePopup();
            _isClearingSelection = true;
            ShapeList.SelectedItem = null;
            _isClearingSelection = false;
        }
        
        // Обработчик для кнопки "Удалить"
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteButton.IsChecked = true;
            CursorButton.IsChecked = false;
            SelectionButton.IsChecked = false;
            PenButton.IsChecked = false;
            FillButton.IsChecked = false;
            ThicknessPopupControl.ClosePopup();
            _isClearingSelection = true;
            ShapeList.SelectedItem = null;
            _isClearingSelection = false;
        }
        
        // Обработчик для кнопки "Толщина"
        private void ThicknessButton_Click(object sender, RoutedEventArgs e)
        {
            ThicknessPopupControl.OpenPopup();
            
            CursorButton.IsChecked = false;
            SelectionButton.IsChecked = false;
            FillButton.IsChecked = false;
            DeleteButton.IsChecked = false;
            
        }
        // Обработчик для областм "Фигуры"
        private void ShapeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isClearingSelection)
            {
                CursorButton.IsChecked = false;
                SelectionButton.IsChecked = false;
                PenButton.IsChecked = false;
                FillButton.IsChecked = false;
                DeleteButton.IsChecked = false;
                ThicknessPopupControl.ClosePopup();
            }
        }

        
        // Скролл для области "Фигуры"
        void ScrollListUp(object sender, Avalonia.Interactivity.RoutedEventArgs e)
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
        
    }
}