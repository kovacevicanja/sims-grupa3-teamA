using BookingProject.Model.Enums;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    public class Guide : User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public GuideType Status { get; set; }
        public List<Tour> MyTours { get; set; }
        public Guide() { }

        public Guide(string name, string surname, GuideType status)
        {
            Name = name;
            Surname = surname;
            Status = status;

        }

        public override string[] ToCSV()
        {
            string[] csvValues = { Name, Surname, Status.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            Name = values[4];
            Surname = values[5];
            GuideType status;
            if (Enum.TryParse<GuideType>(values[7], out status))
            {
                Status = status;
            }
            else
            {
                status = GuideType.NORMAL;
                System.Console.WriteLine("An error occurred while loading the status option");
            }
        }
    }
}
