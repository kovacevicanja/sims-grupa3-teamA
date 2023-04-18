using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourEvaluationService
    {
        void Create(TourEvaluation tourEvaluation);
        List<TourEvaluation> GetAll();
        TourEvaluation GetByID(int id);
        void Initialize();
        void Save();
    }
}