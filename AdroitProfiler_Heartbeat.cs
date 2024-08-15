using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AdroitProfiler_State;

public enum AdroitProfiler_Heartbeat_Timing
{
    Every10Seconds,
    Every5Seconds,
    EveryHalfSecond,
    EveryQuarterSecond,
    EveryTenthSecond,
    None
}
[RequireComponent(typeof(AdroitProfiler_State))]
[RequireComponent(typeof(AdroitProfiler_Logger))]
public class AdroitProfiler_Heartbeat : MonoBehaviour
{
    public AdroitProfiler_Heartbeat_Timing Heartbeat_Timing  = AdroitProfiler_Heartbeat_Timing.None;
    private AdroitProfiler_State AdroitProfiler_State;
    private AdroitProfiler_Logger AdroitProfiler_Logger;

    private void Start()
    {
        AdroitProfiler_State = gameObject.GetComponent<AdroitProfiler_State>();
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        switch (Heartbeat_Timing)
        {
            case AdroitProfiler_Heartbeat_Timing.EveryTenthSecond:
                AdroitProfiler_State.onTenthReset = OnTenthReset;
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryQuarterSecond:
                AdroitProfiler_State.onQuarterReset = OnQuarterReset;
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryHalfSecond:
                AdroitProfiler_State.onHalfReset = OnHalfReset;
                break;
            case AdroitProfiler_Heartbeat_Timing.Every5Seconds:
                AdroitProfiler_State.on5sReset = On5SecondReset;
                break;
            case AdroitProfiler_Heartbeat_Timing.Every10Seconds:
                AdroitProfiler_State.on10sReset = On10SecondReset;
                break;
        }
    }

    private void OnTenthReset()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("OnTenthReset");
    }

    private void OnQuarterReset()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("OnQuarterReset");

    }

    private void OnHalfReset()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("OnHalfReset");

    }

    private void On5SecondReset()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On5sReset");

    }

    private void On10SecondReset()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On10sReset");

    }
}
