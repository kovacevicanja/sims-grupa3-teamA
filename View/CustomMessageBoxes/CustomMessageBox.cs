using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Markup;

namespace BookingProject.View.CustomMessageBoxes
{
    public class CustomMessageBox
    {
        public void ShowCustomMessageBox(string messageText)
        {
            Window customMessageBox = new Window
            {
                Title = "Message",
                Height = 200,
                Width = 300,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.Gray
            };

            TextBlock message = new TextBlock
            {
                Text = messageText,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
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
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightBlue,
                Foreground = Brushes.White,
                Template = GetRoundedButtonTemplate()
            };
            okButton.Click += (o, args) =>
            {
                customMessageBox.Close();
            };

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.Gray
            };
            stackPanel.Children.Add(message);
            stackPanel.Children.Add(okButton);

            customMessageBox.Content = stackPanel;

            // Calculate the screen center
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = customMessageBox.Width;
            double windowHeight = customMessageBox.Height;
            customMessageBox.Left = (screenWidth - windowWidth) / 2;
            customMessageBox.Top = (screenHeight - windowHeight) / 2;

            customMessageBox.ShowDialog();

            ControlTemplate GetRoundedButtonTemplate()
            {
                ControlTemplate template = new ControlTemplate(typeof(Button));
                FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
                borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
                borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));
                FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center); // Align content in the center
                contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center); // Align content in the center
                borderFactory.AppendChild(contentPresenterFactory);
                template.VisualTree = borderFactory;

                // Define trigger to change the button color when pressed
                Trigger pressedTrigger = new Trigger()
                {
                    Property = Button.IsPressedProperty,
                    Value = true
                };
                pressedTrigger.Setters.Add(new Setter(Border.BackgroundProperty, Brushes.LightGray));
                template.Triggers.Add(pressedTrigger);

                return template;
            }
        }
    }
}