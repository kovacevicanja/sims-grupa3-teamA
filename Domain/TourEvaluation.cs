using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingProject.Domain
{
    public class TourEvaluation: ISerializable
    {
        public int Id { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int TourInterestigness { get; set; }
        public string AdditionalComment { get; set; }
        public List<TourEvaluationImage> Images { get; set; }  
        public Tour Tour { get; set; }
        public TourEvaluation() 
        {
            Images = new List<TourEvaluationImage>();
            Tour = new Tour();
        }
        public TourEvaluation (int id, int knowledge, int language, int interestigness, string comment, List<TourEvaluationImage> images, Tour tour)
        {
            Id = id;
            GuideKnowledge = knowledge;
            GuideLanguage = language;
            TourInterestigness = interestigness;
            AdditionalComment = comment;
            Images = images;
            Tour = tour;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuideKnowledge = int.Parse(values[1]);
            GuideLanguage = int.Parse(values[2]);
            TourInterestigness = int.Parse(values[3]);
            AdditionalComment = values[4];
            Tour.Id = int.Parse(values[5]);
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            GuideKnowledge.ToString(),
            GuideLanguage.ToString(),
            TourInterestigness.ToString(),
            AdditionalComment, 
            Tour.Id.ToString()
            };
            return csvValues;
        }
    }
}