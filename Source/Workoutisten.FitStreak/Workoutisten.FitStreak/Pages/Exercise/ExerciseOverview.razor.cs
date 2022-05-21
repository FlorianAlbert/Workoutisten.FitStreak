using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics;
using System.Timers;
using Workoutisten.FitStreak.Data.Converter;
using Workoutisten.FitStreak.Data.Enums;
using Workoutisten.FitStreak.Data.Models.Workout;
using Workoutisten.FitStreak.Pages.Dialogs;

namespace Workoutisten.FitStreak.Pages.Exercise
{
    public partial class ExerciseOverview
    {
        #region Parameters
        [Parameter]
        [SupplyParameterFromQuery(Name = "exercise")]
        public Guid[]? ExerciseGuids { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "workout")]
        public Guid? WorkoutGuid { get; set; }

        //[Parameter]
        //[SupplyParameterFromQuery(Name = "workoutname")]
        public string? WorkoutName { get; set; }
        #endregion

        #region Properties
        ExerciseModel CurrentExercise { get; set; }

        List<ExerciseModel> ExerciseList { get; set; } = new();

        Dictionary<ExerciseModel, List<StrengthExerciseSetModel>> StrengthExerciseSets { get; set; } = new();
        Dictionary<ExerciseModel, List<CardioExerciseSetModel>> CardioExerciseSets { get; set; } = new();

        StrengthExerciseSetModel NewStrengthExerciseSetModel { get; set; } = new();

        CardioExerciseSetModel NewCardioExerciseSetModel { get; set; } = new();

        TimeSpan TimerValue { get; set; } = new TimeSpan(0, 2, 0);

        TimeSpan? TimerValueBackup { get; set; }
        
        System.Timers.Timer _Timer { get; set; } = new System.Timers.Timer(1000);

        Stopwatch _Stopwatch { get; set; } = new();

        bool _TimerIconToggle { get; set; }
        #endregion

        #region Methods
        protected override void OnInitialized()
        {
            //Exercises anhand der ExerciseGuids oder der WorkoutGuid holen

            #region ToDelete
            WorkoutName = "Workout 1 awdawdawdasdas";

            var ex1 = new ExerciseModel() { Name = "Exercise 1", Category = ExerciseCategoryEnum.Strength };
            CurrentExercise = ex1;
            var ex2 = new ExerciseModel() { Name = "Exercise 2", Category = ExerciseCategoryEnum.Strength };
            var ex3 = new ExerciseModel() { Name = "Exercise 3", Category = ExerciseCategoryEnum.Cardio };
            var ex4 = new ExerciseModel() { Name = "Exercise 4", Category = ExerciseCategoryEnum.Cardio };

            ExerciseList.Add(ex1);
            ExerciseList.Add(ex2);
            ExerciseList.Add(ex3);
            ExerciseList.Add(ex4);

            StrengthExerciseSets.Add(ex1, new List<StrengthExerciseSetModel>() { /*new StrengthExerciseSetModel() { SetNumber = 1, Reps = 12, Weight = 10 }, new StrengthExerciseSetModel() { SetNumber = 2, Reps = 12, Weight = 10 }*/ });
            StrengthExerciseSets.Add(ex2, new List<StrengthExerciseSetModel>() { new StrengthExerciseSetModel() { SetNumber = 1, Reps = 12, Weight = 10 }, new StrengthExerciseSetModel() { SetNumber = 2, Reps = 12, Weight = 10 }, new StrengthExerciseSetModel() { SetNumber = 3, Reps = 12, Weight = 10 } });
            CardioExerciseSets.Add(ex3, new List<CardioExerciseSetModel>() { new CardioExerciseSetModel() { SetNumber = 1, Duration = new TimeSpan(0, 1, 0), Distance = 10 } });
            CardioExerciseSets.Add(ex4, new List<CardioExerciseSetModel>() { /*new CardioExerciseSetModel() { SetNumber = 1, Reps = 12, Weight = 10 }*/ });

            //Exercises.
            #endregion

            base.OnInitialized();
        }

        void CancelExercise()
        {
            //To be done
        }

        void CompleteExercise()
        {
            //To be done
        }

        void GetNextExercise()
        {
            var index = ExerciseList.IndexOf(CurrentExercise);
            if (index < ExerciseList.Count() - 1)
            {
                CurrentExercise = ExerciseList[++index];
            }
        }

        void GetPreviousExercise()
        {
            var index = ExerciseList.IndexOf(CurrentExercise);
            if (index > 0)
            {
                CurrentExercise = ExerciseList[--index];
            }
        }

