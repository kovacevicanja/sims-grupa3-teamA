using BookingProject.DependencyInjection;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
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
        public void Initialize ()
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
        public TourEvaluationImage GetByID(int id)
        {
            return _tourEvaluationImageRepository.GetByID(id);
        }
    }
}