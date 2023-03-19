using BookingProject.Controller;
using BookingProject.Model;
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
            _controller = new UserController();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _controller.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    if (IsSelectedOwner)
                    {
                        OwnerView ownerView = new OwnerView();
                        ownerView.Show();
                        NotGradedView not_view = new NotGradedView();
                        int row_num = not_view.RowNum();
                        if(row_num > 0)
                        {
                            MessageBox.Show("You have " + row_num.ToString() + " guests to rate");
                        }
                    }
                    else if(IsSelectedGuest1){
                        Guest1View guest1View = new Guest1View();
                        guest1View.Show();
                        
                    }else if(IsSelectedGuest2)
                    {
                        //MessageBox.Show("You have successfully logged in as second guest!");
                        SecondGuestView secondGuestView = new SecondGuestView();
                        secondGuestView.Show();
                    }else if (IsSelectedGuide)
                    {
                        TourCreationView tourCreationView = new TourCreationView();
                        tourCreationView.Show();
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
