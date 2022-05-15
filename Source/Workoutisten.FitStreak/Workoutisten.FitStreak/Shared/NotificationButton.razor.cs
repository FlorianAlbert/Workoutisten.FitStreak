using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Workoutisten.FitStreak;
using Workoutisten.FitStreak.Shared;
using Workoutisten.FitStreak.Data.Enums;
using Workoutisten.FitStreak.Data.Models;
using MudBlazor;

namespace Workoutisten.FitStreak.Shared
{
    public partial class NotificationButton : MudComponentBase
    {
        private bool _newNotificationsAvailable = false;
        private IDictionary<string, bool> _messages = new Dictionary<string, bool>();

        private DateTime notificationTime = new DateTime(2022, 5, 12, 20, 51, 0);
        private async Task MarkNotificationAsRead()
        {
            foreach (var key in _messages.Keys.ToList())
            {
                _messages[key] = true;
            }
            _newNotificationsAvailable = false;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _messages = new Dictionary<string, bool>() {
                {"test", true },
                {"Notification 1", false },
                {"Notification 2", false }
            };
            _newNotificationsAvailable = true;
        }
    }
}