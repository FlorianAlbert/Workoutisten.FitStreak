namespace Workoutisten.FitStreak.Server.Service.Interface.Data;

public class Result<D>
{
    public D? Data { get; set; }

    public ResultStatus Status { get; set; }
}
