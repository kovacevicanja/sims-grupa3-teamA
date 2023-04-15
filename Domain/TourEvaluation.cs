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
        public TourReservation TourReservation { get; set; }

        public bool IsValid { get; set; }
        public TourEvaluation() 
        {
            Images = new List<TourEvaluationImage>();
            TourReservation = new TourReservation();
            IsValid= true;
        }
        public TourEvaluation (int id, int knowledge, int language, int interestigness, string comment, List<TourEvaluationImage> images, TourReservation tourReservation, bool isValid)
        {
            Id = id;
            GuideKnowledge = knowledge;
            GuideLanguage = language;
            TourInterestigness = interestigness;
            AdditionalComment = comment;
            Images = images;
            TourReservation = tourReservation;
            IsValid = isValid;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuideKnowledge = int.Parse(values[1]);
            GuideLanguage = int.Parse(values[2]);
            TourInterestigness = int.Parse(values[3]);
            AdditionalComment = values[4];
            TourReservation.Id = int.Parse(values[5]);
            IsValid = bool.Parse(values[6]);
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
            TourReservation.Id.ToString(),
            IsValid.ToString(),
            };
            return csvValues;
        }
    }
}