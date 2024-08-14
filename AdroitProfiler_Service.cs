using System.Text;
using UnityEngine;

public static class AdroitProfiler_Service
{
    public static readonly int _targetFrameRate = 20;
    public static readonly float MaxTimeForTimer_TenthSecond = 0.1f;
    public static readonly float MaxTimeForTimer_QuarterSecond = 0.25f;
    public static readonly float MaxTimeForTimer_HalfSecond = 0.5f;
    public static readonly float MaxTimeForTimer_5Seconds = 5.0f;
    public static readonly float MaxTimeForTimer_10Seconds = 10.0f;

    public static string FormatTime(float timeInSeconds)
    {
        var aTime = System.TimeSpan.FromSeconds(timeInSeconds);
        var hours = aTime.Hours.ToString("00");
        var minutes = aTime.Minutes.ToString("00");
        var seconds = aTime.Seconds.ToString("00");
        var milliseconds = aTime.Milliseconds.ToString("000");
        return hours + ":" + minutes + ":" + seconds + ":" + milliseconds;
    }


    public static bool CheckTimer(out float TimerFor_n_TimePerFrame_OUT, float TimerFor_n_TimePerFrame_IN, float MaxTimeForTimer_n_TimePerFrame)
    {
        TimerFor_n_TimePerFrame_OUT = TimerFor_n_TimePerFrame_IN;
       
        if (TimerFor_n_TimePerFrame_OUT > MaxTimeForTimer_n_TimePerFrame)
        {
            TimerFor_n_TimePerFrame_OUT = 0;
            return true;
        }
        return false;
    }

    public static float UpdateMetric(  float MaxInThis_n_TimePerFrame_IN, float LatestValue)
    {
        if (LatestValue > MaxInThis_n_TimePerFrame_IN)
        {
            return LatestValue;
        }
        return MaxInThis_n_TimePerFrame_IN;
    }

    public static void UpdateFPSForTimespan(float newFrameTime, out float total_OUT,float total_IN, out int numberOfFramesInThisTimespan_OUT, int numberOfFramesInThisTimespan_IN, out int averageForThisTimespan_OUT)
    {
        numberOfFramesInThisTimespan_OUT = numberOfFramesInThisTimespan_IN + 1;
        total_OUT = total_IN + newFrameTime;
        if (numberOfFramesInThisTimespan_OUT == 0) numberOfFramesInThisTimespan_OUT = 1;
        var average = (total_OUT / numberOfFramesInThisTimespan_OUT);
        averageForThisTimespan_OUT = Mathf.CeilToInt(1.0f / (0.001f * average));
    }

    public static string UpdateGPUStats(
      long systemMemoryRecorder_lastValue,
      long drawCallsCountRecorder_lastValue,
      long trisCountRecorder_lastValue
      )
    {
        var sb = new StringBuilder(500);
     
        sb.AppendLine($"System Memory: {systemMemoryRecorder_lastValue / (1024 * 1024)} MB");
        sb.AppendLine($"Draw Calls: {drawCallsCountRecorder_lastValue}");
        sb.AppendLine($"Triangles: {trisCountRecorder_lastValue} ");
        return sb.ToString();
    }

}


