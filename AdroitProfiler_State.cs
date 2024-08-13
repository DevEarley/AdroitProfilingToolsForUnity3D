using System.Collections.Generic;
using System.Text;
using TMPro;
using System;
using System.IO;
using UnityEngine;
using Unity.Profiling;
using Unity.Profiling.LowLevel.Unsafe;

using UnityEngine.SceneManagement;


public enum AdroitProfiler_StateMetricType
{
    TimePerFrame,
    SystemMemory,
    GCMemory,
    DrawCalls,
    PolyCount
}
public class AdroitProfiler_StateMetrics
{
    public AdroitProfiler_StateMetricType AdroitProfiler_StateMetricType;
   public float MaxValueInLast_TenthSecond = 0;
   public float MaxValueInLast_QuarterSecond = 0;
   public float MaxValueInLast_HalfSecond = 0;
   public float MaxValueInLast_5Seconds = 0;
   public float MaxValueInLast_10Seconds = 0;
    public AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType _AdroitProfiler_StateMetricType)
    {
        AdroitProfiler_StateMetricType = _AdroitProfiler_StateMetricType;
    }
}

public class AdroitProfiler_State : MonoBehaviour
{
    [HideInInspector]
    public float TimeThisFrame = 0;

    public string RunName = "RUN";
    public string GPUStats = "";

