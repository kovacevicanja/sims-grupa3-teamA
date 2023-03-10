using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.ConversionHelp
{
    public class DateConversion
    {

        public static DateTime StringToDate(string date)
        {
            return DateTime.ParseExact(date, "dd.MM.yyyy.", null);
        }

        public static string DateToString(DateTime date)
        {
            return date.ToString("dd.MM.yyyy.");
        }



    }
}
