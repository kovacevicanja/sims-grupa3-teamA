using BookingProject.DependencyInjection;
using BookingProject.Domain;
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
    }
}
