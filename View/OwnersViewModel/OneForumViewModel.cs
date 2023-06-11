using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Windows.Navigation;

namespace BookingProject.View.OwnersViewModel
{
    public class OneForumViewModel
    {
        public Forum Forum { get; set; }
        public NavigationService NavigationService { get; set; }
        public ObservableCollection<ForumComment> Comments { get; set; }
        public ForumController ForumController { get; set; }
        public ForumCommentController CommentController { get; set; }
        public UserController UserController { get; set; }
        public RelayCommand AddCommand { get; set; }
        public OwnerNotificationCustomBox OwnerNotificationCustomBox { get; set; }
        public RelayCommand BackCommand
        {
            get; set;
        }
        public OneForumViewModel(Forum forum, NavigationService navigationService) 
        {
            Forum = forum;
            NavigationService = navigationService;
            ForumController = new ForumController();
            CommentController = new ForumCommentController();
            Comments = new ObservableCollection<ForumComment>(CommentController.GetAllForForum(forum.Id));
            UserController= new UserController();
            AddCommand = new RelayCommand(Button_Click_Add, CanExecute);
            OwnerNotificationCustomBox= new OwnerNotificationCustomBox();
            BackCommand = new RelayCommand(Button_Back, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Click_Add(object param)
        {
            if (Text.IsEmpty())
            {
                OwnerNotificationCustomBox.ShowCustomMessageBox("You have to add a text!");
                return;
            }
            ForumComment forumComment = new ForumComment();
            forumComment.Text = Text;
            forumComment.Forum = Forum;
            forumComment.User = UserController.GetLoggedUser();
            forumComment.IsOwners = true;
            forumComment.IsGuests = false;
            forumComment.IsInvalid = false;
            forumComment.NumberOfReports = 0;
            int countGuest = 0;
            int countOwner = 0;
            foreach(ForumComment comm in CommentController.GetAllForForum(Forum.Id))
            {
                if (comm.IsGuests && !comm.IsInvalid)
                {
                    countGuest++;
                }
                if (comm.IsOwners)
                {
                    countOwner++;
                }
            }
            CommentController.Create(forumComment);
            this.Comments.Add(forumComment);
            if (countGuest>19 && countOwner > 9)
            {
                Forum.IsUseful = true;
                Forum.Comments.Add(forumComment);
                ForumController.Update(Forum);
            }

           
        }
        private void Button_Back(object param)
        {
            NavigationService.GoBack();
        }
        private string _comment;
        public string Text
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
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
