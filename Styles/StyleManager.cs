using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.Styles
{
    public static class StyleManager
    {
        private static ResourceDictionary LightStyle { get; } = new ResourceDictionary { Source = new Uri("/BookingProject;component/Styles/LightStyle.xaml", UriKind.RelativeOrAbsolute) };
        private static ResourceDictionary DarkStyle { get; } = new ResourceDictionary { Source = new Uri("/BookingProject;component/Styles/DarkStyle.xaml", UriKind.RelativeOrAbsolute) };

        public static void ApplyLightStyle()
        {
            Application.Current.Resources.MergedDictionaries.Remove(DarkStyle);
            Application.Current.Resources.MergedDictionaries.Add(LightStyle);
            Application.Current.Resources["MyWindowStyle"] = Application.Current.Resources["LightStyle"];
        }

        public static void ApplyDarkStyle()
        {          
            Application.Current.Resources.MergedDictionaries.Remove(LightStyle);
            Application.Current.Resources.MergedDictionaries.Add(DarkStyle);
            Application.Current.Resources["MyWindowStyle"] = Application.Current.Resources["DarkStyle"];
        }

        public static void RemoveStyles()
        {           
            Application.Current.Resources.MergedDictionaries.Remove(LightStyle);
            Application.Current.Resources.MergedDictionaries.Remove(DarkStyle);
        }
    }
}
