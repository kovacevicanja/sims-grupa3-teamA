using BookingProject.Domain;
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
    public class Guest2 : User
    {
        public new int Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public GenderOption Gender { get; set; }
        public List<TourReservation> MyTours { get; set; }
        public List<Voucher> MyVouchers{ get; set; }
        public Guest2() : base()
        {
            MyTours = new List<TourReservation>();
            MyVouchers = new List<Voucher>();
        }
        public Guest2(string name, string surname, string email, GenderOption gender)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Gender = gender;
        }

        public override string[] ToCSV()
        {
            String[] result = base.ToCSV();
            string[] csvValues = { Name, Surname, Email, Gender.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            Name = values[0];
            Surname = values[1];
            Email = values[2];

            GenderOption gender;
            if (Enum.TryParse<GenderOption>(values[3], out gender))
            {
                Gender = gender;
            }
            else
            {
                gender = GenderOption.MALE;
                System.Console.WriteLine("An error occurred while loading the gender option");
            }
        }
    }
}