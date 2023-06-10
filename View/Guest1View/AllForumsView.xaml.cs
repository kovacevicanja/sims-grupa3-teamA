using BookingProject.View.Guest1View.Tutorials;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace BookingProject.View.Guest1View
{
	public partial class AllForumsView : Window
	{
            public ObservableCollection<CommentItem> Comments { get; set; }

            public AllForumsView()
            {
                InitializeComponent();
                Comments = new ObservableCollection<CommentItem>
            {
                new CommentItem { Location = "Novi Sad, Serbia", Comment1 = "Milica's comment: dobro", Comment2 = "Kristina's comment: odlicno" },
                new CommentItem { Location = "Beograd, Serbia", Comment1 = "Milica's comment: lose"},
                new CommentItem { Location = "Paris, France", Comment1 = "Milica's comment: odlicno", Comment2 = "Kristina's comment: super" }
            };

                ListViewComments.ItemsSource = Comments;
            }


        private void LeaveComment_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBoxComment = (TextBox)FindName("TextBoxComment");

            var button = (Button)sender;
            var commentItem = (CommentItem)button.DataContext;
            commentItem.Comments.Add(textBoxComment.Text);
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(AllForumsView)) { window.Close(); }
            }
        }

        private void Button_Click_Tutorial(object sender, RoutedEventArgs e)
		{
            var tutorial = new ForumTutorialsView();
            tutorial.Show();
            CloseWindow();
		}

		private void Button_Click_Homepage(object sender, RoutedEventArgs e)
		{
            var homepage = new Guest1HomepageView();
            homepage.Show();
            CloseWindow();
		}

		private void Button_Click_MyReservations(object sender, RoutedEventArgs e)
		{
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            CloseWindow();
        }

		private void Button_Click_CreateForum(object sender, RoutedEventArgs e)
		{
            var forum = new OpenForumView();
            forum.Show();
            CloseWindow();
        }

		private void Button_Click_QuickSearch(object sender, RoutedEventArgs e)
		{
            var quickS = new QuickSearchView();
            quickS.Show();
            CloseWindow();
        }

		private void Button_Click_MyProfile(object sender, RoutedEventArgs e)
		{
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }

		private void Button_Click_Logout(object sender, RoutedEventArgs e)
		{
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }

        private void Button_Click_MyReviews(object sender, RoutedEventArgs e)
		{
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }
	}

	public class CommentItem
        {
            public string Location { get; set; }
            public string Comment1 { get; set; }
            public string Comment2 { get; set; }
            public ObservableCollection<string> Comments { get; set; }

            public CommentItem()
            {
                Comments = new ObservableCollection<string>();
            }
        }

    }

