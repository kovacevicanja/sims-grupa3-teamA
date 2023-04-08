using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class TourEvaluationHandler
    {
        private const string FilePath = "../../Resources/Data/tourEvaluation.csv";
        private readonly Serializer<TourEvaluation> _serializer;
        public List<TourEvaluation> _tourEvaluations;

        public TourEvaluationHandler()
        {
            _serializer = new Serializer<TourEvaluation>();
            _tourEvaluations = Load();
        }

        public List<TourEvaluation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<TourEvaluation> tourEvaluations)
        {
            _serializer.ToCSV(FilePath, tourEvaluations);
        }
    }
}
