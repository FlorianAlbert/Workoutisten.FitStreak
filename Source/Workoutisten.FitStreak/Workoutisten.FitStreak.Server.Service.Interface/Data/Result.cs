namespace Workoutisten.FitStreak.Server.Service.Interface.Data;

public class Result
{
    public int StatusCode { get; set; }

    public string? Detail { get; set; }

    public bool Successful => StatusCode >= 200 && StatusCode < 300;

    public bool Unsccessful => !Successful;
}

public class Result<TValue> : Result
{
    public TValue? Value { get; set; }
}
