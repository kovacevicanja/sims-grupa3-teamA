using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookingProject.View.Guest1ViewModel.QuickSearchViewModel;

namespace BookingProject.View.Guest1ViewModel
{
	public class QuickSearchSuggestionsViewModel
	{
		private List<AccommodationDTO> DTOs { get; set; }
		public QuickSearchSuggestionsViewModel(List<AccommodationDTO> dtos)
		{
			DTOs = dtos;
		}

		
	}
}
