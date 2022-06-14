using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Timers;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Services;
using TimeSpan = System.TimeSpan;
using Timer = System.Timers.Timer;
using Workoutisten.FitStreak.Data.Models.User;
using Workoutisten.FitStreak.Data.Converter;

namespace Workoutisten.FitStreak.Pages
{
    public partial class Homescreen
    {
        #region Properties
        [Inject]
        public IRestClient _RestClient { get; set; }

        [Inject]
        public ErrorDialogService _ErrorDialogService { get; set; }

        [Inject]
        public IConverterWrapper _Converter { get; set; }

        [Inject] 
        public NavigationManager _NavigationManager { get; set; } 

        double[] Counts { get; set; } = new double[] { 0, 100 };

        ChartOptions chartOptions = new ChartOptions()
        {
            DisableLegend = true,
            ChartPalette = new string[] { "#EB5E55", "#3D4151" },
            LineStrokeWidth = 1
        };

        string? remainingTimeString { get; set; } = null;
        double elapsedPercent { get; set; } = 0;
        double remainingPercent { get; set; } = 100;

        Timer Timer { get; set; }

        DateTime LastWorkoutDate { get; set; } = DateTime.MinValue;

        TimeSpan MaxTimeSpan { get; set; } = new TimeSpan(3, 0, 0, 0);

        UserModel CurrentUser { get; set; } = new UserModel() { FirstName = String.Empty, Streak = 0 };

        #endregion

        #region Methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    var userId = Guid.Parse(await SecureStorage.GetAsync("userId"));
                    CurrentUser = await _Converter.ToEntity<User, UserModel>(await _RestClient.CallControlled(c => c.GetUserAsync(userId)));
                    LastWorkoutDate = CurrentUser.LastExercise;
                    if (LastWorkoutDate >= (DateTime.Now - MaxTimeSpan))
                    {
                        StartTimer();
                    }
                    else
                    {
                        CurrentUser.Streak = 0;
                    }
                    
                }
                catch (ApiException e)
                {
                    //await _ErrorDialogService.ShowErrorDialog(e.StatusCode.ToString(), e.Result.Detail);
                    _NavigationManager.NavigateTo("/welcome");
                }
                catch (Exception e)
                {
                    await _ErrorDialogService.ShowErrorDialog();
                }

                StateHasChanged();
            }
            base.OnAfterRenderAsync(firstRender);
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            await InvokeAsync(() =>
            {

                DateTime currentTime = e.SignalTime.ToUniversalTime();
                var remainingTime = MaxTimeSpan - currentTime.Subtract(LastWorkoutDate);
                if (remainingTime.TotalSeconds <= 0)
                {
                    Timer.Stop();
                    CurrentUser.Streak = 0;
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