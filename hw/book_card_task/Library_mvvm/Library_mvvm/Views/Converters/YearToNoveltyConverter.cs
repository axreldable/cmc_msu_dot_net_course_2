using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Library_mvvm.Views.Converters
{
    

    [ValueConversion(typeof(int), typeof(string))]
    public class YearToNoveltyConverter : IValueConverter
    {
        private const string NewBook = "Новинка!!!";
        private const string ComingSoonBook = "Скоро в продаже";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(int)) return DependencyProperty.UnsetValue;
            int year = (int)value;
            int currentYear = DateTime.Now.Year;
            if (year == currentYear || year == currentYear - 1)
            {
                return NewBook;
            }
            if (year > currentYear) return ComingSoonBook;
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
