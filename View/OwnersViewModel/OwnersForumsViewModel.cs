using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class OwnersForumsViewModel
    {
        public NavigationService NavigationService { get; set; }
        public ForumController ForumController { get; set; }
        public AccommodationController AccommodationController { get; set; }
        public UserController UserController { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }

        public OwnersForumsViewModel(NavigationService navigationService)
        {
            NavigationService = navigationService;
            ForumController = new ForumController();
            UserController = new UserController();
            AccommodationController= new AccommodationController();
            Forums = new ObservableCollection<Forum>();
            foreach(Accommodation a in AccommodationController.GetAllForOwner(UserController.GetLoggedUser().Id))
            {
                foreach(Forum f in ForumController.GetAll())
                {
                    if (a.IdLocation == f.Location.Id)
                    {
                        Forums.Add(f);
                    }
                }
            }
        }
    }
}
