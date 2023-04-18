﻿using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationReservationRepository
    {
        void Create(AccommodationReservation reservation);
        List<AccommodationReservation> GetAll();
        AccommodationReservation GetByID(int id);
        void Initialize();
    }
}
