using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_Logger))]

public class AdroitProfiler_SpikeDetector : MonoBehaviour
{
    private AdroitProfiler_Logger AdroitProfiler_Logger;
    
    [HideInInspector]
    public float SpikeThresholdInMS = 80.0f / 1000.0f; // 80 MS == 12.5 FPS
    
    [HideInInspector]
    public float HugeSpikeThresholdInMS = 100.0f / 1000.0f; // 100 MS == 10 FPS
    
    [HideInInspector]
    public float ExtremeSpikeThresholdInMS = 150.0f / 1000.0f; // 150 MS == 6.6 FPS

    void Start()
    {
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
    }

    private void Update()
    {
        if (Time.unscaledDeltaTime > SpikeThresholdInMS  && Time.unscaledDeltaTime <= HugeSpikeThresholdInMS)
        {
            AdroitProfiler_Logger.CapturePerformanceForEvent("Spike Detected (over 80MS)");
        }
        else if (Time.unscaledDeltaTime > HugeSpikeThresholdInMS && Time.unscaledDeltaTime <= ExtremeSpikeThresholdInMS)
        {
            AdroitProfiler_Logger.CapturePerformanceForEvent("Huge Spike Detected (over 100MS)");
        }
        else if (Time.unscaledDeltaTime > ExtremeSpikeThresholdInMS)
        {
            AdroitProfiler_Logger.CapturePerformanceForEvent("Extreme Spike Detected (over 150MS)");
        }
    }
}
