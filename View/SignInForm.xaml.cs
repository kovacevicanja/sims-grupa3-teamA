using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _controller.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    if (user.UserType==UserType.OWNER)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        OwnerView ownerView = new OwnerView();
                        ownerView.Show();
                        NotGradedView not_view = new NotGradedView();
                        int row_num = not_view.RowNum();
                        if(row_num > 0)
                        {
                            MessageBox.Show("You have " + row_num.ToString() + " guests to rate");
                        }
                    }
                    else if(user.UserType == UserType.GUEST1){
                        User guest1 = _controller.GetByUsername(Username);
                        guest1.IsLoggedIn = true;
                        _controller.Save();
                        Guest1View guest1View = new Guest1View();
                        guest1View.Show();
                        
                    }else if(user.UserType == UserType.GUEST2)
                    {
                        //MessageBox.Show("You have successfully logged in as second guest!");
                        //SecondGuestView secondGuestView = new SecondGuestView();
                        //secondGuestView.Show();
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        SecondGuestProfile secondGuestProfile = new SecondGuestProfile();
                        secondGuestProfile.Show();
                    }else if (user.UserType == UserType.GUIDE)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
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
