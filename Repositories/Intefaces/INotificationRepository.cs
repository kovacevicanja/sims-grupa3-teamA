using BookingProject.Domain;
using BookingProject.Model.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface INotificationRepository
    {
        void Create(Notification notification);
        List<Notification> GetAll();
        Notification GetByID(int id);
        void Initialize();
        void DeleteNotificationFromCSV(Notification notification);
        void Save();
    }
}