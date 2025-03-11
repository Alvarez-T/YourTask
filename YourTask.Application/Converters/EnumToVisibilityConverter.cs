using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace YourTask.Application.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Visible; // Se não há valor ou parâmetro, mantém visível por padrão.

            string targetValue = parameter.ToString()!;
            string currentValue = value.ToString()!;

            return currentValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase)
                ? Visibility.Collapsed  // Oculta se for igual ao parâmetro.
                : Visibility.Visible;   // Mostra se for diferente.
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
