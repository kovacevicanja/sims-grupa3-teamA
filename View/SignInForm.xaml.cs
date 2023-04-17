using BookingProject.Domain;
using BookingProject.Controllers;
using BookingProject.Model.Enums;
using BookingProject.View.GuideView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BookingProject.Controller;

namespace BookingProject.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {
        private readonly UserController _controller;
        private readonly AccommodationReservationController _accResController;
        public bool IsSelectedOwner { get; set; }
        public bool IsSelectedGuest1 { get; set; }
        public bool IsSelectedGuest2 { get; set; }
        public bool IsSelectedGuide { get; set; }
        private TourPresenceController _tourPresenceController { get; set; }
        public NotificationController NotificationController { get; set; }

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
            var app = Application.Current as App;
            _controller = app.UserController;
            _accResController = new AccommodationReservationController();
            _tourPresenceController = app.TourPresenceController;
            NotificationController = new NotificationController();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            Model.User user = _controller.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
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
                    }
                    else if(user.UserType == UserType.GUEST1){
                        Model.User guest1 = _controller.GetByUsername(Username);
                        guest1.IsLoggedIn = true;
                        _controller.Save();
                        Guest1View guest1View = new Guest1View();
                        guest1View.Show();

                    }
                    else if (user.UserType == UserType.GUEST2)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        Model.User userGuest = _controller.GetByUsername(Username);
                        _controller.Save();
                        SecondGuestProfile secondGuestProfile = new SecondGuestProfile(userGuest.Id);
                        secondGuestProfile.Show();
                        List<Notification> notifications = _tourPresenceController.GetGuestNotifications(userGuest);
                        List<Notification> notificationsCopy = new List<Notification>();

                        foreach (Notification notification in notifications)
                        {
                            ShowCustomMessageBoxNotification(notification.Text, userGuest);
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

        public void ShowCustomMessageBoxNotification(string messageText, Model.User userGuest)
        {
            List<Notification> notifications = _tourPresenceController.GetGuestNotifications(userGuest);

            Window customMessageBox = new Window
            {
                Title = "Message",
                FontWeight = FontWeights.Bold,
                Height = 200,
                Width = 300,
                WindowStyle = WindowStyle.ThreeDBorderWindow,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.LightBlue,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            TextBlock message = new TextBlock
            {
                Text = messageText,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20, 20, 20, 0)
            };

            Button yesButton = new Button
            {
                Content = "Yes",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 10, 20, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            yesButton.Click += (o, args) =>
            {
                MessageBox.Show("You have successfully confirmed your presence on the tour.");
                foreach (Notification notification in notifications)
                {
                    if (notification.UserId == userGuest.Id)
                    {
                        _controller.GetByID(userGuest.Id).IsPresent = true;
                        _controller.Save();
                        NotificationController.GetByID(notification.Id).Read = true;
                        NotificationController.Save();
                    }
                }
                customMessageBox.Close();
            };

            Button noButton = new Button
            {
                Content = "No",
                Width = 80,
                Height = 30,
                Margin = new Thickness(20, 10, 0, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            noButton.Click += (o, args) =>
            {
                MessageBox.Show("You have successfully reported that you are not present on the tour.");
                foreach (Notification notification in notifications)
                {
                    if (notification.UserId == userGuest.Id)
                    {
                        _controller.GetByID(userGuest.Id).IsPresent = false;
                        _controller.Save();
                    }
                }
                customMessageBox.Close();
            };

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            stackPanel.Children.Add(noButton);
            stackPanel.Children.Add(yesButton);

            StackPanel mainStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            mainStackPanel.Children.Add(message);
            mainStackPanel.Children.Add(stackPanel);

            customMessageBox.Content = mainStackPanel;
            customMessageBox.ShowDialog();
        }
    }
}
