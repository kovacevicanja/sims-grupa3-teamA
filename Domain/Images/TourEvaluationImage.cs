using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Domain;
using BookingProject.Serializer;

namespace BookingProject.Model.Images
{
    public class TourEvaluationImage : ISerializable
    {
        public int Id { get; set; }
        public TourEvaluation TourEvaluation { get; set; }
        public string Url { get; set; }

        public TourEvaluationImage() 
        {
            TourEvaluation = new TourEvaluation();
        }

        public TourEvaluationImage(int id, string url, TourEvaluation tour)
        {
            Id = id;
            Url = url;
            TourEvaluation = tour;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourEvaluation.Id = int.Parse(values[1]);
            Url = values[2];

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourEvaluation.Id.ToString(),
                Url,
            };
            return csvValues;
        }
    }
}