        void AddSet(object setModel)
        {
            if (setModel.GetType() == typeof(StrengthExerciseSetModel))
            {
                var newSet = (StrengthExerciseSetModel)setModel;
                StrengthExerciseSets[CurrentExercise].Add(
                                new StrengthExerciseSetModel
                                {
                                    SetNumber = StrengthExerciseSets[CurrentExercise].Count() + 1,
                                    Reps = (newSet.Reps is null) ? 0 : newSet.Reps,
                                    Weight = (newSet.Weight is null) ? 0 : newSet.Weight
                                });
            }
            else if (setModel.GetType() == typeof(CardioExerciseSetModel))
            {
                var newSet = (CardioExerciseSetModel)setModel;
                CardioExerciseSets[CurrentExercise].Add(
                                new CardioExerciseSetModel
                                {
                                    SetNumber = CardioExerciseSets[CurrentExercise].Count() + 1,
                                    Distance = (newSet.Distance is null) ? 0 : newSet.Distance,
                                    Duration = (newSet.Distance.HasValue) ? (System.TimeSpan)newSet.Duration : new TimeSpan(0)
                                });
            }

            StateHasChanged();
        }

        async void EditSet(object setToEdit, ExerciseCategoryEnum category)
        {
            if (category == ExerciseCategoryEnum.Strength && setToEdit.GetType() == typeof(StrengthExerciseSetModel))
            {
                var set = (StrengthExerciseSetModel)setToEdit;
                var backupSetReps = set.Reps;
                var backupSetWeight = set.Weight;

                var parameters = new DialogParameters();
                parameters.Add("StrengthSetToEdit", set);
                parameters.Add("ExerciseCategory", category);

                var options = new DialogOptions
                {
                    CloseOnEscapeKey = true,
                    CloseButton = true
                };

                var _DialogReference = _DialogService.Show<EditSetDialog>($"Edit {set.Name}", parameters, options);

                var result = await _DialogReference.Result;

                if (result.Cancelled)
                {
                    set.Weight = backupSetWeight;
                    set.Reps = backupSetReps;
                }
            }
            else if (category == ExerciseCategoryEnum.Cardio && setToEdit.GetType() == typeof(CardioExerciseSetModel))
            {
                var set = (CardioExerciseSetModel)setToEdit;
                var backupeDuration = set.Duration;
                var backupDistance = set.Distance;


                var parameters = new DialogParameters();
                parameters.Add("CardioSetToEdit", set);
                parameters.Add("ExerciseCategory", category);

                var options = new DialogOptions
                {
                    CloseOnEscapeKey = true,
                    CloseButton = true
                };

                var _DialogReference = _DialogService.Show<EditSetDialog>($"Edit {set.Name}", parameters, options);

                var result = await _DialogReference.Result;

                if (result.Cancelled)
                {
                    set.Distance = backupDistance;
                    set.Duration = backupeDuration;
                }

            }
            StateHasChanged();
        }

        async void EditTime()
        {
            var parameters = new DialogParameters();
            parameters.Add("DialogTimeSpan", TimerValue);

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true
            };

            var _DialogReference = _DialogService.Show<ChooseTimeDialog>(string.Empty, parameters, options);

            var result = await _DialogReference.Result;

            if (!result.Cancelled)
            {
                TimerValue = (TimeSpan)result.Data;
                TimerValueBackup = TimerValue;
                StateHasChanged();
            }
        }

        void ToggleTimer(bool toggle)
        {
            _TimerIconToggle = toggle;
            if (toggle)
            {
                TimerValueBackup = TimerValue;
                _Timer.Elapsed += TimerEvent;
                _Timer.AutoReset = true;
                _Timer.Enabled = true;
                _Stopwatch.Start();
            }
            else
            {
                _Timer.Enabled = false;
                _Stopwatch.Stop();
                _Stopwatch.Reset();
            }
        }

        async void TimerEvent(object source, ElapsedEventArgs args)
        {
            await InvokeAsync(() =>
            {
                TimerValue = TimerValueBackup.Value - _Stopwatch.Elapsed;
                StateHasChanged();
            });
        }

        void ResetTimer()
        {
            TimerValue = TimerValueBackup ?? TimerValue;
            if (_Stopwatch.IsRunning)
            {
                _Stopwatch.Reset();
            }
            else
            {
                _Stopwatch.Restart();
            }
        }

        #endregion
    }
}
