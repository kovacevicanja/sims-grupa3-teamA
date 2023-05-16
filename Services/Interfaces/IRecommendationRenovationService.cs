using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface IRecommendationRenovationService
    {
        void Initialize();
        void Create(RecommendationRenovation recommendationRenovation);
        void Save(List<RecommendationRenovation> recommendations);
        List<RecommendationRenovation> GetAll();
        RecommendationRenovation GetById(int id);
        int CountAccommodationRenovationRecommendationsForSpecificYear(int year, int accommodationId);
        int CountAccommodationRenovationRecommendationsForSpecificMonth(int year, int month, int accommodationId);
    }
}
