using BookingProject.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace BookingProject.View.CustomMessageBoxes
{
    public class CustomMessageBoxComplexTourRequests
    {
        public CustomMessageBoxComplexTourRequests() 
        { 
        
        }
        public void ShowCustomMessageBoxComplexTourRequests(string messageText, out bool flag)
        {
            flag = false;

            bool insideFlag = false;

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

            Button continueButton = new Button
            {
                Content = "Continue",
                Width = 80,
                Height = 30,
                Margin = new Thickness(0, 10, 10, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightBlue,
                Foreground = Brushes.White,
                Template = GetRoundedButtonTemplate()
            };
            continueButton.Click += (o, args) =>
            {
                insideFlag = true;
                // Continue with adding tour requests
                customMessageBox.Close();
                // Add your logic here to handle the "Continue" button click
            };

            Button finishButton = new Button
            {
                Content = "Finish",
                Width = 80,
                Height = 30,
                Margin = new Thickness(10, 10, 0, 0),
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightBlue,
                Foreground = Brushes.White,
                Template = GetRoundedButtonTemplate()
            };
            finishButton.Click += (o, args) =>
            {
                insideFlag = false;
                // Finish creating complex tour request
                customMessageBox.Close();
                // Add your logic here to handle the "Finish" button click
            };

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            stackPanel.Children.Add(continueButton);
            stackPanel.Children.Add(finishButton);

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

            flag = insideFlag;
        }
    }
}