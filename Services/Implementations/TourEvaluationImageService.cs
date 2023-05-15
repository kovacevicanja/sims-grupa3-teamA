using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services
{
    public class TourEvaluationImageService : ITourEvaluationImageService
    {
        public ITourEvaluationImageRepository _tourEvaluationImageRepository { get; set; }
        public TourEvaluationImageService() { }
        public void Initialize()
        {
            _tourEvaluationImageRepository = Injector.CreateInstance<ITourEvaluationImageRepository>();
        }
        public void Create(TourEvaluationImage tourEvaluationImage)
        {
            _tourEvaluationImageRepository.Create(tourEvaluationImage);
        }
        public List<TourEvaluationImage> GetAll()
        {
            return _tourEvaluationImageRepository.GetAll();
        }
        public TourEvaluationImage GetById(int id)
        {
            return _tourEvaluationImageRepository.GetById(id);
        }
        public void Save(List<TourEvaluationImage> images) 
        { 
            _tourEvaluationImageRepository.Save(images);    
        }
        public void DeleteImageIfEvaluationNotCreated(int tourEvaluationId)
        {
            List<TourEvaluationImage> images = GetAll();
            List <TourEvaluationImage> imagesCopy = new List<TourEvaluationImage>(images);

            foreach (TourEvaluationImage image in imagesCopy)
            {
                if (image.TourEvaluation.Id == tourEvaluationId)
                {
                    images.Remove(image);
                }
            }
            Save(images);
        }
    }
}