    public AdroitProfiler_StateMetrics TimePerFrame_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.TimePerFrame);
    public AdroitProfiler_StateMetrics SystemMemory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.SystemMemory);
    public AdroitProfiler_StateMetrics GCMemory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.GCMemory);
    public AdroitProfiler_StateMetrics DrawCalls_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.DrawCalls);
    public AdroitProfiler_StateMetrics PolyCount_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.PolyCount);

    [HideInInspector]
    public float TimerFor_TenthSecond = 0;
    [HideInInspector]
    public float TimerFor_QuarterSecond = 0;
    [HideInInspector]
    public float TimerFor_HalfSecond = 0;
    [HideInInspector]
    public float TimerFor_5Seconds = 0;
    [HideInInspector]
    public float TimerFor_10Seconds = 0;

    [HideInInspector]
    public int AverageFPSFor_TenthSecond = 0;
    [HideInInspector]
    public int AverageFPSFor_QuarterSecond = 0;
    [HideInInspector]
    public int AverageFPSFor_HalfSecond = 0;
    [HideInInspector]
    public int AverageFPSFor_5Seconds = 0;
    [HideInInspector]
    public int AverageFPSFor_10Seconds = 0;

    ProfilerRecorder systemMemoryRecorder;
    ProfilerRecorder gcMemoryRecorder;
    ProfilerRecorder drawCallsCountRecorder;
    ProfilerRecorder trisCountRecorder;


    private float TotalTimeFor_TenthSecond = 0;
    private float TotalTimeFor_QuarterSecond = 0;
    private float TotalTimeFor_HalfSecond = 0;
    private float TotalTimeFor_5Seconds = 0;
    private float TotalTimeFor_10Seconds = 0;

    private int NumberOfFramesThis_TenthSecond = 0;
    private int NumberOfFramesThis_QuarterSecond = 0;
    private int NumberOfFramesThis_HalfSecond = 0;
    private int NumberOfFramesThis_5Seconds = 0;
    private int NumberOfFramesThis_10Seconds = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        drawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        trisCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //RESET STATE
    }
    private void Update()
    {
        TimerFor_TenthSecond += Time.deltaTime;
        TimerFor_QuarterSecond += Time.deltaTime;
        TimerFor_HalfSecond += Time.deltaTime;
        TimerFor_5Seconds += Time.deltaTime;
        TimerFor_10Seconds += Time.deltaTime;
        TimeThisFrame = Time.deltaTime * 1000.0f;
        CheckTimers();
        UpdateMetrics();
        UpdateAverages();
        UpdateGPUStats();
    }

    private void UpdateAverages()
    {
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_TenthSecond,
            TotalTimeFor_TenthSecond,
            out NumberOfFramesThis_TenthSecond,
            NumberOfFramesThis_TenthSecond,
            out AverageFPSFor_TenthSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_QuarterSecond,
            TotalTimeFor_QuarterSecond,
            out NumberOfFramesThis_QuarterSecond,
            NumberOfFramesThis_QuarterSecond,
            out AverageFPSFor_QuarterSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_HalfSecond,
            TotalTimeFor_HalfSecond,
            out NumberOfFramesThis_HalfSecond,
            NumberOfFramesThis_HalfSecond,
            out AverageFPSFor_HalfSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_5Seconds,
            TotalTimeFor_5Seconds,
            out NumberOfFramesThis_5Seconds,
            NumberOfFramesThis_5Seconds,
            out AverageFPSFor_5Seconds);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_10Seconds,
            TotalTimeFor_10Seconds,
            out NumberOfFramesThis_10Seconds,
            NumberOfFramesThis_10Seconds,
            out AverageFPSFor_10Seconds);
    }

    private void UpdateMetrics()
    {

        TimePerFrame_Metrics.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_TenthSecond, TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_QuarterSecond, TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_HalfSecond, TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_5Seconds, TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_10Seconds, TimeThisFrame);

        SystemMemory_Metrics.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(SystemMemory_Metrics.MaxValueInLast_TenthSecond, systemMemoryRecorder.LastValue);
        SystemMemory_Metrics.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(SystemMemory_Metrics.MaxValueInLast_QuarterSecond, systemMemoryRecorder.LastValue);
        SystemMemory_Metrics.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(SystemMemory_Metrics.MaxValueInLast_HalfSecond, systemMemoryRecorder.LastValue);
        SystemMemory_Metrics.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(SystemMemory_Metrics.MaxValueInLast_5Seconds, systemMemoryRecorder.LastValue);
        SystemMemory_Metrics.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(SystemMemory_Metrics.MaxValueInLast_10Seconds, systemMemoryRecorder.LastValue);

        GCMemory_Metrics.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(GCMemory_Metrics.MaxValueInLast_TenthSecond, gcMemoryRecorder.LastValue);
        GCMemory_Metrics.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(GCMemory_Metrics.MaxValueInLast_QuarterSecond, gcMemoryRecorder.LastValue);
        GCMemory_Metrics.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(GCMemory_Metrics.MaxValueInLast_HalfSecond, gcMemoryRecorder.LastValue);
        GCMemory_Metrics.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(GCMemory_Metrics.MaxValueInLast_5Seconds, gcMemoryRecorder.LastValue);
        GCMemory_Metrics.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(GCMemory_Metrics.MaxValueInLast_10Seconds, gcMemoryRecorder.LastValue);

        DrawCalls_Metrics.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_TenthSecond, drawCallsCountRecorder.LastValue);
        DrawCalls_Metrics.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_QuarterSecond, drawCallsCountRecorder.LastValue);
        DrawCalls_Metrics.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_HalfSecond, drawCallsCountRecorder.LastValue);
        DrawCalls_Metrics.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_5Seconds, drawCallsCountRecorder.LastValue);
        DrawCalls_Metrics.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_10Seconds, drawCallsCountRecorder.LastValue);


    }

    private void CheckTimers()
    {
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_TenthSecond, TimerFor_TenthSecond, AdroitProfiler_Service.MaxTimeForTimer_TenthSecond_TimePerFrame))
        {
            NumberOfFramesThis_TenthSecond = 0;
            TotalTimeFor_TenthSecond = 0;
            TimePerFrame_Metrics.MaxValueInLast_TenthSecond = 0;
            SystemMemory_Metrics.MaxValueInLast_TenthSecond = 0;
            GCMemory_Metrics.MaxValueInLast_TenthSecond = 0;
            DrawCalls_Metrics.MaxValueInLast_TenthSecond = 0;
            PolyCount_Metrics.MaxValueInLast_TenthSecond = 0;
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_QuarterSecond, TimerFor_QuarterSecond, AdroitProfiler_Service.MaxTimeForTimer_QuarterSecond_TimePerFrame))
        {
            TotalTimeFor_QuarterSecond = 0;
            NumberOfFramesThis_QuarterSecond = 0;
            TimePerFrame_Metrics.MaxValueInLast_QuarterSecond = 0;
            SystemMemory_Metrics.MaxValueInLast_QuarterSecond = 0;
            GCMemory_Metrics.MaxValueInLast_QuarterSecond = 0;
            DrawCalls_Metrics.MaxValueInLast_QuarterSecond = 0;
            PolyCount_Metrics.MaxValueInLast_QuarterSecond = 0;
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_HalfSecond, TimerFor_HalfSecond, AdroitProfiler_Service.MaxTimeForTimer_HalfSecond_TimePerFrame))
        {
            TotalTimeFor_HalfSecond = 0;
            NumberOfFramesThis_HalfSecond = 0;
            TimePerFrame_Metrics.MaxValueInLast_HalfSecond = 0;
            SystemMemory_Metrics.MaxValueInLast_HalfSecond = 0;
            GCMemory_Metrics.MaxValueInLast_HalfSecond = 0;
            DrawCalls_Metrics.MaxValueInLast_HalfSecond = 0;
            PolyCount_Metrics.MaxValueInLast_HalfSecond = 0;
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_5Seconds, TimerFor_5Seconds, AdroitProfiler_Service.MaxTimeForTimer_5Seconds_TimePerFrame))
        {
            TotalTimeFor_5Seconds = 0;
            NumberOfFramesThis_5Seconds = 0;
            TimePerFrame_Metrics.MaxValueInLast_5Seconds = 0;
            SystemMemory_Metrics.MaxValueInLast_5Seconds = 0;
            GCMemory_Metrics.MaxValueInLast_5Seconds = 0;
            DrawCalls_Metrics.MaxValueInLast_5Seconds = 0;
            PolyCount_Metrics.MaxValueInLast_5Seconds = 0;
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_10Seconds, TimerFor_10Seconds, AdroitProfiler_Service.MaxTimeForTimer_10Seconds_TimePerFrame))
        {
            TotalTimeFor_10Seconds = 0;
            NumberOfFramesThis_10Seconds = 0;
            TimePerFrame_Metrics.MaxValueInLast_10Seconds = 0;
            SystemMemory_Metrics.MaxValueInLast_10Seconds = 0;
            GCMemory_Metrics.MaxValueInLast_10Seconds = 0;
            DrawCalls_Metrics.MaxValueInLast_10Seconds = 0;
            PolyCount_Metrics.MaxValueInLast_10Seconds = 0;
        }
    }

    private void UpdateGPUStats()
    {
        GPUStats = AdroitProfiler_Service.UpdateGPUStats(gcMemoryRecorder.LastValue, systemMemoryRecorder.LastValue, drawCallsCountRecorder.LastValue, trisCountRecorder.LastValue);

    }


}




