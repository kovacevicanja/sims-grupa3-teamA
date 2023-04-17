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
            { typeof(ITourStartingTimeRepository), new TourStartingTimeRepository() },
            { typeof(IAccommodationService), new AccommodationService() },
            { typeof(IAccommodationOwnerGradeService), new AccommodationOwnerGradeService() },
            { typeof(IRequestAccommodationReservationService), new RequestAccommodationReservationService() },

        };
        public static void Initialize()
        {
            //CreateInstance<IVoucherRepository>().Initialize();
            CreateInstance<ITourReservationRepository>().Initialize();
            CreateInstance<IAccommodationRepository>().Initialize();
            CreateInstance<IAccommodationDateRepository>().Initialize();
            CreateInstance<IAccommodationGuestImageRepository>().Initialize();
            CreateInstance<IAccommodationOwnerGradeRepository>().Initialize();
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