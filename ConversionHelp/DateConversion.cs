using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.ConversionHelp
{
    public class DateConversion
    {

        public static DateTime StringToDateTour(string date)
        {
            return DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", null);
            //CultureInfo.InvariantCulture
        }

        public static DateTime StringToDateAccommodation (string date)
        {
            return DateTime.ParseExact(date, "dd.MM.yyyy.", null);
        }

        public static string DateToStringTour(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string DateToStringAccommodation (DateTime date)
        {
            return date.ToString("dd.MM.yyyy.");
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
