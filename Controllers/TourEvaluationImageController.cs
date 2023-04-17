using BookingProject.Controllers;
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

namespace BookingProject.Controller
{
    public class TourEvaluationImageController: ISubject
    {
        private readonly List<IObserver> observers;
        private readonly TourEvaluationImageHandler _imageHandler;
        private List<TourEvaluationImage> _images;
        private TourEvaluationController _tourEvaluationController;
        public TourEvaluationImageController()
        {
            _imageHandler = new TourEvaluationImageHandler();
            _images = new List<TourEvaluationImage>();
            observers = new List<IObserver>();
            _tourEvaluationController = new TourEvaluationController();
            Load();
        }
        public void Load()
        {
            _images = _imageHandler.Load();
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (TourEvaluationImage image in _images)
            {
                if (image.Id > maxId)
                {
                    maxId = image.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(TourEvaluationImage image) 
        {
            image.Id = GenerateId();
            _images.Add(image);
            NotifyObservers();
        }
        public void Save()
        {
            _imageHandler.Save(_images);
            NotifyObservers();
        }
        public void TourEvaluationBind()
        {
            _tourEvaluationController.Load();
            foreach (TourEvaluationImage image in _images)
            {
                TourEvaluation tourEvaluation = _tourEvaluationController.GetByID(image.TourEvaluation.Id);
                image.TourEvaluation = tourEvaluation;
            }
            NotifyObservers();
        }
        public List<TourEvaluationImage> GetAll()
        {
            return _images;
        }
        public TourEvaluationImage GetByID(int id)
        {
            return _images.Find(image => image.Id == id);
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