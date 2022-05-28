using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Timers;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Services;
using TimeSpan = System.TimeSpan;
using Timer = System.Timers.Timer;

namespace Workoutisten.FitStreak.Pages
{
    public partial class Homescreen
    {
        #region Properties
        [Inject]
        public IRestClient _RestClient { get; set; }

        [Inject]
        public ErrorDialogService _ErrorDialogService { get; set; }

        double[] Counts { get; set; } = new double[] { 0, 100 };

        ChartOptions chartOptions = new ChartOptions()
        {
            DisableLegend = true,
            ChartPalette = new string[] { "#EB5E55", "#3D4151" },
            LineStrokeWidth = 1
        };

        CustomAuthenticationStateProvider customAuthenticationStateProvider = new CustomAuthenticationStateProvider();

        string? remainingTimeString { get; set; } = null;
        double elapsedPercent { get; set; } = 0;
        double remainingPercent { get; set; } = 100;

        Timer Timer { get; set; }

        DateTime LastWorkoutDate { get; set; }

        TimeSpan MaxTimeSpan { get; set; } = new TimeSpan(3, 0, 0, 0);

        User CurrentUser { get; set; }

        #endregion

        #region Methods

        protected async override void OnInitialized()
        {
            try
            {
                var test = Guid.Parse(await SecureStorage.GetAsync("userId"));
                CurrentUser = await _RestClient.GetUserAsync(Guid.Parse(await SecureStorage.GetAsync("userId")));
                LastWorkoutDate = CurrentUser.LastExercise;

            }
            catch (ApiException<ProblemDetails> e)
            {
                //if(e.StatusCode == 404)
            }
            catch (Exception e)
            {
                //Navig
            }


            if (LastWorkoutDate >= (DateTime.Now - MaxTimeSpan))
            {
                StartTimer();
            }
            else
            {
                CurrentUser.ExerciseStreak = 0;
                //_RestClient.UpdateUserAsync()
            }
            base.OnInitialized();
            StateHasChanged();
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            await InvokeAsync(() =>
            {
                
                DateTime currentTime = e.SignalTime;
                var remainingTime = MaxTimeSpan - currentTime.Subtract(LastWorkoutDate);
                if (remainingTime.TotalSeconds <= 0)
                {
                    Timer.Stop();
                    CurrentUser.ExerciseStreak = 0;
                    remainingTimeString = null;
                    return;
                }
                remainingPercent = (remainingTime.TotalSeconds / MaxTimeSpan.TotalSeconds) * 100;
                elapsedPercent = 100 - remainingPercent;
                Counts = new double[] { remainingPercent, elapsedPercent };
                remainingTimeString = $"{remainingTime:dd\\:hh\\:mm\\:ss}";
                StateHasChanged();
            });
        }

        void StartTimer()
        {
                Timer = new Timer(1000);
                Timer.Elapsed += OnTimedEvent;
                Timer.AutoReset = true;
                Timer.Enabled = true;
        }

        #endregion
    }
}