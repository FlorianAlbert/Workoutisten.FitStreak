﻿namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement;

public class User
{
    public Guid UserId { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime CreatedDate { get; set; }

    public int ExerciseStreak { get; set; }

    public DateTime LastExercise { get; set; }

    public Guid[] FriendIds { get; set; }

    public Guid[] FriendRequestIds { get; set; }
}