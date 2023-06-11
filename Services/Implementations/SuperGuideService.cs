using BookingProject.DependencyInjection;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Implementations;
using BookingProject.Repositories.Intefaces;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.Services.Implementations
{
    public class SuperGuideService : ISuperGuideService
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private ITourService _tourService;
        private ITourTimeInstanceService _tourTimeInstanceService;
        private ITourEvaluationService _tourEvaluationService;
        public SuperGuideService() { }

        public void Initialize()
        {
            _userRepository = Injector.CreateInstance<IUserRepository>();
            _userService = Injector.CreateInstance<IUserService>();
            _tourTimeInstanceService = Injector.CreateInstance<ITourTimeInstanceService>();
            _tourEvaluationService = Injector.CreateInstance<ITourEvaluationService>();
            _tourService= Injector.CreateInstance<ITourService>();
        }

        public void SetToSuper()
        {
            if (IsSuper())
            {
                _userService.GetLoggedUser().IsSuper=true;
                _userService.Save();
            }
            else
            {
                _userService.GetLoggedUser().IsSuper = false;
                _userService.Save();
            }
        }
        public bool IsSuper()
        {
            return (IsSuperEnglish() || IsSuperGerman() || IsSuperSpanish() || IsSuperSerbian()) ;
        }
        public bool IsSuperEnglish()
        {
            return (ReturnLanguageNumber(LanguageEnum.ENGLISH) && ReturnRating(LanguageEnum.ENGLISH));
        }

        public bool IsSuperGerman()
        {
            return (ReturnLanguageNumber(LanguageEnum.GERMAN) && ReturnRating(LanguageEnum.GERMAN));
        }

        public bool IsSuperSpanish()
        {
            return (ReturnLanguageNumber(LanguageEnum.SPANISH) && ReturnRating(LanguageEnum.SPANISH));
        }

        public bool IsSuperSerbian()
        {
            return (ReturnLanguageNumber(LanguageEnum.SERBIAN) && ReturnRating(LanguageEnum.SERBIAN));
        }  

        public bool ReturnLanguageNumber(LanguageEnum language)
        {
            int counter = 0;
            foreach(var instance in _tourTimeInstanceService.GetAll())
            {
                if((instance.State==TourState.COMPLETED) && (instance.Tour.Language == language)){
                    counter++;
                }
            }
            return (counter >= 20) ;
        }

        public bool ReturnRating(LanguageEnum language)
        {
            int counter = 0;
            double sum = 0;
            foreach(var evaluation in _tourEvaluationService.GetAll())
            {
                if ((evaluation.Tour.Language) == language && (_tourService.GetById(evaluation.Tour.Id).GuideId==_userService.GetLoggedUser().Id)){
                    double minisum = (evaluation.GuideLanguage + evaluation.TourInterestigness + evaluation.GuideLanguage) / 3;
                    sum += minisum;
                    counter++;
                }
            }
            if (counter != 0)
            {
                return ((sum / counter) > 4.0);
            }
            else
            {
                return false;
            }

        }

    }
}
