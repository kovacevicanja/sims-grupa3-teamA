using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model.Images
{
    internal class TourImages
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Tour Tour{ get; set; }
        public TourImages()
        {
            Tour = new Tour();
        }
    }
}
