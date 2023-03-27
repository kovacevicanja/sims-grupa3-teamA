using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        
    }
}
