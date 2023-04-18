using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class TourEvaluationController
    {
        private readonly ITourEvaluationService _tourEvaluationService;
        public TourEvaluationController()
        {
            _tourEvaluationService = Injector.CreateInstance<ITourEvaluationService>();
        }
        public void Create(TourEvaluation tourEvaluation)
        {
            _tourEvaluationService.Create(tourEvaluation);
        }
        public List<TourEvaluation> GetAll()
        {
            return _tourEvaluationService.GetAll();
        }
        public TourEvaluation GetByID(int id)
        {
            return _tourEvaluationService.GetByID(id);
        }
        /*
        public void Initialize()
        {
            _tourEvaluationRepository.Initialize();
        }
        */
    }
}