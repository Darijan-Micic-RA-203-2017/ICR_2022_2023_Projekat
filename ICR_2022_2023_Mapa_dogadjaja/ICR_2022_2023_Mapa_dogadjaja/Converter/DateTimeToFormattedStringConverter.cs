using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace ICR_2022_2023_Mapa_dogadjaja.Converter
{
    public class DateTimeToFormattedStringConverter : MarkupExtension, IValueConverter
    {
        public DateTimeToFormattedStringConverter() { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime) value;

            string dateTimeAsString;
            try
            {
                dateTimeAsString = dateTime.ToString("MM/dd/yyyy");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                return null;
            }

            return dateTimeAsString;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateTimeAsString = value as string;

            DateTime dateTime;
            if (DateTime.TryParse(dateTimeAsString, out dateTime))
            {
                return dateTime;
            }
            else
            {
                return value;
            }
        }
    }
}
