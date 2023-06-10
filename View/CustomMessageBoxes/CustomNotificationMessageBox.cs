using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain;

namespace BookingProject.View.CustomMessageBoxes
{
    public class CustomNotificationMessageBox
    {
        private TourPresenceController _tourPresenceController;
        private UserController _userController;
        private NotificationController _notificationController;
        private CustomMessageBox _customMessageBox;

        public CustomNotificationMessageBox()
        {
            _tourPresenceController = new TourPresenceController();
            _userController = new UserController();
            _notificationController = new NotificationController();
            _customMessageBox = new CustomMessageBox();
        }

        public void ShowCustomMessageBoxNotification(string messageText, Model.User userGuest)
        {
            List<Notification> notifications = _tourPresenceController.GetGuestNotifications(userGuest);

            Window customMessageBox = new Window
            {
                Title = "",
                FontWeight = FontWeights.Bold,
                Height = 200,
                Width = 300,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.Gray,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                AllowsTransparency = true,
            };

            TextBlock message = new TextBlock
            {
                Text = messageText,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20, 20, 20, 0),
                Foreground = Brushes.White
            };

            Button yesButton = new Button
            {
                Content = "Yes",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 10, 10, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightBlue,
                Foreground = Brushes.White,
                Template = GetRoundedButtonTemplate()
            };
            yesButton.Click += (o, args) =>
            {
                _customMessageBox.ShowCustomMessageBox("You have successfully confirmed your presence on the tour.");
                foreach (Notification notification in notifications)
                {
                    if (notification.UserId == userGuest.Id)
                    {
                        _userController.GetById(userGuest.Id).IsPresent = true;
                        _userController.Save();
                        _notificationController.GetById(notification.Id).Read = true;
                        _notificationController.Save();
                    }
                }
                customMessageBox.Close();
            };

            Button noButton = new Button
            {
                Content = "No",
                Width = 80,
                Height = 30,
                Margin = new Thickness(10, 10, 0, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightBlue,
                Foreground = Brushes.White,
                Template = GetRoundedButtonTemplate()
            };
            noButton.Click += (o, args) =>
            {
                _customMessageBox.ShowCustomMessageBox("You have successfully reported that you are not present on the tour.");
                foreach (Notification notification in notifications)
                {
                    if (notification.UserId == userGuest.Id)
                    {
                        _userController.GetById(userGuest.Id).IsPresent = false;
                        _userController.Save();
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

            ControlTemplate GetRoundedButtonTemplate()
            {
                ControlTemplate template = new ControlTemplate(typeof(Button));
                FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
                borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
                borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
                FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
                borderFactory.AppendChild(contentPresenterFactory);
                template.VisualTree = borderFactory;

                Trigger pressedTrigger = new Trigger()
                {
                    Property = Button.IsPressedProperty,
                    Value = true
                };
                pressedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightGray));
                template.Triggers.Add(pressedTrigger);

                return template;
            }

            // Calculate the screen center
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = customMessageBox.Width;
            double windowHeight = customMessageBox.Height;
            customMessageBox.Left = (screenWidth - windowWidth) / 2;
            customMessageBox.Top = (screenHeight - windowHeight) / 2;

            customMessageBox.ShowDialog();
        }
    }
}