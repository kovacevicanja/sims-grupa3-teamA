﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public User() { }

        public User(string username, string password, UserType userType)
        {
            Username = username;
            Password = password;
            UserType = userType;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserType.ToString(), Username, Password };
            return csvValues;
        }

        public void FromCSV(string[] values)
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
        }

        
    }
}
