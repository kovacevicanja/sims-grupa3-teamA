using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Implementations
{
    public class RecommendationRenovationService : IRecommendationRenovationService
    {
        private IRecommendationRenovationRepository _recommendationRenovationRepository;
        public RecommendationRenovationService() { }
        public void Initialize()
        {
            _recommendationRenovationRepository = Injector.CreateInstance<IRecommendationRenovationRepository>();
        }
        public void Create(RecommendationRenovation recommendation)
        {
            _recommendationRenovationRepository.Create(recommendation);
        }
        public void Save(List<RecommendationRenovation> recommendations)
        {
            _recommendationRenovationRepository.Save(recommendations);
        }

        public List<RecommendationRenovation> GetAll()
        {
            return _recommendationRenovationRepository.GetAll();
        }

        public RecommendationRenovation GetById(int id)
        {
            return _recommendationRenovationRepository.GetById(id);
        }

        public int CountAccommodationRenovationRecommendationsForSpecificYear(int year, int accommodationId)
        {
            int number = 0;
            foreach (RecommendationRenovation renovation in _recommendationRenovationRepository.GetAll())
            {
                if (renovation.AccommodationReservation.Accommodation.Id == accommodationId && renovation.AccommodationReservation.EndDate.Year == year)
                {
                    number++;
                }
            }
            return number;
        }
        public int CountAccommodationRenovationRecommendationsForSpecificMonth(int year, int month, int accommodationId)
        {
            int number = 0;
            foreach (RecommendationRenovation renovation in _recommendationRenovationRepository.GetAll())
            {
                if (renovation.AccommodationReservation.Accommodation.Id == accommodationId && renovation.AccommodationReservation.EndDate.Month==month && renovation.AccommodationReservation.EndDate.Year == year)
                {
                    number++;
                }
            }
            return number;
        }
    }
}
