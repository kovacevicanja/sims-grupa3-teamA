using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.View.OwnersViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View.OwnersView
{
    /// <summary>
    /// Interaction logic for OneForumView.xaml
    /// </summary>
    public partial class OneForumView : Page
    {
        public ForumCommentController ForumCommentController { get; set; }
        public OwnerNotificationCustomBox box { get; set; }
        public OneForumView(Forum selectedForum, NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new OneForumViewModel(selectedForum, navigationService);
            ForumCommentController = new ForumCommentController();
            box = new OwnerNotificationCustomBox();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var comment = ((Button)sender).DataContext as ForumComment;
            comment.NumberOfReports++;
            ForumCommentController.Update(comment);
            box.ShowCustomMessageBox("You have reported a comment!");
        }
    }
}
