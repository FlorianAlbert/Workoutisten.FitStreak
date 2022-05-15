using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class WorkoutModel
    {
        public string WorkoutName { get; set; }

        public string Description { get; set; }

        public DateOnly LastTraining { get; set; }

        public List<ExerciseModel> Exercises { get; set; } = new List<ExerciseModel>();
    }
}
