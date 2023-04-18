using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using BookingProject.Services;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories
{
    public class TourEvaluationImageRepository : ITourEvaluationImageRepository
    {
        private const string FilePath = "../../Resources/Data/tourEvaluationImages.csv";

        private Serializer<TourEvaluationImage> _serializer;

        public List<TourEvaluationImage> _images;

        public TourEvaluationImageRepository() { }
        public void Initialize()
        {
            _serializer = new Serializer<TourEvaluationImage>();
            _images = Load();
            TourEvaluationBind();
        }
        public List<TourEvaluationImage> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourEvaluationImage> images)
        {
            _serializer.ToCSV(FilePath, images);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourEvaluationImage image in _images)
            {
                if (image.Id > maxId)
                {
                    maxId = image.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(TourEvaluationImage image)
        {
            image.Id = GenerateId();
            _images.Add(image);
            Save(_images);
        }

        public void TourEvaluationBind()
        {
            foreach (TourEvaluationImage image in _images)
            {
                TourEvaluation tourEvaluation = Injector.CreateInstance<TourEvaluationRepository>().GetByID(image.TourEvaluation.Id);
                image.TourEvaluation = tourEvaluation;
            }
        }

        public List<TourEvaluationImage> GetAll()
        {
            return _images.ToList();
        }
        public TourEvaluationImage GetByID(int id)
        {
            return _images.Find(image => image.Id == id);
        }
    }
}