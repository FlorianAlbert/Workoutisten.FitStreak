using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class CardioExerciseSetModel: BaseExerciseSetModel
    {
        public TimeSpan? Duration { get; set; }

        public int? Distance { get; set; }
    }
}
