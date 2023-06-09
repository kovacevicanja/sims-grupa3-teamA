using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Styles
{
    public class MessagingService
    {
        public event EventHandler<ChangeModeMessage> ModeChanged;

        public void PublishModeChange(bool isDarkMode)
        {
            ModeChanged?.Invoke(this, new ChangeModeMessage(isDarkMode));
        }
    }
}
