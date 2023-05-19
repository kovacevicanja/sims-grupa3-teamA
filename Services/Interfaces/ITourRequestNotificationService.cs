using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ITourRequestNotificationService
    {
        void SendNotification(User guest, Tour CreatedTour);
        void SystemSendingNotification(int guestId);
        void Initialize();
    }
}