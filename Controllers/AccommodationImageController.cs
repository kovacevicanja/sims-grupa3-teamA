﻿using BookingProject.DependencyInjection;
using BookingProject.FileHandler;
using BookingProject.Model.Images;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class AccommodationImageController
    {
        private readonly IAccommodationImageService _imageService;
        public AccommodationImageController()
        {
            _imageService= Injector.CreateInstance<IAccommodationImageService>();
        }
        public void Create(AccommodationImage image)
        {
            _imageService.Create(image);
        }
        public List<AccommodationImage> GetAll()
        {
            return _imageService.GetAll();
        }
        public AccommodationImage GetByID(int id)
        {
            return _imageService.GetByID(id);
        }
        public void LinkToAccommodation(int id)
        {
            _imageService.LinkToAccommodation(id);
        }
        public void DeleteUnused()
        {
            _imageService.DeleteUnused();
        }


    }
}
