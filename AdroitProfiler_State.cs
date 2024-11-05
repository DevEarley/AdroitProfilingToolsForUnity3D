using System;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

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

[RequireComponent(typeof(AdroitProfiler_Heartbeat))]
public class AdroitProfiler_State : MonoBehaviour
{
    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;


    [HideInInspector]
    public string GPUStats = "";

    public AdroitProfiler_StateMetrics TimePerFrame_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.TimePerFrame);
    public AdroitProfiler_StateMetrics DrawCalls_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.DrawCalls);
    public AdroitProfiler_StateMetrics PolyCount_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.PolyCount);

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


    [HideInInspector]
    public float TotalTimeForFPSAvg_TenthSecond = 0;

    [HideInInspector]
    public float TotalTimeForFPSAvg_QuarterSecond = 0;

    [HideInInspector]
    public float TotalTimeForFPSAvg_HalfSecond = 0;

    [HideInInspector]
    public float TotalTimeForFPSAvg_5Seconds = 0;

    [HideInInspector]
    public float TotalTimeForFPSAvg_10Seconds = 0;


    ProfilerRecorder drawCallsCountRecorder;
    private long drawCallsCountRecorder_lastValue;
    ProfilerRecorder polyCountRecorder;
    private long polyCountRecorder_lastValue;


    ProfilerRecorder systemMemoryRecorder;
    private long systemMemoryRecorder_lastValue;

    ProfilerRecorder gcMemoryRecorder;
    private long gcMemoryRecorder_lastValue;

    ProfilerRecorder mainThreadTimeRecorder;
    private long mainThreadTimeRecorder_lastValue;



    ProfilerRecorder mAudioClipCountRecorder;                        
    private long mAudioClipCountRecorder_lastValue;
    ProfilerRecorder mDynamicBathcedDrawCallsCountRecorder;          
    private long mDynamicBathcedDrawCallsCountRecorder_lastValue;
    ProfilerRecorder mDynamicBatchesCountRecorder;                   
    private long mDynamicBatchesCountRecorder_lastValue;
    ProfilerRecorder mStaticBatchedDrawCallsCountRecorder;           
    private long mStaticBatchedDrawCallsCountRecorder_lastValue;
    ProfilerRecorder mStaticBatchesCountRecorder;                    
    private long mStaticBatchesCountRecorder_lastValue;
    ProfilerRecorder mInstancedBatchedDrawCallsCountRecorder;        
    private long mInstancedBatchedDrawCallsCountRecorder_lastValue;
    ProfilerRecorder mInstancedBatchesCountRecorder;                 
    private long mInstancedBatchesCountRecorder_lastValue;
    ProfilerRecorder mBatchesCountRecorder;                          
    private long mBatchesCountRecorder_lastValue;
    ProfilerRecorder mVerticesCountRecorder;                         
    private long mVerticesCountRecorder_lastValue;
    ProfilerRecorder mSetPassCallsCountRecorder;                     
    private long mSetPassCallsCountRecorder_lastValue;
    ProfilerRecorder mShadowCastersCountRecorder;                    
    private long mShadowCastersCountRecorder_lastValue;
    ProfilerRecorder mVisibleSkinnedMeshesCountRecorder;
    private long mVisibleSkinnedMeshesCountRecorder_lastValue;


    private int NumberOfFramesThis_TenthSecond = 0;
    private int NumberOfFramesThis_QuarterSecond = 0;
    private int NumberOfFramesThis_HalfSecond = 0;
    private int NumberOfFramesThis_5Seconds = 0;
    private int NumberOfFramesThis_10Seconds = 0;



    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        drawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        polyCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
        systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
        mAudioClipCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "AudioClip Count");
        mDynamicBathcedDrawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Dynamic Batched Draw Calls Count");
        mDynamicBatchesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Dynamic Batches Count");
        mStaticBatchedDrawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Static Batched Draw Calls Count");
        mStaticBatchesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Static Batches Count");
        mInstancedBatchedDrawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Instanced Batched Draw Calls Count");
        mInstancedBatchesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Instanced Batches Count");
        mBatchesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
        mVerticesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        mSetPassCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
        mShadowCastersCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Shadow Casters Count");
        mVisibleSkinnedMeshesCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Visible Skinned Meshes Count");




    }

    void OnDisable()
    {
        drawCallsCountRecorder.Dispose();
        polyCountRecorder.Dispose();
        systemMemoryRecorder.Dispose();
        gcMemoryRecorder.Dispose();
        mainThreadTimeRecorder.Dispose();
    }

    private void Start()
    {
        AdroitProfiler_Heartbeat = gameObject.GetComponent<AdroitProfiler_Heartbeat>();

        AdroitProfiler_Heartbeat.onTenth_Heartbeat_delegates.Add(OnTenthHeartbeat);

        AdroitProfiler_Heartbeat.onQuarter_Heartbeat_delegates.Add(OnQuarterHeartbeat);

        AdroitProfiler_Heartbeat.onHalf_Heartbeat_delegates.Add(OnHalfHeartbeat);

        AdroitProfiler_Heartbeat.on5s_Heartbeat_delegates.Add(On5SecondHeartbeat);

        AdroitProfiler_Heartbeat.on10s_Heartbeat_delegates.Add(On10SecondHeartbeat);

    }


    private void Update()
    {

        drawCallsCountRecorder_lastValue = drawCallsCountRecorder.LastValue;
        polyCountRecorder_lastValue = polyCountRecorder.LastValue;
        UpdateGPUStats();

        UpdateMetrics();
        UpdateAverages();
    }

    private void UpdateAverages()
    {
        AdroitProfiler_Service.UpdateFPSForTimespan(
          AdroitProfiler_Heartbeat.TimeThisFrame,
            out TotalTimeForFPSAvg_TenthSecond,
            TotalTimeForFPSAvg_TenthSecond,
            out NumberOfFramesThis_TenthSecond,
            NumberOfFramesThis_TenthSecond,
            out AverageFPSFor_TenthSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            AdroitProfiler_Heartbeat.TimeThisFrame,
            out TotalTimeForFPSAvg_QuarterSecond,
            TotalTimeForFPSAvg_QuarterSecond,
            out NumberOfFramesThis_QuarterSecond,
            NumberOfFramesThis_QuarterSecond,
            out AverageFPSFor_QuarterSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            AdroitProfiler_Heartbeat.TimeThisFrame,
            out TotalTimeForFPSAvg_HalfSecond,
            TotalTimeForFPSAvg_HalfSecond,
            out NumberOfFramesThis_HalfSecond,
            NumberOfFramesThis_HalfSecond,
            out AverageFPSFor_HalfSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            AdroitProfiler_Heartbeat.TimeThisFrame,
            out TotalTimeForFPSAvg_5Seconds,
            TotalTimeForFPSAvg_5Seconds,
            out NumberOfFramesThis_5Seconds,
            NumberOfFramesThis_5Seconds,
            out AverageFPSFor_5Seconds);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            AdroitProfiler_Heartbeat.TimeThisFrame,
            out TotalTimeForFPSAvg_10Seconds,
            TotalTimeForFPSAvg_10Seconds,
            out NumberOfFramesThis_10Seconds,
            NumberOfFramesThis_10Seconds,
            out AverageFPSFor_10Seconds);
    }

    private void UpdateMetrics()
    {
        TimePerFrame_Metrics.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_TenthSecond, AdroitProfiler_Heartbeat.TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_QuarterSecond, AdroitProfiler_Heartbeat.TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_HalfSecond, AdroitProfiler_Heartbeat.TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_5Seconds, AdroitProfiler_Heartbeat.TimeThisFrame);
        TimePerFrame_Metrics.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(TimePerFrame_Metrics.MaxValueInLast_10Seconds, AdroitProfiler_Heartbeat.TimeThisFrame);

        DrawCalls_Metrics.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_TenthSecond, drawCallsCountRecorder_lastValue);
        DrawCalls_Metrics.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_QuarterSecond, drawCallsCountRecorder_lastValue);
        DrawCalls_Metrics.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_HalfSecond, drawCallsCountRecorder_lastValue);
        DrawCalls_Metrics.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_5Seconds, drawCallsCountRecorder_lastValue);
        DrawCalls_Metrics.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(DrawCalls_Metrics.MaxValueInLast_10Seconds, drawCallsCountRecorder_lastValue);

        PolyCount_Metrics.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(PolyCount_Metrics.MaxValueInLast_TenthSecond, polyCountRecorder_lastValue);
        PolyCount_Metrics.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(PolyCount_Metrics.MaxValueInLast_QuarterSecond, polyCountRecorder_lastValue);
        PolyCount_Metrics.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(PolyCount_Metrics.MaxValueInLast_HalfSecond, polyCountRecorder_lastValue);
        PolyCount_Metrics.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(PolyCount_Metrics.MaxValueInLast_5Seconds, polyCountRecorder_lastValue);
        PolyCount_Metrics.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(PolyCount_Metrics.MaxValueInLast_10Seconds, polyCountRecorder_lastValue);
    }

    private void OnTenthHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_TenthSecond = 1;
        TimePerFrame_Metrics.MaxValueInLast_TenthSecond = timeNow;
        DrawCalls_Metrics.MaxValueInLast_TenthSecond = drawCallsCountRecorder_lastValue;
        TotalTimeForFPSAvg_TenthSecond = timeNow;
        PolyCount_Metrics.MaxValueInLast_TenthSecond = polyCountRecorder_lastValue;
    }

    private void OnQuarterHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_QuarterSecond = 1;
        TotalTimeForFPSAvg_QuarterSecond = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_QuarterSecond = timeNow;
        DrawCalls_Metrics.MaxValueInLast_QuarterSecond = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_QuarterSecond = polyCountRecorder_lastValue;
    }

    private void OnHalfHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_HalfSecond = 1;
        TotalTimeForFPSAvg_HalfSecond = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_HalfSecond = timeNow;
        DrawCalls_Metrics.MaxValueInLast_HalfSecond = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_HalfSecond = polyCountRecorder_lastValue;
    }

    private void On5SecondHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_5Seconds = 1;
        TotalTimeForFPSAvg_5Seconds = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_5Seconds = timeNow;
        DrawCalls_Metrics.MaxValueInLast_5Seconds = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_5Seconds = polyCountRecorder_lastValue;
    }

    private void On10SecondHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_10Seconds = 1;
        TotalTimeForFPSAvg_10Seconds = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_10Seconds = timeNow;
        DrawCalls_Metrics.MaxValueInLast_10Seconds = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_10Seconds = polyCountRecorder_lastValue;
    }

    private void UpdateGPUStats()
    {
        GPUStats = AdroitProfiler_Service.UpdateGPUStats(drawCallsCountRecorder_lastValue, polyCountRecorder_lastValue);
    }
}




