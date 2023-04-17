using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services
{
    internal class NotificationService : INotificationService
    {
        public INotificationRepository _notificationRepository { get; set; }
        public NotificationService()
        {
            _notificationRepository = Injector.CreateInstance<INotificationRepository>();
        }
        public void Create(Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public Notification GetByID(int id)
        {
            return _notificationRepository.GetByID(id);    
        }
    }
}