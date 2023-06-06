using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class OwnersProfileViewModel
    {
        public UserController UserController { get; set; }
        public NavigationService NavigationService { get; set; }
        public User User { get; set; }
        public string super { get; set; }

        public OwnersProfileViewModel(NavigationService navigationService) {
            NavigationService = navigationService;
            UserController = new UserController();
            User = UserController.GetLoggedUser();
            PictureSource = new Uri("https://media.allure.com/photos/59d2b3901457176746bd3937/1:1/w_1489,h_1489,c_limit/centenarian%20beauty%202.png");
            if (User.IsSuper)
            {
                super = "SUPER";
            } else
            {
                super = "ORDINARY";
            }
        }
        
        private bool CanExecute(object param) { return true; }
        private Uri pictureSource;

        public Uri PictureSource
        {
            get { return pictureSource; }
            set
            {
                if (pictureSource != value)
                {
                    pictureSource = value;
                    OnPropertyChanged(nameof(PictureSource));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
