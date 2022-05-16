using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Data.Enums;

namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class ExerciseModel
    {
        public string Name { get; set; }

        public ExerciseCategoryEnum Category { get; set; }

    }
}
