using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IRecommendationRenovationRepository
    {
        void Create(RecommendationRenovation recommendation);
        List<RecommendationRenovation> GetAll();
        RecommendationRenovation GetById(int id);
        void Initialize();
        void Save(List<RecommendationRenovation> recommendations);
    }
}
