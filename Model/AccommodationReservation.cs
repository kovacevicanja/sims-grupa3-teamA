using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model
{
    public class AccommodationReservation
    {
        public int Id { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysToStay { get; set; }
        public int AccommodationId { get; set; }
    }
}
