using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.FileHandler
{
    public class GuestGradeHandler
    {
        private const string FilePath = "../../Resources/Data/guestGrades.csv";

        private readonly Serializer<GuestGrade> _serializer;

        public List<GuestGrade> _grades;

        public GuestGradeHandler()
        {
            _serializer = new Serializer<GuestGrade>();
            _grades = Load();
        }

        public List<GuestGrade> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(List<GuestGrade> grades)
        {
            _serializer.ToCSV(FilePath, grades);
        }
    }
}
