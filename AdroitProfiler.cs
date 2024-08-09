using UnityEngine;

public class AdroitProfiler : MonoBehaviour
{
    public float TimeThisFrame = 0;
    public float PreviousAverage = 0;

    public float MaxInThis_HalfSecond_TimePerFrame = 0;
    public float MaxInThis_5Seconds_TimePerFrame = 0;
    public float MaxInThis_10Seconds_TimePerFrame = 0;
 
    public float TimerFor_HalfSecond_TimePerFrame = 0;
    public float TimerFor_5Seconds_TimePerFrame = 0;
    public float TimerFor_10Seconds_TimePerFrame = 0;


    private void Update()
    {
        TimerFor_HalfSecond_TimePerFrame += Time.deltaTime;
        TimerFor_5Seconds_TimePerFrame += Time.deltaTime;
        TimerFor_10Seconds_TimePerFrame += Time.deltaTime;

        TimeThisFrame = Time.deltaTime * 1000.0f;
        PreviousAverage = 1000.0f / MaxInThis_HalfSecond_TimePerFrame;

        UpdateHalfSecondMetric();
        Update5SecondsMetric();
        Update10SecondsMetric();

    }

    private void UpdateHalfSecondMetric()
    {
        if (TimerFor_HalfSecond_TimePerFrame > AdroitProfiler_Service.MaxTimeForTimer_HalfSecond_TimePerFrame)
        {
            TimerFor_HalfSecond_TimePerFrame = 0;
            MaxInThis_HalfSecond_TimePerFrame = 0;
        }
        if (TimeThisFrame > MaxInThis_HalfSecond_TimePerFrame)
        {
            MaxInThis_HalfSecond_TimePerFrame = TimeThisFrame;
        }
    }
    private void Update5SecondsMetric()
    {
        if (TimerFor_5Seconds_TimePerFrame > AdroitProfiler_Service.MaxTimeForTimer_5Seconds_TimePerFrame)
        {
            TimerFor_5Seconds_TimePerFrame = 0;
            MaxInThis_5Seconds_TimePerFrame = 0;
        }
        if (TimeThisFrame > MaxInThis_5Seconds_TimePerFrame)
        {
            MaxInThis_5Seconds_TimePerFrame = TimeThisFrame;
        }
    }
    private void Update10SecondsMetric()
    {
        if (TimerFor_10Seconds_TimePerFrame > AdroitProfiler_Service.MaxTimeForTimer_10Seconds_TimePerFrame)
        {
            TimerFor_10Seconds_TimePerFrame = 0;
            MaxInThis_10Seconds_TimePerFrame = 0;
        }
        if (TimeThisFrame > MaxInThis_10Seconds_TimePerFrame)
        {
            MaxInThis_10Seconds_TimePerFrame = TimeThisFrame;
        }
    }
}
