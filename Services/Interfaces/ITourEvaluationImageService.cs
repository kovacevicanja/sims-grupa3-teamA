using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourEvaluationImageService 
    {
        void Create(TourEvaluationImage tourEvaluationImage);
        List<TourEvaluationImage> GetAll();
        TourEvaluationImage GetById(int id);
        void Initialize();
        void Save(List<TourEvaluationImage> images);
        void DeleteImageIfEvaluationNotCreated(int tourEvaluationId);
    }
}
