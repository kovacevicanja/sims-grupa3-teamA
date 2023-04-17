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
    public class CustomMessageBox
    {
        public void ShowCustomMessageBox(string messageText)
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

            Button okButton = new Button
            {
                Content = "OK",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 10, 0, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            okButton.Click += (o, args) =>
            {
                customMessageBox.Close();
            };
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            stackPanel.Children.Add(message);
            stackPanel.Children.Add(okButton);

            customMessageBox.Content = stackPanel;
            customMessageBox.ShowDialog();
        }

        /*
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
        */
    }
}
