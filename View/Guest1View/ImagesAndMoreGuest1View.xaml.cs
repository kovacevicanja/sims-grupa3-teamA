using BookingProject.Model;
using BookingProject.View.Guest1ViewModel;
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

namespace BookingProject.View.Guest1View
{
	public partial class ImagesAndMoreGuest1View : Window
	{
		public ImagesAndMoreGuest1View(Accommodation selectedAccommodation)
		{
			InitializeComponent();
			this.DataContext = new ImagesAndMoreGuest1ViewModel(selectedAccommodation);
		}
	}
}
