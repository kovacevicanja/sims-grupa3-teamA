using BookingProject.Model.Enums;
using BookingProject.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Services.Interfaces
{
    public interface ISuperGuideService
    {
        void Initialize();

        void SetToSuper();

        bool IsSuper();

        bool IsSuperEnglish();


        bool IsSuperGerman();


        bool IsSuperSpanish();


        bool IsSuperSerbian();


        bool ReturnLanguageNumber(LanguageEnum language);


        bool ReturnRating(LanguageEnum language);

    }
}
