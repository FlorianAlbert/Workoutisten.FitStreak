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
            Description = "Hey! Time to work out!",
            Title = "This is your reminder to stay on your workout schedule.\n Your body will appreciate it, and even more importantly your streak will appreciate it! ",
            NotificationId = 1,
            Android = new AndroidOptions
            {
                Priority = AndroidPriority.High
            }
        };

        NotificationRequest _StreakNotification12HoursBefore = new NotificationRequest
        {
            BadgeNumber = 1,
            Description = "Your Streak is running out!",
            Title = "You are only 12 hours away from losing your Streak, better sooner than later!",
            NotificationId = 1,
            Android = new AndroidOptions
            {
                Priority = AndroidPriority.High
            }
        };

        NotificationRequest _StreakNotification2HoursBefore = new NotificationRequest
        {
            BadgeNumber = 1,
            Description = "Your Streak is running out!",
            Title = "You are only 2 hours away from  losing your Streak, this is not a test, I repeat, this is not a test! You are about to lose your streak!",
            NotificationId = 1,
            Android = new AndroidOptions
            {
                Priority = AndroidPriority.Max
            }
        };


        public void SetDailySchedule(System.TimeSpan notificationRepeatTime)
        {
#if ANDROID

            _OptionalNotification.Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0).Add(notificationRepeatTime),
                RepeatType = NotificationRepeat.TimeInterval,
                NotifyRepeatInterval = notificationRepeatTime
            };
            ShowNotification(_OptionalNotification);
#endif

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
            NotificationCenter.Current.Show(notificationToShow);
#endif
        }
    }
}
