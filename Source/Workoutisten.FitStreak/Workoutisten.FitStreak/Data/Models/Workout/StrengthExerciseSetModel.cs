using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class StrengthExerciseSetModel: BaseExerciseSetModel
    {
        public int? Reps { get; set; }

        public double? Weight { get; set; }
    }
}
