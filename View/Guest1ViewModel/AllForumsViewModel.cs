using BookingProject.Commands;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.View.Guest1ViewModel
{
	public class AllForumsViewModel
	{
        public ObservableCollection<Forum> Forums { get; set; }
        public IForumService ForumService { get; set; }
        public Forum SelectedForum { get; set; }
        public RelayCommand ShowCommentsCommand { get; set; }
        public RelayCommand CloseForumCommand { get; set; }

        public AllForumsViewModel()
        {
            ForumService = Injector.CreateInstance<IForumService>();
            Forums = new ObservableCollection<Forum>(ForumService.GetAll());
            ShowCommentsCommand = new RelayCommand(ShowForumComments);
            CloseForumCommand = new RelayCommand(CloseForumButton);
        }
        public void CloseForumButton(object sender)
        {
            SelectedForum.Status = "CLOSED";
           // ForumService.UpdateForum(SelectedForum);
        }

        public void ShowForumComments(object param)
        {
            //navigationService.Navigate(new ShowAllForumComments(SelectedForum));
        }
    }
}
