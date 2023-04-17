using BookingProject.ConversionHelp;
using BookingProject.Model.Enums;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
   
        public class Voucher : ISerializable
        {
            public int Id { get; set; }

            public int GuestId { get; set; }
            public string Text { get; set; }

            public DateTime ValidityDate { get; set; }

            public Voucher()
            {

            }

            

            public void FromCSV(string[] values)
            {
                Id = int.Parse(values[0]);
                GuestId = int.Parse(values[1]);
                Text = values[2];
                ValidityDate = DateConversion.StringToDateTour(values[3]);

        }

            public string[] ToCSV()
            {
                string[] csvValues =
                {
                Id.ToString(),
                GuestId.ToString(),
                Text,
                 DateConversion.DateToStringTour(ValidityDate)
            };
                return csvValues;

            }
        }
    
}
