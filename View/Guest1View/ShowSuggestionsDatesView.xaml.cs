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
using static BookingProject.View.Guest1ViewModel.QuickSearchViewModel;

namespace BookingProject.View.Guest1View
{
	public partial class ShowSuggestionsDatesView : Window
	{
		public ShowSuggestionsDatesView(AccommodationDTO dto)
		{ 
			InitializeComponent();
			this.DataContext = new ShowSuggestionsDatesViewModel(dto);
		}
	}
}
