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
using Workoutisten.FitStreak.Shared.PageExclusives.HomescreenPage;
using ApexCharts;
using System.Timers;

namespace Workoutisten.FitStreak.Pages
{
    public partial class Homescreen
    {
        public double[] Counts { get; set; } = new double[] { 0, 100 };
        public ChartOptions chartOptions = new ChartOptions()
        { DisableLegend = true, ChartPalette = new string[] { "#EB5E55", "#3D4151" }, LineStrokeWidth = 1 };
        CustomAuthenticationStateProvider customAuthenticationStateProvider = new CustomAuthenticationStateProvider();
        
        string remainingTimeString = "00:00:00";
        double elapsedPercent = 0;
        double remainingPercent = 100;

        System.Timers.Timer timer;
        DateTime lastWorkoutDate = new DateTime(2022, 5, 12, 20, 51, 0);
        TimeSpan maxTimeSpan = new TimeSpan(3, 0, 0, 0);
        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            await InvokeAsync(() =>
            {
                DateTime currentTime = e.SignalTime;
                var remainingTime = maxTimeSpan - currentTime.Subtract(lastWorkoutDate);
                remainingPercent = (remainingTime.TotalSeconds/maxTimeSpan.TotalSeconds)*100;
                elapsedPercent = 100 - remainingPercent;
                Counts = new double[] { elapsedPercent, remainingPercent };
                remainingTimeString = $"{remainingTime:dd\\:hh\\:mm\\:ss}";
                StateHasChanged();
            });
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            StartTimer();
            var data = customAuthenticationStateProvider.GetAuthenticationStateAsync();
        }

        void StartTimer()
        {
            lastWorkoutDate = DateTime.Now;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

    }
}