using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics;
using System.Timers;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Data.Converter;
using Workoutisten.FitStreak.Data.Enums;
using Workoutisten.FitStreak.Data.Models.Workout;
using Workoutisten.FitStreak.Pages.Dialogs;
using Workoutisten.FitStreak.Services;
using TimeSpan = System.TimeSpan;

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
        public WorkoutModel Workout { get; set; }
        #endregion

        #region Properties

        [Inject]
        public IRestClient _RestClient { get; set; }

        [Inject]
        public IConverterWrapper _Converter { get; set; }

        [Inject]
        public ErrorDialogService _ErrorDialogService { get; set; }

        ExerciseModel CurrentExercise { get; set; }

        List<ExerciseModel> ExerciseList { get; set; } = new();

        Dictionary<ExerciseModel, List<StrengthExerciseSetModel>> StrengthExerciseSets { get; set; } = new();
        Dictionary<ExerciseModel, List<CardioExerciseSetModel>> CardioExerciseSets { get; set; } = new();

        StrengthExerciseSetModel NewStrengthExerciseSetModel { get; set; } = new();

        CardioExerciseSetModel NewCardioExerciseSetModel { get; set; } = new();

        public ExerciseGroup ExerciseGroup { get; set; }

        public List<DoneExercise> DoneExercises { get; set; } = new();

        TimeSpan TimerValue { get; set; } = new TimeSpan(0, 2, 0);

        TimeSpan? TimerValueBackup { get; set; }

        System.Timers.Timer _Timer { get; set; } = new System.Timers.Timer(1000);

        Stopwatch _Stopwatch { get; set; } = new();

        bool _TimerIconToggle { get; set; }
        #endregion

        #region Methods
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (WorkoutGuid.HasValue)
                {
                    try
                    {
                        Workout = await _Converter.ToEntity<Client.RestClient.Workout, WorkoutModel>(await _RestClient.CallControlled(c => c.GetWorkoutAsync(WorkoutGuid.Value)));
                        ExerciseList = (await Task.WhenAll((await _RestClient.CallControlled(c => c.GetExercisesOfWorkoutAsync(Workout.Guid))).Select(exercise => _Converter.ToEntity<Client.RestClient.Exercise, ExerciseModel>(exercise)))).ToList();

                        if (ExerciseList.Count == 0)
                        {
                            await _ErrorDialogService.ShowGeneralErrorDialog();
                            base.OnAfterRenderAsync(firstRender);
                            return;
                        }

                        CurrentExercise = ExerciseList[0];

                        ExerciseList.ForEach(exercise =>
                        {
                            if (exercise.Category == ExerciseCategoryEnum.Cardio)
                            {
                                CardioExerciseSets.Add(exercise, new List<CardioExerciseSetModel>());
                            }
                            else
                            {
                                StrengthExerciseSets.Add(exercise, new List<StrengthExerciseSetModel>());
                            }
                        });

                        ExerciseGroup = await _RestClient.CallControlled(async c => await c.CreateExerciseGroupAsync(
                            new ExerciseGroup()
                            {
                                GroupName = $"{Workout.WorkoutName} {DateTime.Now.ToString(@"dd\:MM\:yyyy\HH:\mm")}",
                                WorkoutId = WorkoutGuid,
                                ParticipantIds = new List<Guid>() { Guid.Parse(await SecureStorage.GetAsync("userId"))},
                                DoneExerciseIds = new List<Guid>()
                            }));

                    }
                    catch (ApiException<ProblemDetails> e)
                    {
                        await _ErrorDialogService.ShowErrorDialog(e.StatusCode.ToString(), e.Result.Detail);
                    }
                    catch (Exception e)
                    {
                        await _ErrorDialogService.ShowErrorDialog();
                    }
                }
            }
            //Exercises anhand der ExerciseGuids oder der WorkoutGuid holen

            #region ToDelete
            //WorkoutName = "Workout 1 awdawdawdasdas";

            //var ex1 = new ExerciseModel() { Name = "Exercise 1", Category = ExerciseCategoryEnum.Strength };
            //CurrentExercise = ex1;
            //var ex2 = new ExerciseModel() { Name = "Exercise 2", Category = ExerciseCategoryEnum.Strength };
            //var ex3 = new ExerciseModel() { Name = "Exercise 3", Category = ExerciseCategoryEnum.Cardio };
            //var ex4 = new ExerciseModel() { Name = "Exercise 4", Category = ExerciseCategoryEnum.Cardio };

            //ExerciseList.Add(ex1);
            //ExerciseList.Add(ex2);
            //ExerciseList.Add(ex3);
            //ExerciseList.Add(ex4);

            //StrengthExerciseSets.Add(ex1, new List<StrengthExerciseSetModel>() { /*new StrengthExerciseSetModel() { SetNumber = 1, Reps = 12, Weight = 10 }, new StrengthExerciseSetModel() { SetNumber = 2, Reps = 12, Weight = 10 }*/ });
            //StrengthExerciseSets.Add(ex2, new List<StrengthExerciseSetModel>() { new StrengthExerciseSetModel() { SetNumber = 1, Reps = 12, Weight = 10 }, new StrengthExerciseSetModel() { SetNumber = 2, Reps = 12, Weight = 10 }, new StrengthExerciseSetModel() { SetNumber = 3, Reps = 12, Weight = 10 } });
            //CardioExerciseSets.Add(ex3, new List<CardioExerciseSetModel>() { new CardioExerciseSetModel() { SetNumber = 1, Duration = new TimeSpan(0, 1, 0), Distance = 10 } });
            //CardioExerciseSets.Add(ex4, new List<CardioExerciseSetModel>() { /*new CardioExerciseSetModel() { SetNumber = 1, Reps = 12, Weight = 10 }*/ });

            //Exercises.
            #endregion

            StateHasChanged();

            base.OnAfterRenderAsync(firstRender);
        }

        void CancelWorkout()
        {
            //To be done
        }

        void CompleteWorkout()
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

        async Task AddSet(object setModel)
        {
            try
            {
                var doneExercise = DoneExercises.SingleOrDefault(exercise => exercise.ExerciseId.Equals(CurrentExercise.Guid));
                if (doneExercise is null)
                {
                    doneExercise = await _RestClient.CallControlled(async c => await c.CreateDoneExerciseAsync(new DoneExercise() { ExerciseGroupId = ExerciseGroup.ExerciseGroupId, ExerciseId = CurrentExercise.Guid }));
                    DoneExercises.Add(doneExercise);
                }

                if (setModel is StrengthExerciseSetModel strengthSetModel)
                {
                    var set = new StrengthSet() { DoneExerciseId = doneExercise.DoneExerciseId, Repetitions = strengthSetModel.Reps ?? 0, Weight = strengthSetModel.Weight ?? 0 };
                    set = await _RestClient.CallControlled(c => c.CreateStrengthSetAsync(set));
                    StrengthExerciseSets[CurrentExercise].Add(new StrengthExerciseSetModel() { Reps = set.Repetitions, Weight = set.Weight, SetNumber = StrengthExerciseSets[CurrentExercise].Count });
                }
                else if (setModel is CardioExerciseSetModel cardioSetModel)
                {
                    var set = new CardioSet()
                    {
                        DoneExerciseId = doneExercise.DoneExerciseId,
                        Distance = cardioSetModel.Distance ?? 0,
                        Duration = cardioSetModel.Duration.HasValue ?
                        new Client.RestClient.TimeSpan()
                        {
                            Hours = cardioSetModel.Duration.Value.Hours,
                            Minutes = cardioSetModel.Duration.Value.Minutes,
                            Seconds = cardioSetModel.Duration.Value.Seconds
                        } :
                            new Client.RestClient.TimeSpan() { Hours = 0, Minutes = 0, Seconds = 0 }
                    };
                    set = await _RestClient.CallControlled(c => c.CreateCardioSetAsync(set));

                    CardioExerciseSets[CurrentExercise].Add(new CardioExerciseSetModel() { Distance = set.Distance, Duration = new TimeSpan(set.Duration.Ticks), SetNumber = CardioExerciseSets[CurrentExercise].Count });
                }
            }
            catch (ApiException<ProblemDetails> e)
            {
                await _ErrorDialogService.ShowErrorDialog(e.StatusCode.ToString(), e.Result.Detail);
            }
            catch (Exception e)
            {
                await _ErrorDialogService.ShowErrorDialog();
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
                if (TimerValue.TotalSeconds == 0)
                {
                    ToggleTimer(false);
                    for (int i = 0; i < 3; i++)
                    {
                        Vibration.Vibrate(1000);
                    }
                    //TODO: Maybe Ton spielen oder so?
                }
                StateHasChanged();
            });
        }

        void ResetTimer()
        {
            TimerValue = TimerValueBackup ?? TimerValue;
            if (_Stopwatch.IsRunning)
            {

                _Stopwatch.Restart();
            }
            else
            {
                _Stopwatch.Reset();
            }
        }

        #endregion
    }
}
