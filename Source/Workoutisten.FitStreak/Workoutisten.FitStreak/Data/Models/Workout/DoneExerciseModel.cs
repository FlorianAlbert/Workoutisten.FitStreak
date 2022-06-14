using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Data.Enums;

namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class DoneExerciseModel
    {
        public Guid DoneExerciseId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid ExerciseId { get; set; }

        public ExerciseModel ExerciseModel { get; set; }

        public Guid? ExerciseGroupId { get; set; }

        public ICollection<Guid> SetIds { get; set; }

        public ICollection<BaseExerciseSetModel> Sets { get; set; }
    }
}
