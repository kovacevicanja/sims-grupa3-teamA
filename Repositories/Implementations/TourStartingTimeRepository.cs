﻿using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class TourStartingTimeRepository : ITourStartingTimeRepository
    {
        private const string FilePath = "../../Resources/Data/tourStartingTime.csv";

        private Serializer<TourDateTime> _serializer;

        public List<TourDateTime> _dates;

        public TourStartingTimeRepository() { }
       
        public void Initialize()
        {
            _serializer = new Serializer<TourDateTime>();
            _dates = Load();
        }
        public List<TourDateTime> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourDateTime> dates)
        {
            _serializer.ToCSV(FilePath, dates);
        }
    
        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourDateTime date in _dates)
            {
                if (date.Id > maxId)
                {
                    maxId = date.Id;
                }
            }
            return maxId + 1;
        }

        public void Create(TourDateTime date)
        {
            date.Id = GenerateId();
            _dates.Add(date);
            Save(_dates);
        }

        public List<TourDateTime> GetAll()
        {
            return _dates.ToList();
        }

        public TourDateTime GetByID(int id)
        {
            return _dates.Find(date => date.Id == id);
        }
    }
}