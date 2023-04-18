using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Model;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Repositories;
using BookingProject.Repositories.Intefaces;

namespace BookingProject.Services
{
    public class TourEvaluationRepository : ITourEvaluationRepository
    {
        private const string FilePath = "../../Resources/Data/tourEvaluation.csv";
        private Serializer<TourEvaluation> _serializer;
        public List<TourEvaluation> _tourEvaluations;
        private List<TourEvaluationImage> _images;
        private TourRepository _tourRepository;

        public TourEvaluationRepository() { }

        public void Initialize()
        {
            _serializer = new Serializer<TourEvaluation>();
            _images = new List<TourEvaluationImage>();
            _tourRepository = new TourRepository();
            _tourEvaluations = Load();
        }

        public List<TourEvaluation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _tourEvaluations);
        }
        public void BindTourEvaluationToTour()
        {
            foreach (TourEvaluation evaluation in _tourEvaluations)
            {
                Tour tour = _tourRepository.GetByID(evaluation.Tour.Id);
                evaluation.Tour = tour;
            }
        }
        public void TourEvaluationImagesBind()
        {
            TourEvaluationImageRepository imageRepository = new TourEvaluationImageRepository();
            _images = imageRepository.GetAll();

            foreach (TourEvaluation tourEvaluation in _tourEvaluations)
            {
                foreach (TourEvaluationImage tourImage in _images)
                {
                    if (tourEvaluation.Id == tourImage.TourEvaluation.Id)
                    {
                        tourEvaluation.Images.Add(tourImage);
                    }
                }
            }
        }
        public List<TourEvaluation> GetAll()
        {
            return _tourEvaluations.ToList();
        }
        public int GenerateId()
        {
            int maxId = 0;
            foreach (TourEvaluation tourEvaluation in _tourEvaluations)
            {
                if (tourEvaluation.Id > maxId)
                {
                    maxId = tourEvaluation.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(TourEvaluation tourEvalution)
        {
            tourEvalution.Id = GenerateId();
            _tourEvaluations.Add(tourEvalution);
            Save();
        }

        public TourEvaluation GetByID(int id)
        {
            return _tourEvaluations.Find(tourEvalution => tourEvalution.Id == id);
        }
    }
}
