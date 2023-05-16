using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class MonthData
    {
        public int Month { get; set; }
        public int NumberOfReservations { get; set; }
        public int NumberOfCancelledReservations { get; set; }
        public int NumberOfRescheduledReservations { get; set; }
        public int NumberOfRenovationRecommendations { get; set; }

        public MonthData() { }
    }
}
