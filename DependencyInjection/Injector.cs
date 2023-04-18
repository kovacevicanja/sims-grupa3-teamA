using BookingProject.Repositories;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Repository;
using BookingProject.Services;
using BookingProject.Services.Implementations;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.DependencyInjection
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(IUserRepository), new UserRepository() },
            { typeof(ITourReservationRepository), new TourReservationRepository() },
            { typeof(ITourLocationRepository), new TourLocationRepository() },
            { typeof(ITourImageRepository), new TourImageRepository() },
            { typeof(ITourEvaluationRepository), new TourEvaluationRepository() },
            { typeof(IKeyPointRepository), new KeyPointRepository() },
            { typeof(ITourStartingTimeRepository), new TourStartingTimeRepository() },
            { typeof(IGuestGradeRepository), new GuestGradeRepository() },
            { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
            { typeof(IAccommodationImageRepository), new AccommodationImageRepository() },
            { typeof(IAccommodationLocationRepository), new AccommodationLocationRepository() },
            { typeof(INotificationRepository), new NotificationRepository() },
            { typeof(ITourPresenceRepository), new TourPresenceRepository() },
            { typeof(ITourTimeInstanceRepository), new TourPresenceRepository() },
            { typeof(IAccommodationRepository), new AccommodationRepository() },
            { typeof(IAccommodationDateRepository), new AccommodationDateRepository() },
            { typeof(IAccommodationGuestImageRepository), new AccommodationGuestImageRepository() },
            { typeof(IAccommodationOwnerGradeRepository), new AccommodationOwnerGradeRepository() },
            { typeof(IRequestAccommodationReservationRepository), new RequestAccommodationReservationRepository() },


            { typeof(ITourService), new TourService() },
            { typeof(ITourReservationService), new TourReservationService() },
            { typeof(ITourLocationService), new TourLocationService() },
            { typeof(IVoucherService), new VoucherService() },
            { typeof(IUserService), new UserService() },
            { typeof(IVoucherService), new VoucherService() },
            { typeof(ITourEvaluationImageService), new TourEvaluationImageService() },
            { typeof(ITourEvaluationService), new TourEvaluationService() },
            { typeof(IKeyPointService), new KeyPointService() },
            { typeof(ITourStartingTimeService), new TourStartingTimeService() },
            { typeof(INotificationService), new NotificationService() },
            { typeof(ITourStartingTimeRepository), new TourStartingTimeRepository() },
            { typeof(IGuestGradeService), new GuestGradeService() },
            { typeof(IAccommodationReservationService), new AccommodationReservationService() },
            { typeof(IAccommodationImageService), new AccommodationImageService() },
            { typeof(INotificationService), new NotificationService() },
            { typeof(IAccommodationLocationService), new AccommodationLocationService() },
            { typeof(ITourPresenceService), new TourPresenceService() },
            { typeof(ITourTimeInstanceService), new TourTimeInstanceService() },
            { typeof(IAccommodationService), new AccommodationService() },
            { typeof(IAccommodationDateService), new AccommodationDateService() },
            { typeof(IAccommodationGuestImageService), new AccommodationGuestImageService() },
            { typeof(IAccommodationOwnerGradeService), new AccommodationOwnerGradeService() },
            { typeof(IRequestAccommodationReservationService), new RequestAccommodationReservationService() },


        };
        public static void Initialize()
        {
            CreateInstance<IVoucherRepository>().Initialize();
            CreateInstance<ITourReservationRepository>().Initialize();
            CreateInstance<IUserRepository>().Initialize();
            CreateInstance<ITourRepository>().Initialize();
            CreateInstance<ITourReservationRepository>().Initialize();
            CreateInstance<ITourLocationRepository>().Initialize();  
            CreateInstance<ITourEvaluationImageRepository>().Initialize();
            CreateInstance<ITourEvaluationRepository>().Initialize();
            CreateInstance<IKeyPointRepository>().Initialize();
            CreateInstance<ITourStartingTimeRepository>().Initialize();
            CreateInstance<INotificationRepository>().Initialize();
            CreateInstance<IGuestGradeService>().Initialize();
            CreateInstance<IAccommodationReservationService>().Initialize();
            CreateInstance<IAccommodationImageService>().Initialize();
            CreateInstance<INotificationService>().Initialize();
            CreateInstance<IAccommodationRepository>().Initialize();
            CreateInstance<IAccommodationDateRepository>().Initialize();
            CreateInstance<IAccommodationGuestImageRepository>().Initialize();
            CreateInstance<IAccommodationOwnerGradeRepository>().Initialize();
            CreateInstance<IRequestAccommodationReservationRepository>().Initialize();
        }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}