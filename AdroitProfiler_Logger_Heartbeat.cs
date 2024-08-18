using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AdroitProfiler_State;

[RequireComponent(typeof(AdroitProfiler_State))]
[RequireComponent(typeof(AdroitProfiler_Logger))]
public class AdroitProfiler_Logger_Heartbeat : MonoBehaviour
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
                AdroitProfiler_State.onTenth_Heartbeat_delegates.Add(OnTenthHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryQuarterSecond:
                AdroitProfiler_State.onQuarter_Heartbeat_delegates.Add(OnQuarterHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryHalfSecond:
                AdroitProfiler_State.onHalf_Heartbeat_delegates.Add(OnHalfHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every5Seconds:
                AdroitProfiler_State.on5s_Heartbeat_delegates.Add(On5SecondHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every10Seconds:
                AdroitProfiler_State.on10s_Heartbeat_delegates.Add(On10SecondHeartbeat);
                break;
        }
    }

    private void OnTenthHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 0.1s Heartbeat | Logger");
    }

    private void OnQuarterHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 0.25s Heartbeat | Logger");

    }

    private void OnHalfHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 0.5s Heartbeat | Logger");

    }

    private void On5SecondHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 5s Heartbeat | Logger");

    }

    private void On10SecondHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 10s Heartbeat | Logger");

    }
}
