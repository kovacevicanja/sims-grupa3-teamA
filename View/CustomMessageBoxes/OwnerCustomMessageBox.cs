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
    public class OwnerCustomMessageBox
    {
        public void ShowCustomMessageBox(string messageText)
        {
            Window customMessageBox = new Window
            {
                Title = "Message",
                FontWeight = FontWeights.Bold,
                Height = 300,
                Width = 280,
                WindowStyle = WindowStyle.ThreeDBorderWindow,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.SteelBlue,
                Left = SystemParameters.WorkArea.Left + 611,
                Top = SystemParameters.WorkArea.Top + 260
            };

            TextBlock message = new TextBlock
            {
                Text = messageText,
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
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
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(3),
                Background = Brushes.LightBlue,
                Foreground = Brushes.SteelBlue,
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
                HorizontalAlignment = HorizontalAlignment.Center
            };
            stackPanel.Children.Add(message);
            stackPanel.Children.Add(okButton);

            customMessageBox.Content = stackPanel;
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
