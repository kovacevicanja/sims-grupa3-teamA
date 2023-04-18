using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services
{
    public class NotificationService : INotificationService
    {
        private INotificationRepository _notificationRepository;
        public NotificationService() { }
        public void Initialize()
        {
            _notificationRepository = Injector.CreateInstance<NotificationRepository>();
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
