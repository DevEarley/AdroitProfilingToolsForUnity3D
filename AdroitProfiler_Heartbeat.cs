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
    public AdroitProfiler_Heartbeat_Timing Heartbeat_Timing;
    private AdroitProfiler_State AdroitProfiler_State;
    private AdroitProfiler_Logger AdroitProfiler_Logger;

    private void Start()
    {
        AdroitProfiler_State = gameObject.GetComponent<AdroitProfiler_State>();
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        AdroitProfiler_State.onTenthReset = OnTenthReset;
        AdroitProfiler_State.onQuarterReset = OnQuarterReset;
        AdroitProfiler_State.onHalfReset = OnHalfReset;
        AdroitProfiler_State.on5sReset = On5SecondReset;
        AdroitProfiler_State.on10sReset = On10SecondReset;
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
