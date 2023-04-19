using BookingProject.Domain;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourEvaluationImageRepository
    {
        void Create(TourEvaluationImage tourEvaluationImage);
        List<TourEvaluationImage> GetAll();
        TourEvaluationImage GetByID(int id);
        void Initialize();
        void Save(List<TourEvaluationImage> images);
    }
}