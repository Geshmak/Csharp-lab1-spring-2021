using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Lab1V4Kabanov
{
    public class ConverterCompl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            return ((DataItem)value).ElectFieldCompl.ToString("0.###") + "\nMagnitude: " + ((DataItem)value).ElectFieldCompl.Magnitude.ToString("0.###") +"\n---------------------------------";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class ConverterCoord : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "("+((DataItem)value).CoordPoint.X.ToString("0.##") + "  " + ((DataItem)value).CoordPoint.Y.ToString("0.##") +")";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ConverterMaxMin : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int num = ((V4DataOnGrid)value).Massiv.GetLength(0);
                double[] m = new double[((V4DataOnGrid)value).Massiv.Length];
                for (int i =0;i< num; i++)
                {
                    for (int j = 0; j < num; j++)
                        m[i * num + j] = ((V4DataOnGrid)value).Massiv[i, j].Magnitude;
                }
                double max = m.Max();
                double min = m.Min();
                return "Min:" + min.ToString() + "\nMax:" + max.ToString(); 
            }
            else
                return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
