using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
   
    public class RecommendationRenovationRepository : IRecommendationRenovationRepository
    {
        private const string FilePath = "../../Resources/Data/renovationrecommendations.csv";

        private Serializer<RecommendationRenovation> _serializer;

        public List<RecommendationRenovation> _recommendations;
        public RecommendationRenovationRepository()
        {
            _serializer = new Serializer<RecommendationRenovation>();
            _recommendations = Load();
        }
        public void Initialize()
        {
            ReservationRecommendationBind();
        }
        public List<RecommendationRenovation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<RecommendationRenovation> recommendations)
        {
            _serializer.ToCSV(FilePath, recommendations);
        }
        public List<RecommendationRenovation> GetAll()
        {
            return _recommendations.ToList();
        }
        public RecommendationRenovation GetById(int id)
        {
            return _recommendations.Find(r => r.Id == id);
        }
        public void Create(RecommendationRenovation recommendation)
        {
            recommendation.Id = GenerateId();
            _recommendations.Add(recommendation);
            Save(_recommendations);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (RecommendationRenovation recommendation in _recommendations)
            {
                if (recommendation.Id > maxId)
                {
                    maxId = recommendation.Id;
                }
            }
            return maxId + 1;
        }
        public void ReservationRecommendationBind()
        {
            foreach (RecommendationRenovation recommendation in _recommendations)
            {
                AccommodationReservation accommodationReservation = Injector.CreateInstance<IAccommodationReservationRepository>().GetById(recommendation.AccommodationReservation.Id);
                recommendation.AccommodationReservation = accommodationReservation;
            }
        }
    }
}
