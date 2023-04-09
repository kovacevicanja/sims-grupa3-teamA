using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Domain;
using BookingProject.Model.Enums;
using BookingProject.Serializer;

namespace BookingProject.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public UserType UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Name;

        public string Surname;
        public int Age { get; set; }

        public bool IsSuper { get; set; }

        public bool IsLoggedIn { get; set; }

        public List<Voucher> Vouchers { get; set; }


        public User() {
            Vouchers= new List<Voucher>();
        }



        public virtual string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserType.ToString(), Username, Password, Name, Surname, Age.ToString(), IsSuper.ToString(), IsLoggedIn.ToString() };
            return csvValues;
        }


        public virtual void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[2];
            Password = values[3];

            UserType userType;
            if (Enum.TryParse<UserType>(values[1], out userType)) 
            {
                UserType = userType;
            }
            else
            {
                userType = UserType.OWNER;
                System.Console.WriteLine("An error occurred while loading the user type");               
            }

            Name = values[4];
            Surname = values[5];
            Age= int.Parse(values[6]);
            IsSuper = bool.Parse(values[7]);
            IsLoggedIn= bool.Parse(values[8]);


        }  
    }
}
