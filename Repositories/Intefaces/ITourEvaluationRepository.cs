using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourEvaluationRepository
    {
        void Create(TourEvaluation tourEvaluation);
        List<TourEvaluation> GetAll();
        TourEvaluation GetByID(int id);
        void Initialize();
    }
}
