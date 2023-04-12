using BookingProject.Controller;
using BookingProject.Domain;
using BookingProject.FileHandler;
using BookingProject.Model;
using BookingProject.Model.Images;
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
        private readonly List<IObserver> observers;

        private readonly TourEvaluationHandler _tourEvaluationHandler;

        private List<TourEvaluation> _tourEvaluations; 
        private TourController _tourController { get; set; }

        private List<TourEvaluationImage> _images { get; set; }


        public TourEvaluationController()
        {
            _tourEvaluationHandler = new TourEvaluationHandler();
            _tourEvaluations = new List<TourEvaluation>();
            _tourController = new TourController();
            _images = new List<TourEvaluationImage>();
            Load();
        }

        public void Load()
        {
            _tourEvaluations = _tourEvaluationHandler.Load();
            BindTourEvaluationToTour();
            TourEvaluationImagesBind();
        }

        public void BindTourEvaluationToTour()
        {
            _tourController.Load();
            foreach (TourEvaluation evaluation in _tourEvaluations)
            {
                Tour tour = _tourController.GetByID(evaluation.Tour.Id);
                evaluation.Tour = tour;
            }
        }

        public void TourEvaluationImagesBind()
        {
            TourEvaluationImageHandler imageHandler = new TourEvaluationImageHandler();
            _images = imageHandler.Load();

            foreach (TourEvaluation tourEvaluation in _tourEvaluations)
            {
                foreach (TourEvaluationImage tourImage in _images)
                {
                    if (tourEvaluation.Id == tourImage.TourEvaluation.Id)
                    {
                        tourEvaluation.Images.Add(tourImage);
                    }
                }
            }
        }

        public List<TourEvaluation> GetAll()
        {
            return _tourEvaluations;
        }

        public int GenerateId()
        {
            int maxId = 0;
            foreach (TourEvaluation tourEvaluation in _tourEvaluations)
            {
                if (tourEvaluation.Id > maxId)
                {
                    maxId = tourEvaluation.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(TourEvaluation tourEvalution)
        {
            tourEvalution.Id = GenerateId();
            _tourEvaluations.Add(tourEvalution);
        }

        public void SaveEvaluation()
        {
            _tourEvaluationHandler.Save(_tourEvaluations);
        }

        public TourEvaluation GetByID(int id)
        {
            return _tourEvaluations.Find(tourEvalution => tourEvalution.Id == id);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }
    }
}
