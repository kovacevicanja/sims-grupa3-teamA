using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    internal class KeyPoint
    {
        public int Id { get; set; }

        public int Tourid { get; set; }
        public string Point { get; set; }
        public KeyPoint() { }

        public KeyPoint(int id, int tourId, string point)
        {
            Id = id;
            Tourid = tourId;
            Point = point;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Tourid = int.Parse(values[1]);
            Point = values[2];

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Tourid.ToString(),
                Point,
            };
            return csvValues;

        }
    }
}
