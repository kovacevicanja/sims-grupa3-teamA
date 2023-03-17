﻿using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class AccommodationLocationHandler
    {
        private const string FilePath = "../../Resources/Data/accommodationLocations.csv";

        private readonly Serializer<Location> _serializer;

        public List<Location> _locations;

        public AccommodationLocationHandler()
        {
            _serializer = new Serializer<Location>();
            _locations = Load();
        }

        public List<Location> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<Location> locations)
        {
            _serializer.ToCSV(FilePath, locations);
        }
    }
}