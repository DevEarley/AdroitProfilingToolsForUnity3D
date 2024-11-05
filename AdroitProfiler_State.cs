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
    PolyCount,
    drawCallsCountRecorder,
    polyCountRecorder,
    systemMemoryRecorder,
    gcMemoryRecorder,
    mainThreadTimeRecorder,
    mAudioClipCountRecorder,
    mDynamicBathcedDrawCallsCountRecorder,
    mDynamicBatchesCountRecorder,
    mStaticBatchedDrawCallsCountRecorder,
    mStaticBatchesCountRecorder,
    mInstancedBatchedDrawCallsCountRecorder,
    mInstancedBatchesCountRecorder,
    mBatchesCountRecorder,
    mVerticesCountRecorder,
    mSetPassCallsCountRecorder,
    mShadowCastersCountRecorder,
    mVisibleSkinnedMeshesCountRecorder

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
    public AdroitProfiler_StateMetrics systemMemoryRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.systemMemoryRecorder);
    public AdroitProfiler_StateMetrics gcMemoryRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.gcMemoryRecorder);
    public AdroitProfiler_StateMetrics mainThreadTimeRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mainThreadTimeRecorder);
    public AdroitProfiler_StateMetrics mAudioClipCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mAudioClipCountRecorder);
    public AdroitProfiler_StateMetrics mDynamicBathcedDrawCallsCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mDynamicBathcedDrawCallsCountRecorder);
    public AdroitProfiler_StateMetrics mDynamicBatchesCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mDynamicBatchesCountRecorder);
    public AdroitProfiler_StateMetrics mStaticBatchedDrawCallsCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mStaticBatchedDrawCallsCountRecorder);
    public AdroitProfiler_StateMetrics mStaticBatchesCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mStaticBatchesCountRecorder);
    public AdroitProfiler_StateMetrics mInstancedBatchedDrawCallsCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mInstancedBatchedDrawCallsCountRecorder);
    public AdroitProfiler_StateMetrics mInstancedBatchesCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mInstancedBatchesCountRecorder);
    public AdroitProfiler_StateMetrics mBatchesCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mBatchesCountRecorder);
    public AdroitProfiler_StateMetrics mVerticesCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mVerticesCountRecorder);
    public AdroitProfiler_StateMetrics mSetPassCallsCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mSetPassCallsCountRecorder);
    public AdroitProfiler_StateMetrics mShadowCastersCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mShadowCastersCountRecorder);
    public AdroitProfiler_StateMetrics mVisibleSkinnedMeshesCountRecorder_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.mVisibleSkinnedMeshesCountRecorder);


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
        mAudioClipCountRecorder.Dispose();
        mDynamicBathcedDrawCallsCountRecorder.Dispose();
        mDynamicBatchesCountRecorder.Dispose();
        mStaticBatchedDrawCallsCountRecorder.Dispose();
        mStaticBatchesCountRecorder.Dispose();
        mInstancedBatchedDrawCallsCountRecorder.Dispose();
        mInstancedBatchesCountRecorder.Dispose();
        mBatchesCountRecorder.Dispose();
        mVerticesCountRecorder.Dispose();
        mSetPassCallsCountRecorder.Dispose();
        mShadowCastersCountRecorder.Dispose();
        mVisibleSkinnedMeshesCountRecorder.Dispose();

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
        CaptureLastValueForMetrics();
        UpdateGPUStats();
        UpdateMetrics();
        UpdateAverages();
    }

    private void CaptureLastValueForMetrics()
    {
        drawCallsCountRecorder_lastValue = drawCallsCountRecorder.LastValue;
        polyCountRecorder_lastValue = polyCountRecorder.LastValue;
        systemMemoryRecorder_lastValue = systemMemoryRecorder.LastValue;
        gcMemoryRecorder_lastValue = gcMemoryRecorder.LastValue;
        mainThreadTimeRecorder_lastValue = mainThreadTimeRecorder.LastValue;
        mAudioClipCountRecorder_lastValue = mAudioClipCountRecorder.LastValue;
        mDynamicBathcedDrawCallsCountRecorder_lastValue = mDynamicBathcedDrawCallsCountRecorder.LastValue;
        mDynamicBatchesCountRecorder_lastValue = mDynamicBatchesCountRecorder.LastValue;
        mStaticBatchedDrawCallsCountRecorder_lastValue = mStaticBatchedDrawCallsCountRecorder.LastValue;
        mStaticBatchesCountRecorder_lastValue = mStaticBatchesCountRecorder.LastValue;
        mInstancedBatchedDrawCallsCountRecorder_lastValue = mInstancedBatchedDrawCallsCountRecorder.LastValue;
        mInstancedBatchesCountRecorder_lastValue = mInstancedBatchesCountRecorder.LastValue;
        mBatchesCountRecorder_lastValue = mBatchesCountRecorder.LastValue;
        mVerticesCountRecorder_lastValue = mVerticesCountRecorder.LastValue;
        mSetPassCallsCountRecorder_lastValue = mSetPassCallsCountRecorder.LastValue;
        mShadowCastersCountRecorder_lastValue = mShadowCastersCountRecorder.LastValue;
        mVisibleSkinnedMeshesCountRecorder_lastValue = mVisibleSkinnedMeshesCountRecorder.LastValue;
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
        var timeThisFrame = AdroitProfiler_Heartbeat.TimeThisFrame;
        UpdateMetricWithTime(TimePerFrame_Metrics, timeThisFrame);
        UpdateMetricWithTime(DrawCalls_Metrics, timeThisFrame);
        UpdateMetricWithTime(PolyCount_Metrics, timeThisFrame);
        UpdateMetricWithTime(systemMemoryRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(gcMemoryRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mainThreadTimeRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mAudioClipCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mDynamicBathcedDrawCallsCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mDynamicBatchesCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mStaticBatchedDrawCallsCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mStaticBatchesCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mInstancedBatchedDrawCallsCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mInstancedBatchesCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mBatchesCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mVerticesCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mSetPassCallsCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mShadowCastersCountRecorder_Metrics, timeThisFrame);
        UpdateMetricWithTime(mVisibleSkinnedMeshesCountRecorder_Metrics, timeThisFrame);
    }

    private static void UpdateMetricWithTime(AdroitProfiler_StateMetrics aMetric, float timeThisFrame)
    {
        aMetric.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_TenthSecond, timeThisFrame);
        aMetric.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_QuarterSecond, timeThisFrame);
        aMetric.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_HalfSecond, timeThisFrame);
        aMetric.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_5Seconds, timeThisFrame);
        aMetric.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_10Seconds, timeThisFrame);
    }

    private void OnTenthHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_TenthSecond = 1;
        TimePerFrame_Metrics.MaxValueInLast_TenthSecond = timeNow;
        TotalTimeForFPSAvg_TenthSecond = timeNow;
        DrawCalls_Metrics.MaxValueInLast_TenthSecond = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_TenthSecond = polyCountRecorder_lastValue;
       
        systemMemoryRecorder_Metrics.MaxValueInLast_TenthSecond = systemMemoryRecorder_lastValue;
        gcMemoryRecorder_Metrics.MaxValueInLast_TenthSecond = gcMemoryRecorder_lastValue;
        mainThreadTimeRecorder_Metrics.MaxValueInLast_TenthSecond = mainThreadTimeRecorder_lastValue;
        mAudioClipCountRecorder_Metrics.MaxValueInLast_TenthSecond = mAudioClipCountRecorder_lastValue;
        mDynamicBathcedDrawCallsCountRecorder_Metrics.MaxValueInLast_TenthSecond = mDynamicBathcedDrawCallsCountRecorder_lastValue;
        mDynamicBatchesCountRecorder_Metrics.MaxValueInLast_TenthSecond = mDynamicBatchesCountRecorder_lastValue;
        mStaticBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_TenthSecond = mStaticBatchedDrawCallsCountRecorder_lastValue;
        mStaticBatchesCountRecorder_Metrics.MaxValueInLast_TenthSecond = mStaticBatchesCountRecorder_lastValue;
        mInstancedBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_TenthSecond = mInstancedBatchedDrawCallsCountRecorder_lastValue;
        mInstancedBatchesCountRecorder_Metrics.MaxValueInLast_TenthSecond = mInstancedBatchesCountRecorder_lastValue;
        mBatchesCountRecorder_Metrics.MaxValueInLast_TenthSecond = mBatchesCountRecorder_lastValue;
        mVerticesCountRecorder_Metrics.MaxValueInLast_TenthSecond = mVerticesCountRecorder_lastValue;
        mSetPassCallsCountRecorder_Metrics.MaxValueInLast_TenthSecond = mSetPassCallsCountRecorder_lastValue;
        mShadowCastersCountRecorder_Metrics.MaxValueInLast_TenthSecond = mShadowCastersCountRecorder_lastValue;
        mVisibleSkinnedMeshesCountRecorder_Metrics.MaxValueInLast_TenthSecond = mVisibleSkinnedMeshesCountRecorder_lastValue;
    }

    private void OnQuarterHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_QuarterSecond = 1;
        TotalTimeForFPSAvg_QuarterSecond = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_QuarterSecond = timeNow;
        DrawCalls_Metrics.MaxValueInLast_QuarterSecond = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_QuarterSecond = polyCountRecorder_lastValue;
      
        systemMemoryRecorder_Metrics.MaxValueInLast_QuarterSecond = systemMemoryRecorder_lastValue;
        gcMemoryRecorder_Metrics.MaxValueInLast_QuarterSecond = gcMemoryRecorder_lastValue;
        mainThreadTimeRecorder_Metrics.MaxValueInLast_QuarterSecond = mainThreadTimeRecorder_lastValue;
        mAudioClipCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mAudioClipCountRecorder_lastValue;
        mDynamicBathcedDrawCallsCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mDynamicBathcedDrawCallsCountRecorder_lastValue;
        mDynamicBatchesCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mDynamicBatchesCountRecorder_lastValue;
        mStaticBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mStaticBatchedDrawCallsCountRecorder_lastValue;
        mStaticBatchesCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mStaticBatchesCountRecorder_lastValue;
        mInstancedBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mInstancedBatchedDrawCallsCountRecorder_lastValue;
        mInstancedBatchesCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mInstancedBatchesCountRecorder_lastValue;
        mBatchesCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mBatchesCountRecorder_lastValue;
        mVerticesCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mVerticesCountRecorder_lastValue;
        mSetPassCallsCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mSetPassCallsCountRecorder_lastValue;
        mShadowCastersCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mShadowCastersCountRecorder_lastValue;
        mVisibleSkinnedMeshesCountRecorder_Metrics.MaxValueInLast_QuarterSecond = mVisibleSkinnedMeshesCountRecorder_lastValue;
    }

    private void OnHalfHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_HalfSecond = 1;
        TotalTimeForFPSAvg_HalfSecond = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_HalfSecond = timeNow;
        DrawCalls_Metrics.MaxValueInLast_HalfSecond = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_HalfSecond = polyCountRecorder_lastValue;
     
        systemMemoryRecorder_Metrics.MaxValueInLast_HalfSecond = systemMemoryRecorder_lastValue;
        gcMemoryRecorder_Metrics.MaxValueInLast_HalfSecond = gcMemoryRecorder_lastValue;
        mainThreadTimeRecorder_Metrics.MaxValueInLast_HalfSecond = mainThreadTimeRecorder_lastValue;
        mAudioClipCountRecorder_Metrics.MaxValueInLast_HalfSecond = mAudioClipCountRecorder_lastValue;
        mDynamicBathcedDrawCallsCountRecorder_Metrics.MaxValueInLast_HalfSecond = mDynamicBathcedDrawCallsCountRecorder_lastValue;
        mDynamicBatchesCountRecorder_Metrics.MaxValueInLast_HalfSecond = mDynamicBatchesCountRecorder_lastValue;
        mStaticBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_HalfSecond = mStaticBatchedDrawCallsCountRecorder_lastValue;
        mStaticBatchesCountRecorder_Metrics.MaxValueInLast_HalfSecond = mStaticBatchesCountRecorder_lastValue;
        mInstancedBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_HalfSecond = mInstancedBatchedDrawCallsCountRecorder_lastValue;
        mInstancedBatchesCountRecorder_Metrics.MaxValueInLast_HalfSecond = mInstancedBatchesCountRecorder_lastValue;
        mBatchesCountRecorder_Metrics.MaxValueInLast_HalfSecond = mBatchesCountRecorder_lastValue;
        mVerticesCountRecorder_Metrics.MaxValueInLast_HalfSecond = mVerticesCountRecorder_lastValue;
        mSetPassCallsCountRecorder_Metrics.MaxValueInLast_HalfSecond = mSetPassCallsCountRecorder_lastValue;
        mShadowCastersCountRecorder_Metrics.MaxValueInLast_HalfSecond = mShadowCastersCountRecorder_lastValue;
        mVisibleSkinnedMeshesCountRecorder_Metrics.MaxValueInLast_HalfSecond = mVisibleSkinnedMeshesCountRecorder_lastValue;
    }

    private void On5SecondHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_5Seconds = 1;
        TotalTimeForFPSAvg_5Seconds = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_5Seconds = timeNow;
        DrawCalls_Metrics.MaxValueInLast_5Seconds = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_5Seconds = polyCountRecorder_lastValue;
      
        systemMemoryRecorder_Metrics.MaxValueInLast_5Seconds = systemMemoryRecorder_lastValue;
        gcMemoryRecorder_Metrics.MaxValueInLast_5Seconds = gcMemoryRecorder_lastValue;
        mainThreadTimeRecorder_Metrics.MaxValueInLast_5Seconds = mainThreadTimeRecorder_lastValue;
        mAudioClipCountRecorder_Metrics.MaxValueInLast_5Seconds = mAudioClipCountRecorder_lastValue;
        mDynamicBathcedDrawCallsCountRecorder_Metrics.MaxValueInLast_5Seconds = mDynamicBathcedDrawCallsCountRecorder_lastValue;
        mDynamicBatchesCountRecorder_Metrics.MaxValueInLast_5Seconds = mDynamicBatchesCountRecorder_lastValue;
        mStaticBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_5Seconds = mStaticBatchedDrawCallsCountRecorder_lastValue;
        mStaticBatchesCountRecorder_Metrics.MaxValueInLast_5Seconds = mStaticBatchesCountRecorder_lastValue;
        mInstancedBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_5Seconds = mInstancedBatchedDrawCallsCountRecorder_lastValue;
        mInstancedBatchesCountRecorder_Metrics.MaxValueInLast_5Seconds = mInstancedBatchesCountRecorder_lastValue;
        mBatchesCountRecorder_Metrics.MaxValueInLast_5Seconds = mBatchesCountRecorder_lastValue;
        mVerticesCountRecorder_Metrics.MaxValueInLast_5Seconds = mVerticesCountRecorder_lastValue;
        mSetPassCallsCountRecorder_Metrics.MaxValueInLast_5Seconds = mSetPassCallsCountRecorder_lastValue;
        mShadowCastersCountRecorder_Metrics.MaxValueInLast_5Seconds = mShadowCastersCountRecorder_lastValue;
        mVisibleSkinnedMeshesCountRecorder_Metrics.MaxValueInLast_5Seconds = mVisibleSkinnedMeshesCountRecorder_lastValue;
    }

    private void On10SecondHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_10Seconds = 1;
        TotalTimeForFPSAvg_10Seconds = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_10Seconds = timeNow;
        DrawCalls_Metrics.MaxValueInLast_10Seconds = drawCallsCountRecorder_lastValue;
        PolyCount_Metrics.MaxValueInLast_10Seconds = polyCountRecorder_lastValue;
        systemMemoryRecorder_Metrics.MaxValueInLast_10Seconds = systemMemoryRecorder_lastValue;
        gcMemoryRecorder_Metrics.MaxValueInLast_10Seconds = gcMemoryRecorder_lastValue;
        mainThreadTimeRecorder_Metrics.MaxValueInLast_10Seconds = mainThreadTimeRecorder_lastValue;
        mAudioClipCountRecorder_Metrics.MaxValueInLast_10Seconds = mAudioClipCountRecorder_lastValue;
        mDynamicBathcedDrawCallsCountRecorder_Metrics.MaxValueInLast_10Seconds = mDynamicBathcedDrawCallsCountRecorder_lastValue;
        mDynamicBatchesCountRecorder_Metrics.MaxValueInLast_10Seconds = mDynamicBatchesCountRecorder_lastValue;
        mStaticBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_10Seconds = mStaticBatchedDrawCallsCountRecorder_lastValue;
        mStaticBatchesCountRecorder_Metrics.MaxValueInLast_10Seconds = mStaticBatchesCountRecorder_lastValue;
        mInstancedBatchedDrawCallsCountRecorder_Metrics.MaxValueInLast_10Seconds = mInstancedBatchedDrawCallsCountRecorder_lastValue;
        mInstancedBatchesCountRecorder_Metrics.MaxValueInLast_10Seconds = mInstancedBatchesCountRecorder_lastValue;
        mBatchesCountRecorder_Metrics.MaxValueInLast_10Seconds = mBatchesCountRecorder_lastValue;
        mVerticesCountRecorder_Metrics.MaxValueInLast_10Seconds = mVerticesCountRecorder_lastValue;
        mSetPassCallsCountRecorder_Metrics.MaxValueInLast_10Seconds = mSetPassCallsCountRecorder_lastValue;
        mShadowCastersCountRecorder_Metrics.MaxValueInLast_10Seconds = mShadowCastersCountRecorder_lastValue;
        mVisibleSkinnedMeshesCountRecorder_Metrics.MaxValueInLast_10Seconds = mVisibleSkinnedMeshesCountRecorder_lastValue;
    }

    private void UpdateGPUStats()
    {
        GPUStats = AdroitProfiler_Service.UpdateGPUStats(drawCallsCountRecorder_lastValue, polyCountRecorder_lastValue);
    }
}




