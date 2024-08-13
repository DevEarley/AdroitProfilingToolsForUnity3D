using UnityEngine;

public static class AdroitProfiler_Service
{
    public static readonly int _targetFrameRate = 20;
    public static readonly float MaxTimeForTimer_TenthSecond_TimePerFrame = 0.1f;
    public static readonly float MaxTimeForTimer_QuarterSecond_TimePerFrame = 0.25f;
    public static readonly float MaxTimeForTimer_HalfSecond_TimePerFrame = 0.5f;
    public static readonly float MaxTimeForTimer_5Seconds_TimePerFrame = 5.0f;
    public static readonly float MaxTimeForTimer_10Seconds_TimePerFrame = 10.0f;

    public static string FormatTime(float timeInSeconds)
    {
        var aTime = System.TimeSpan.FromSeconds(timeInSeconds);
        var hours = aTime.Hours.ToString("00");
        var minutes = aTime.Minutes.ToString("00");
        var seconds = aTime.Seconds.ToString("00");
        var milliseconds = aTime.Milliseconds.ToString("000");
        return hours + ":" + minutes + ":" + seconds + ":" + milliseconds;
    }

    public static void UpdateMetric(out float TimerFor_n_TimePerFrame, out float MaxInThis_n_TimePerFrame, float TimerFor_n_TimePerFrame_IN, float MaxInThis_n_TimePerFrame_IN, float _TimeThisFrame, float MaxTimeForTimer_n_TimePerFrame)
    {
        TimerFor_n_TimePerFrame = TimerFor_n_TimePerFrame_IN;
        MaxInThis_n_TimePerFrame = MaxInThis_n_TimePerFrame_IN;
        if (TimerFor_n_TimePerFrame > MaxTimeForTimer_n_TimePerFrame)
        {
            TimerFor_n_TimePerFrame = 0;
            MaxInThis_n_TimePerFrame = 0;
        }
        if (_TimeThisFrame > MaxInThis_n_TimePerFrame)
        {
            MaxInThis_n_TimePerFrame = _TimeThisFrame;
        }
    }

    public static void UpdateFPSForTimespan(float newFrameTime, out float total_OUT,float total_IN, out int numberOfFramesInThisTimespan, int numberOfFramesInThisTimespan_IN, out int averageForThisTimespan)
    {
        numberOfFramesInThisTimespan = numberOfFramesInThisTimespan_IN + 1;
        total_OUT = total_IN + newFrameTime;
        if (numberOfFramesInThisTimespan == 0) numberOfFramesInThisTimespan = 1;
        averageForThisTimespan = Mathf.CeilToInt(total_OUT / numberOfFramesInThisTimespan);
    }

}


