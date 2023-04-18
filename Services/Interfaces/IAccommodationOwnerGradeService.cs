﻿using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IAccommodationOwnerGradeService
    {
        void Initialize();
        bool ExistsAccommodationGradeForAccommodationId(int accomomodationId);
        List<AccommodationOwnerGrade> GetGradesForAccAndUserLastN(int accId, int userId, int n);
        bool ExistsAlreadyAccommodationAndUser(int accId, int userId, List<AccommodationOwnerGrade> grades);
        List<AccommodationOwnerGrade> GradesGradedByBothSidesForOwner(int ownerId);
        bool IsOwnerSuperOwner(int ownerId);
        void Create(AccommodationOwnerGrade grade);
        List<AccommodationOwnerGrade> GetAll();
        AccommodationOwnerGrade GetByID(int id);
    }
}
