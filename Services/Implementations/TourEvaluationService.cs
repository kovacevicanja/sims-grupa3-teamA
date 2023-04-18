﻿using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services
{
    public class TourEvaluationService : ITourEvaluationService
    {
        public ITourEvaluationRepository _tourEvaluationRepository { get; set; }
        public TourEvaluationService() { }
        public void Initialize()
        {
            _tourEvaluationRepository = Injector.CreateInstance<ITourEvaluationRepository>();   
        }
        public void Create(TourEvaluation tourEvaluation)
        {
            _tourEvaluationRepository.Create(tourEvaluation);
        }
        public List<TourEvaluation> GetAll()
        {
            return _tourEvaluationRepository.GetAll();
        }
        public TourEvaluation GetByID(int id)
        {
            return _tourEvaluationRepository.GetByID(id);
        }
 
                /*
        public void Initialize()
        {
           _tourEvaluationRepository.Initialize();
        }
        */
    }
}