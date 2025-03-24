using System;
using System.Collections.Generic;
using System.Globalization;

using Avalonia.Data.Converters;

namespace GraphicsApp.ViewModels
{
    public class NumericConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // �������������� �� �������� � ����� (OneWay)
            return value?.ToString() ?? string.Empty;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // �������������� �� ������ � �������� (TwoWay)
            string strValue = value as string ?? string.Empty;
            if (string.IsNullOrEmpty(strValue))
                return null;

            // �������� ������������� � �����
            if (double.TryParse(strValue, out double numericValue))
            {
                return numericValue;
            }

            // ���� �� �����, ���������� null ��� ������� ��������
            return null;
        }
    }
}