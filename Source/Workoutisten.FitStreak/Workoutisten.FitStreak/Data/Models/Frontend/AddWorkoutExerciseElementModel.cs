using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Data.Models.Workout;

namespace Workoutisten.FitStreak.Data.Models.Frontend
{
    public class AddWorkoutExerciseElementModel
    {
        public ExerciseModel ExerciseModel { get; set; }

        public bool Checked { get; set; }
    }
}
