using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourStatisticsService
    {
        void Initialize();
        List<Tour> FindToursCreatedByStatistcis();
        List<Tour> FindToursCreatedByStatistcisForGuest(int guestId);
    }
}
