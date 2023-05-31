﻿using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public List<ForumComment> Comments { get; set; }
        public bool IsUseful { get; set; }

        public Forum() {
            Location = new Location();
            Comments = new List<ForumComment>();
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Location.Id = int.Parse(values[2]);
            IsUseful = bool.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Location.Id.ToString(),
                IsUseful.ToString()
            };
            return csvValues;
        }
    }
}