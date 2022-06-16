using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Services
{
    public interface IPushNotificationManager
    {
        public void SetDailySchedule(TimeSpan timeSpan);

        public void SetWeekdaySchedule(bool[] weekdays);

        public void ResetStreakNotification();

    }
}
