using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

namespace Workoutisten.FitStreak.Services
{
    public class PushNotificationManager : IPushNotificationManager
    {
        public PushNotificationManager() { }

        NotificationRequest _OptionalNotification = new NotificationRequest
        {
            BadgeNumber = 1,
            Description = "This is your reminder to stay on your workout schedule.\n Your body will appreciate it, and even more importantly your streak will appreciate it!",
            Title = "Hey! Time to work out!",
            NotificationId = 1,
#if ANDROID
            Android = new AndroidOptions
            {
                Priority = AndroidPriority.High
            }
#endif
        };

        NotificationRequest _StreakNotification12HoursBefore = new NotificationRequest
        {
            BadgeNumber = 1,
            Description = "You are only 12 hours away from losing your Streak, better sooner than later!",
            Title = "Your Streak is running out!",
            NotificationId = 1,
#if ANDROID
            Android = new AndroidOptions
            {
                Priority = AndroidPriority.High
            }
#endif
        };

        NotificationRequest _StreakNotification2HoursBefore = new NotificationRequest
        {
            BadgeNumber = 1,
            Description = "You are only 2 hours away from  losing your Streak, this is not a test, I repeat, this is not a test! You are about to lose your streak!",
            Title = "Your Streak is running out!",
            NotificationId = 1,
#if ANDROID
            Android = new AndroidOptions
            {
                Priority = AndroidPriority.Max
            }
#endif
        };


        public void SetDailySchedule(System.TimeSpan notificationRepeatTime)
        {

            _OptionalNotification.Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0).Add(notificationRepeatTime),
                //NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).Add(new TimeSpan(0,0,5)),
                RepeatType = NotificationRepeat.TimeInterval,
                NotifyRepeatInterval = notificationRepeatTime
            };
            ShowNotification(_OptionalNotification);

        }

        public void SetWeekdaySchedule(bool[] weekdays)
        {
            //TODO: Wie wollen wir das machen?
            //#if ANDROID || IOS
            //   _Notification.Schedule = new NotificationRequestSchedule()
            //    {
            //        DateTime.Now.Da

            //    NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0).Add(notificationRepeatTime.Value),
            //    RepeatType = NotificationRepeat.TimeInterval,
            //    NotifyRepeatInterval = notificationRepeatTime
            //    }
            //    };
            //ShowNotification(_OptionalNotification);
            //#endif
        }

        public void ResetStreakNotification()
        {
            _StreakNotification12HoursBefore.Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateTime.Now.Add(new TimeSpan(60, 0, 0)),
                RepeatType = NotificationRepeat.No
            };

            _StreakNotification2HoursBefore.Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateTime.Now.Add(new TimeSpan(70, 0, 0)),
                RepeatType = NotificationRepeat.No
            };
            ShowNotification(_StreakNotification12HoursBefore);
            ShowNotification(_StreakNotification2HoursBefore);
        }

        private void ShowNotification(NotificationRequest notificationToShow)
        {
#if ANDROID
            LocalNotificationCenter.Current.Show(notificationToShow);
#endif
        }
    }
}
