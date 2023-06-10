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
using System.Windows.Shapes;

namespace BookingProject.View.Guest1View.Tutorials
{
	public partial class MyProfileTutorialView : Window
	{
		public TimeSpan currentPosition;
		public MyProfileTutorialView()
		{
			InitializeComponent();
			this.DataContext = this;
			currentPosition = videoPlayer.Position;
			videoPlayer.Source = new Uri("C:\\Users\\pmili\\OneDrive\\Desktop\\KOPIJA PROJEKTA ZA SVAKI SLUCAJ\\Resources\\TutorialsGuest1\\mojProfil.mp4", UriKind.RelativeOrAbsolute);
			videoPlayer.LoadedBehavior = MediaState.Manual;
			videoPlayer.UnloadedBehavior = MediaState.Manual;
		}

		private void CloseWindow()
		{
			foreach (Window window in App.Current.Windows)
			{
				if (window.GetType() == typeof(MyProfileTutorialView)) { window.Close(); }
			}
		}

		private void Button_Click_Play(object sender, RoutedEventArgs e)
		{
			currentPosition = videoPlayer.Position;
			videoPlayer.Play();
		}

		private void Button_Click_Pause(object sender, RoutedEventArgs e)
		{
			currentPosition = videoPlayer.Position;
			videoPlayer.Pause();
		}

		private void Button_Click_Continue(object sender, RoutedEventArgs e)
		{
			videoPlayer.Position = currentPosition;
			videoPlayer.Play();
		}

		private void Button_Click_PreviousPage(object sender, RoutedEventArgs e)
		{
			var previousPage = new Guest1ProfileView();
			previousPage.Show();
			CloseWindow();
		}
		private void Button_Click_MyReservations(object sender, RoutedEventArgs e)
		{
			var res = new Guest1Reservations();
			res.Show();
			CloseWindow();
		}
		private void Button_Click_Homepage(object sender, RoutedEventArgs e)
		{
			var ghp = new Guest1HomepageView();
			ghp.Show();
			CloseWindow();
		}

		private void Button_Click_Logout(object sender, RoutedEventArgs e)
		{
			SignInForm signInForm = new SignInForm();
			signInForm.Show();
			CloseWindow();
		}

		private void Button_Click_MyReviews(object sender, RoutedEventArgs e)
		{
			var reviews = new Guest1ReviewsView();
			reviews.Show();
			CloseWindow();
		}
		private void Button_Click_MyProfile(object sender, RoutedEventArgs e)
		{
			var profile = new Guest1ProfileView();
			profile.Show();
			CloseWindow();
		}
		private void Button_Click_OpenForum(object sender, RoutedEventArgs e)
		{
			var forum = new OpenForumView();
			forum.Show();
			CloseWindow();
		}

		private void Button_Click_Quick_Search(object sender, RoutedEventArgs e)
		{
			var quickS = new QuickSearchView();
			quickS.Show();
			CloseWindow();
		}
	}
}
