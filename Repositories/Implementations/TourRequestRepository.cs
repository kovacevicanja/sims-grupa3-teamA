﻿using BookingProject.DependencyInjection;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using System.Windows.Media.Animation;

namespace BookingProject.Repositories.Implementations
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../Resources/Data/tourRequests.csv";

        private Serializer<TourRequest> _serializer;

        public List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = Load();
        }
        public void Initialize()
        {
            TourRequestLocationBind();
        }
        public List<TourRequest> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _tourRequests);
        }
        public void SaveTourRequest(List<TourRequest> tourRequests)
        {
            _serializer.ToCSV(FilePath, tourRequests);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourRequest tourRequest in _tourRequests)
            {
                if (tourRequest.Id > maxId)
                {
                    maxId = tourRequest.Id;
                }
            }
            return maxId + 1;
        }
        public TourRequest GetByID(int id)
        {
            return _tourRequests.Find(tourRequest => tourRequest.Id == id);
        }
        public List<TourRequest> GetAll()
        {
            return _tourRequests;
        }
        public void Create(TourRequest tourRequest)
        {
            tourRequest.Id = GenerateId();
            _tourRequests.Add(tourRequest);
            SaveTourRequest(_tourRequests);
        }
        public void TourRequestLocationBind()
        {
            foreach (TourRequest tourRequest in _tourRequests)
            {
                Location location = Injector.CreateInstance<ITourLocationRepository>().GetByID(tourRequest.Location.Id);
                tourRequest.Location = location;
            }
        }
        public void CheckRequestStatus()
        {
            List<TourRequest> requestsCopy = new List<TourRequest>(GetAll());
            foreach (TourRequest tourRequest in requestsCopy)
            {
                if (DateTime.Now >= tourRequest.EndDate.AddHours(-48) && tourRequest.Status == TourRequestStatus.PENDING)
                {
                    TourRequest newRequestStatus = new TourRequest(tourRequest.Id, -1, TourRequestStatus.INVALID, 
                        tourRequest.Location, tourRequest.Description, tourRequest.Language, tourRequest.GuestsNumber, tourRequest.StartDate, tourRequest.EndDate, tourRequest.Guest);
                    _tourRequests.Remove(tourRequest);
                    _tourRequests.Add(newRequestStatus);
                }
            }
            SaveTourRequest(_tourRequests);
        }
        public List<TourRequest> GetGuestRequests(int guestId)
        {
            CheckRequestStatus();

            List<TourRequest> guestRequests = new List<TourRequest>();
            foreach (TourRequest tourRequest in _tourRequests)
            {
                if (tourRequest.Guest.Id == guestId)
                {
                    guestRequests.Add(tourRequest);
                }
            }
            return guestRequests;
        }
    }
}