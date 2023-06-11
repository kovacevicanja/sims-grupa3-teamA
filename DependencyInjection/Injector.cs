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
            { typeof(ITourTimeInstanceRepository), new TourTimeInstanceRepository() },
            { typeof(IAccommodationRepository), new AccommodationRepository() },
            { typeof(IAccommodationDateRepository), new AccommodationDateRepository() },
            { typeof(IAccommodationGuestImageRepository), new AccommodationGuestImageRepository() },
            { typeof(IAccommodationOwnerGradeRepository), new AccommodationOwnerGradeRepository() },
            { typeof(IRequestAccommodationReservationRepository), new RequestAccommodationReservationRepository() },
            { typeof(ITourEvaluationImageRepository), new TourEvaluationImageRepository() },
            { typeof(IRecommendationRenovationRepository), new RecommendationRenovationRepository() },
            { typeof(ISuperGuestRepository), new SuperGuestRepository() },
            { typeof(IAccommodationRenovationRepository), new AccommodationRenovationRepository() },
            { typeof(IForumRepository), new ForumRepository() },
            { typeof(IForumCommentRepository), new ForumCommentRepository() },
            {typeof(IComplexTourRequestRepository), new ComplexTourRequestRepository() },
            { typeof(ITourRequestRepository), new TourRequestRepository() },

            { typeof(ITourService), new TourService() },
            { typeof(ITourReservationService), new TourReservationService() },
            { typeof(ITourLocationService), new TourLocationService() },
            { typeof(IUserService), new UserService() },
            { typeof(IVoucherService), new VoucherService() },
            { typeof(ITourEvaluationImageService), new TourEvaluationImageService() },
            { typeof(ITourEvaluationService), new TourEvaluationService() },
            { typeof(IKeyPointService), new KeyPointService() },
            { typeof(ITourStartingTimeService), new TourStartingTimeService() },
            { typeof(INotificationService), new NotificationService() },
            { typeof(IGuestGradeService), new GuestGradeService() },
            { typeof(IAccommodationReservationService), new AccommodationReservationService() },
            { typeof(IAccommodationImageService), new AccommodationImageService() },
            { typeof(IAccommodationLocationService), new AccommodationLocationService() },
            { typeof(ITourPresenceService), new TourPresenceService() },
            { typeof(ITourTimeInstanceService), new TourTimeInstanceService() },
            { typeof(IAccommodationService), new AccommodationService() },
            { typeof(IAccommodationDateService), new AccommodationDateService() },
            { typeof(IAccommodationGuestImageService), new AccommodationGuestImageService() },
            { typeof(IAccommodationOwnerGradeService), new AccommodationOwnerGradeService() },
            { typeof(IRequestAccommodationReservationService), new RequestAccommodationReservationService() },
            { typeof(ITourImageService), new TourImageService() },
            { typeof(IRecommendationRenovationService), new RecommendationRenovationService() },
            { typeof(ISuperGuestService), new SuperGuestService() },
            { typeof(ITourRequestGuideService), new TourRequestGuideService() },
            { typeof(ITourRequestFilterService), new TourRequestFilterService() },
            { typeof(ITourRequestNotificationService), new TourRequestNotificationService() },
            { typeof(ITourRequestStatisticsService), new TourRequestStatisticsService() },
            { typeof(ITourSearchService), new TourSearchService() },
            { typeof(ITourStatisticsService), new TourStatisticsService() },
            { typeof(IAccommodationRenovationService), new AccommodationRenovationService() },
            { typeof(IForumService), new ForumService() },
            { typeof(IForumCommentService), new ForumCommentService() },
            { typeof(IComplexTourRequestService), new ComplexTourRequestService() },
            { typeof(ITourRequestService), new TourRequestService() },
        };
        public static void Initialize()
        {
            CreateInstance<IAccommodationDateRepository>().Initialize();
            CreateInstance<IAccommodationGuestImageRepository>().Initialize();
            CreateInstance<IAccommodationImageRepository>().Initialize();
            CreateInstance<IAccommodationLocationRepository>().Initialize();
            CreateInstance<IAccommodationOwnerGradeRepository>().Initialize();
            CreateInstance<IAccommodationReservationRepository>().Initialize();
            CreateInstance<IAccommodationRepository>().Initialize();
            CreateInstance<IGuestGradeRepository>().Initialize();
            CreateInstance<IKeyPointRepository>().Initialize();
            CreateInstance<INotificationRepository>().Initialize();
            CreateInstance<IRequestAccommodationReservationRepository>().Initialize();
            CreateInstance<ITourEvaluationImageRepository>().Initialize();
            CreateInstance<ITourEvaluationRepository>().Initialize();
            CreateInstance<ITourImageRepository>().Initialize();
            CreateInstance<ITourLocationRepository>().Initialize();
            CreateInstance<ITourPresenceRepository>().Initialize();
            CreateInstance<ITourReservationRepository>().Initialize();
            CreateInstance<ITourRepository>().Initialize();
            CreateInstance<ITourStartingTimeRepository>().Initialize();
            CreateInstance<ITourTimeInstanceRepository>().Initialize();
            CreateInstance<IUserRepository>().Initialize();
            CreateInstance<IVoucherRepository>().Initialize();
            CreateInstance<IRecommendationRenovationRepository>().Initialize();
            CreateInstance<ISuperGuestRepository>().Initialize();
            CreateInstance<IAccommodationRenovationRepository>().Initialize();
            CreateInstance<IComplexTourRequestRepository>().Initialize();
            CreateInstance<ITourRequestRepository>().Initialize();
            CreateInstance<IForumRepository>().Initialize();
            CreateInstance<IForumCommentRepository>().Initialize();

            CreateInstance<IAccommodationDateService>().Initialize();
            CreateInstance<IAccommodationGuestImageService>().Initialize();
            CreateInstance<IAccommodationImageService>().Initialize();
            CreateInstance<IAccommodationLocationService>().Initialize();
            CreateInstance<IAccommodationOwnerGradeService>().Initialize();
            CreateInstance<IAccommodationReservationService>().Initialize();
            CreateInstance<IAccommodationService>().Initialize();
            CreateInstance<IGuestGradeService>().Initialize();
            CreateInstance<IKeyPointService>().Initialize();
            CreateInstance<INotificationService>().Initialize();
            CreateInstance<IRequestAccommodationReservationService>().Initialize();
            CreateInstance<ITourEvaluationImageService>().Initialize();
            CreateInstance<ITourEvaluationService>().Initialize();
            CreateInstance<ITourImageService>().Initialize();
            CreateInstance<ITourLocationService>().Initialize();
            CreateInstance<ITourPresenceService>().Initialize();
            CreateInstance<ITourReservationService>().Initialize();
            CreateInstance<ITourService>().Initialize();
            CreateInstance<ITourStartingTimeService>().Initialize();
            CreateInstance<ITourTimeInstanceService>().Initialize();
            CreateInstance<IUserService>().Initialize();
            CreateInstance<IVoucherService>().Initialize();
            CreateInstance<ITourRequestGuideService>().Initialize();
            CreateInstance<ITourRequestStatisticsService>().Initialize();
            CreateInstance<ITourRequestNotificationService>().Initialize();
            CreateInstance<IRecommendationRenovationService>().Initialize();
            CreateInstance<ITourRequestFilterService>().Initialize();
            CreateInstance<ITourStatisticsService>().Initialize();
            CreateInstance<ITourSearchService>().Initialize();
            CreateInstance<ISuperGuestService>().Initialize();
            CreateInstance<IAccommodationRenovationService>().Initialize();
            CreateInstance<IComplexTourRequestService>().Initialize();
            CreateInstance<ITourRequestService>().Initialize();
            CreateInstance<IForumService>().Initialize();
            CreateInstance<IForumCommentService>().Initialize();
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