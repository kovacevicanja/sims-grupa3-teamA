namespace BookingProject.View.Guest2ViewModel
{
    internal class ValidationContext
    {
        private CreateTourRequestViewModel createTourRequestViewModel;
        private object value1;
        private object value2;

        public ValidationContext(CreateTourRequestViewModel createTourRequestViewModel, object value1, object value2)
        {
            this.createTourRequestViewModel = createTourRequestViewModel;
            this.value1 = value1;
            this.value2 = value2;
        }

        public string MemberName { get; set; }
    }
}