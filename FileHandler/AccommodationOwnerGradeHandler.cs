using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class AccommodationOwnerGradeHandler
    {
        private const string FilePath = "../../Resources/Data/accommodationOwnerGrades.csv";

        private readonly Serializer<AccommodationOwnerGrade> _serializer;

        public List<Accommodation> _accommodations;

        public AccommodationOwnerGradeHandler()
        {
            _serializer = new Serializer<AccommodationOwnerGrade>();
            //grades = Load();
        }

        public List<AccommodationOwnerGrade> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<AccommodationOwnerGrade> grades)
        {
            _serializer.ToCSV(FilePath, grades);
        }
    }
}
