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
            _tourPresenceController = app.TourPresenceController;
            NotificationController = new NotificationController();
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
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        OwnerView ownerView = new OwnerView();
                        ownerView.Show();
                        NotGradedView not_view = new NotGradedView();
                        int row_num = not_view.RowNum();
                        if (row_num > 0)
                        {
                            MessageBox.Show("You have " + row_num.ToString() + " guests to rate");
                        }
                        //GuestGradesForOwnerView view = new GuestGradesForOwnerView();
                        //view.Show();
                    }
                    else if (user.UserType == UserType.GUEST1)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        Guest1View guest1View = new Guest1View();
                        guest1View.Show();

                    }
                    else if (user.UserType == UserType.GUEST2)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        User userGuest = _controller.GetByUsername(Username);
                        _controller.Save();
                        SecondGuestProfile secondGuestProfile = new SecondGuestProfile(userGuest.Id);
                        secondGuestProfile.Show();
                        List<Notification> notifications = _tourPresenceController.GetGuestNotifications(userGuest);
                        List<Notification> notificationsCopy = new List<Notification>();

                        foreach (Notification n in notifications)
                        {
                            if (n.UserId == user.Id)
                            {
                                _controller.GetByID(user.Id).IsPresent = true; //dodaj dugme za sredjivanje
                                _controller.Save();
                                NotificationController.GetByID(n.Id).Read = true;
                                NotificationController.Save();
                            }
                        }

                        notifications = _tourPresenceController.GetGuestNotifications(userGuest);

                        foreach (Notification notification in notifications)
                        {
                            ShowCustomMessageBoxNotification(notification.Text);
                            notificationsCopy.Add(notification);
                            _tourPresenceController.GetGuestNotifications(userGuest);
                        }
                        foreach (Notification notification1 in notificationsCopy)
                        {
                            _tourPresenceController.GetGuestNotifications(userGuest);
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

        public void ShowCustomMessageBoxNotification(string messageText)
        {
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
                // Perform action for 'Yes' button
                MessageBox.Show("Action for 'Yes' button clicked!");
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
                // Perform action for 'No' button
                MessageBox.Show("Action for 'No' button clicked!");
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
