using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_Heartbeat))]
[RequireComponent(typeof(AdroitProfiler_Logger))]
public class AdroitProfiler_Logger_Heartbeat : MonoBehaviour
{
    public AdroitProfiler_Timing Heartbeat_Timing  = AdroitProfiler_Timing.InvokeAtTime;
    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;
    private AdroitProfiler_Logger AdroitProfiler_Logger;

    private void Start()
    {
        AdroitProfiler_Heartbeat = gameObject.GetComponent<AdroitProfiler_Heartbeat>();
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        switch (Heartbeat_Timing)
        {
            case AdroitProfiler_Timing.EveryTenthSecond:
                AdroitProfiler_Heartbeat.onTenth_Heartbeat_delegates.Add(OnTenthHeartbeat);
                break;
            case AdroitProfiler_Timing.EveryQuarterSecond:
                AdroitProfiler_Heartbeat.onQuarter_Heartbeat_delegates.Add(OnQuarterHeartbeat);
                break;
            case AdroitProfiler_Timing.EveryHalfSecond:
                AdroitProfiler_Heartbeat.onHalf_Heartbeat_delegates.Add(OnHalfHeartbeat);
                break;
            case AdroitProfiler_Timing.Every5Seconds:
                AdroitProfiler_Heartbeat.on5s_Heartbeat_delegates.Add(On5SecondHeartbeat);
                break;
            case AdroitProfiler_Timing.Every10Seconds:
                AdroitProfiler_Heartbeat.on10s_Heartbeat_delegates.Add(On10SecondHeartbeat);
                break;
        }
    }

    private void OnTenthHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 0.1s Heartbeat");
    }

    private void OnQuarterHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 0.25s Heartbeat");
    }

    private void OnHalfHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 0.5s Heartbeat");
    }

    private void On5SecondHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 5s Heartbeat");
    }

    private void On10SecondHeartbeat()
    {
        AdroitProfiler_Logger.CapturePerformanceForEvent("On 10s Heartbeat");
    }
}
