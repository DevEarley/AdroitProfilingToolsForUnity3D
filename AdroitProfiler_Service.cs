using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class AdroitProfiler_Service
{
    public static readonly int _targetFrameRate = 20;
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

  
}








//

//}
