﻿namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Password;

public class ResetPassword
{
    public string NewPassword { get; set; }

    public string Token { get; set; }
}