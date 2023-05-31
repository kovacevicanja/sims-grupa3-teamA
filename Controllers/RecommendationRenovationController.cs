using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class RecommendationRenovationController
    {
        private readonly IRecommendationRenovationService recommendationRenovationService;
        public RecommendationRenovationController()
        {
            recommendationRenovationService = Injector.CreateInstance<IRecommendationRenovationService>();
        }
        public void Create(RecommendationRenovation recommendation)
        {
            recommendationRenovationService.Create(recommendation);
        }
        public List<RecommendationRenovation> GetAll()
        {
            return recommendationRenovationService.GetAll();
        }
        public void Save(List<RecommendationRenovation> recommendations)
        {
            recommendationRenovationService.Save(recommendations);
        }
        public RecommendationRenovation GetById(int id)
        {
            return recommendationRenovationService.GetById(id);
        }
        public int CountAccommodationRenovationRecommendationsForSpecificYear(int year, int accId)
        {
            return recommendationRenovationService.CountAccommodationRenovationRecommendationsForSpecificYear(year, accId);
        }
        public int CountAccommodationRenovationRecommendationsForSpecificMonth(int year,int month, int accId)
        {
            return recommendationRenovationService.CountAccommodationRenovationRecommendationsForSpecificMonth(year, month, accId);
        }
        public List<RecommendationRenovation> GetAllCancelledForAccommodation(int accId)
        {
            List<RecommendationRenovation> res = new List<RecommendationRenovation>();
            foreach (RecommendationRenovation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == accId)
                {
                    res.Add(reservation);
                }
            }
            return res;
        }
        public int CountResForAcc(int accId)
        {
            int res = 0;
            foreach (RecommendationRenovation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == accId)
                {
                    res++;
                }
            }
            return res;
        }
        public List<RecommendationRenovation> GetAllCancelledForAccommodationAndYear(int accId, int year)
        {
            List<RecommendationRenovation> res = new List<RecommendationRenovation>();
            foreach (RecommendationRenovation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == accId && reservation.AccommodationReservation.EndDate.Year==year)
                {
                    res.Add(reservation);
                }
            }
            return res;
        }
        public int CountResForAccAndYear(int accId, int year)
        {
            int res = 0;
            foreach (RecommendationRenovation reservation in GetAll())
            {
                if (reservation.AccommodationReservation.Accommodation.Id == accId && reservation.AccommodationReservation.EndDate.Year == year)
                {
                    res++;
                }
            }
            return res;
        }
    }
}
