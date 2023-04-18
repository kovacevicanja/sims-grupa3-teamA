using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model.Enums;
using BookingProject.View.GuideView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Domain;
using BookingProject.View.CustomMessageBoxes;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {
        private readonly UserController _controller;
        public bool IsSelectedOwner { get; set; }
        public bool IsSelectedGuest1 { get; set; }
        public bool IsSelectedGuest2 { get; set; }
        public bool IsSelectedGuide { get; set; }
        public static User LoggedInUser { get; set; }
        private TourPresenceController _tourPresenceController { get; set; }
        public NotificationController NotificationController { get; set; }
        private readonly AccommodationReservationController _accResController;
        private readonly RequestAccommodationReservationController _requestController;
        private readonly NotificationController _notificationController;
        public CustomNotificationMessageBox CustomNotificationMessageBox { get; set; }  

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;

            _controller = new UserController();
            _accResController = new AccommodationReservationController();
            _tourPresenceController = new TourPresenceController();
            _accResController = new AccommodationReservationController();
            _requestController = new RequestAccommodationReservationController();
            _notificationController = new NotificationController();
            NotificationController = new NotificationController();
            CustomNotificationMessageBox = new CustomNotificationMessageBox();
        }
        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _controller.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    LoggedInUser = user;
                    if (user.UserType == UserType.OWNER)
                    {
                        Model.User owner = _controller.GetByUsername(Username);
                        owner.IsLoggedIn = true;
                        _controller.Save();
                        OwnerView ownerView = new OwnerView();
                        ownerView.Show();
                        List<Notification> notifications = _accResController.GetOwnerNotifications(owner);
                        List<Notification> notificationsCopy = new List<Notification>();
                        foreach (Notification notification in notifications)
                        {
                            MessageBox.Show(notification.Text);
                            notificationsCopy.Add(notification);
                            _accResController.DeleteNotificationFromCSV(notification);
                        }
                        foreach (Notification notification1 in notificationsCopy)
                        {
                            _accResController.WriteNotificationAgain(notification1);
                        }
                        NotGradedView not_view = new NotGradedView();
                        int row_num = not_view.RowNum();
                        if (row_num > 0)
                        {
                            MessageBox.Show("You have " + row_num.ToString() + " guests to rate");
                        }
                        //OwnersRequestView view = new OwnersRequestView();
                        //view.Show();
                    }
                    else if (user.UserType == UserType.GUEST1)
                    {
                        //_controller.GetByUsername(Username).IsLoggedIn = true;
                        Model.User guest = _controller.GetByUsername(Username);
                        guest.IsLoggedIn = true;
                        _controller.Save();
                        Guest1View guest1View = new Guest1View();
                        guest1View.Show();
                        List<Notification> notifications = new List<Notification>();
                        notifications = _requestController.GetGuest1Notifications(guest);
                        List<Notification> notificationsCopy = new List<Notification>();
                        foreach (Notification notification in notifications)
                        {
                            MessageBox.Show(notification.Text);
                            notificationsCopy.Add(notification);
                            _notificationController.DeleteNotificationFromCSV(notification);
                        }
                        foreach (Notification notification1 in notificationsCopy)
                        {
                            _accResController.WriteNotificationAgain(notification1);
                        }
                    }
                    else if (user.UserType == UserType.GUEST2)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        User userGuest = _controller.GetByUsername(Username);
                        _controller.Save();
                        SecondGuestProfile secondGuestProfile = new SecondGuestProfile(userGuest.Id);
                        secondGuestProfile.Show();
                        List<Notification> notifications = new List<Notification>();
                        notifications = _tourPresenceController.GetGuestNotifications(userGuest);
                        List<Notification> notificationsCopy = new List<Notification>();

                        foreach (Notification notification in notifications)
                        {
                            CustomNotificationMessageBox.ShowCustomMessageBoxNotification(notification.Text, userGuest);
                            notificationsCopy.Add(notification);
                            _tourPresenceController.DeleteNotificationFromCSV(notification);
                        }
                        foreach (Notification notification1 in notificationsCopy)
                        {
                            _tourPresenceController.WriteNotificationAgain(notification1);
                        }
                    }
                    else if (user.UserType == UserType.GUIDE)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        _controller.Save();
                        GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
                        guideHomeWindow.Show();
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }
    }
}