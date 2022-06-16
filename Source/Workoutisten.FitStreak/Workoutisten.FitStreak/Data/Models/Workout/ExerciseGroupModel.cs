using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class ExerciseGroupModel
    {
        public Guid ExerciseGroupId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string GroupName { get; set; }

        public List<Guid> ParticipantIds { get; set; }

        public Guid? WorkoutId { get; set; }

        public List<DoneExerciseModel> DoneExercises { get; set; } = new();
    }
}
