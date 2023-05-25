using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingProject.ConversionHelp
{
    public static class TextBoxClearHelper
    {
        public static void ClearTextBox(object param)
        {
            if (param is TextBox textBox)
            {
                textBox.Text = string.Empty;
            }
        }
    }
}
