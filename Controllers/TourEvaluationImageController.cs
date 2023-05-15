using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class TourEvaluationImageController
    {
        private readonly ITourEvaluationImageService _tourEvaluationImageService;
        public TourEvaluationImageController()
        {
            _tourEvaluationImageService = Injector.CreateInstance<ITourEvaluationImageService>();
        }
        public void Create(TourEvaluationImage tourEvaluationImage)
        {
            _tourEvaluationImageService.Create(tourEvaluationImage);
        }
        public List<TourEvaluationImage> GetAll()
        {
            return _tourEvaluationImageService.GetAll();
        }
        public TourEvaluationImage GetById(int id)
        {
            return _tourEvaluationImageService.GetById(id);
        }
        public void Save(List<TourEvaluationImage> images)
        {
            _tourEvaluationImageService.Save(images);
        }
        public void DeleteImageIfEvaluationNotCreated(int tourEvaluationId)
        {
            _tourEvaluationImageService.DeleteImageIfEvaluationNotCreated(tourEvaluationId);
        }
    }
}