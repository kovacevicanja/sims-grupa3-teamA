using BookingProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface INotificationService
    {
        void Create(Notification notification);
        List<Notification> GetAll();
        Notification GetByID(int id);
    }
}
