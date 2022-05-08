using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models
{
    public class AddWorkoutExerciseElementModel
    {
        public ExerciseModel ExerciseModel { get; set; }

        public bool Checked { get; set; }
    }
}
