using System;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

public enum AdroitProfiler_StateMetricType
{
    TimePerFrame,

    App_Committed_Memory,
    App_Resident_Memory,
    Audio_Reserved_Memory,
    Audio_Used_Memory,
    GC_Reserved_Memory,
    GC_Used_Memory,
    Profiler_Reserved_Memory,
    Profiler_Used_Memory,
    System_Total_Used,
    System_Used_Memory,
    Total_Reserved_Memory,
    Total_Used_Memory,
    Video_Reserved_Memory,
    Video_Used_Memory,

    Batches_Count,
    CPU_Main_Thread_Frame_Time,
    CPU_Render_Thread_Frame_Time,
    CPU_Total_Frame_Time,
    Draw_Calls_Count,
    GPU_Frame_Time,
    Index_Buffer_Upload_In_Frame_Bytes,
    Index_Buffer_Upload_In_Frame_Count,
    Bytes,
    Render_Textures_Changes_Count,
    Render_Textures_Count,
    SetPass_Calls_Count,
    Shadow_Casters_Count,
    Triangles_Count,
    Used_Buffers_Bytes,
    Used_Buffers_Count,
    Vertex_Buffer_Upload_In_Frame_Bytes,
    Vertex_Buffer_Upload_In_Frame_Count,
    Vertices_Count,
    Video_Memory_Bytes,
    Visible_Skinned_Meshes_Count
}

/*

     App Committed Memory        
     App Resident Memory     
     Audio Reserved Memory 	
     Audio Used Memory 	
     GC Reserved Memory 	
     GC Used Memory 	
     Profiler Reserved Memory 	
     Profiler Used Memory 	
     System Total Used 
     System Used Memory 	
     Total Reserved Memory 	
     Total Used Memory 	
     Video Reserved Memory 	
     Video Used Memory 	

     Batches Count  
     CPU Main Thread Frame Time
     CPU Render Thread Frame Time
     CPU Total Frame Time
     Draw Calls Count 	
     GPU Frame Time 	
     Index Buffer Upload In Frame Bytes
     Index Buffer Upload In Frame Count
     Bytes 
     Render Textures Changes Count
     Render Textures Count 	
     SetPass Calls Count 	
     Shadow Casters Count 	
     Triangles Count 	 
     Used Buffers Bytes 	
     Used Buffers Count 	
     Vertex Buffer Upload In Frame Bytes
     Vertex Buffer Upload In Frame Count 
     Vertices Count 	 
     Video Memory Bytes 	
     Visible Skinned Meshes Count
 */


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

    public AdroitProfiler_StateMetrics App_Committed_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.App_Committed_Memory);
    public AdroitProfiler_StateMetrics App_Resident_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.App_Resident_Memory);
    public AdroitProfiler_StateMetrics Audio_Reserved_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Audio_Reserved_Memory);
    public AdroitProfiler_StateMetrics Audio_Used_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Audio_Used_Memory);
    public AdroitProfiler_StateMetrics GC_Reserved_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.GC_Reserved_Memory);
    public AdroitProfiler_StateMetrics GC_Used_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.GC_Used_Memory);
    public AdroitProfiler_StateMetrics Profiler_Reserved_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Profiler_Reserved_Memory);
    public AdroitProfiler_StateMetrics Profiler_Used_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Profiler_Used_Memory);
    public AdroitProfiler_StateMetrics System_Total_Used_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.System_Total_Used);
    public AdroitProfiler_StateMetrics System_Used_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.System_Used_Memory);
    public AdroitProfiler_StateMetrics Total_Reserved_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Total_Reserved_Memory);
    public AdroitProfiler_StateMetrics Total_Used_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Total_Used_Memory);
    public AdroitProfiler_StateMetrics Video_Reserved_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Video_Reserved_Memory);
    public AdroitProfiler_StateMetrics Video_Used_Memory_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Video_Used_Memory);


    public AdroitProfiler_StateMetrics Batches_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Batches_Count);
    public AdroitProfiler_StateMetrics CPU_Main_Thread_Frame_Time_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.CPU_Main_Thread_Frame_Time);
    public AdroitProfiler_StateMetrics CPU_Render_Thread_Frame_Time_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.CPU_Render_Thread_Frame_Time);
    public AdroitProfiler_StateMetrics CPU_Total_Frame_Time_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.CPU_Total_Frame_Time);
    public AdroitProfiler_StateMetrics Draw_Calls_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Draw_Calls_Count);
    public AdroitProfiler_StateMetrics GPU_Frame_Time_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.GPU_Frame_Time);
    public AdroitProfiler_StateMetrics Index_Buffer_Upload_In_Frame_Bytes_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Index_Buffer_Upload_In_Frame_Bytes);
    public AdroitProfiler_StateMetrics Index_Buffer_Upload_In_Frame_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Index_Buffer_Upload_In_Frame_Count);
    public AdroitProfiler_StateMetrics Bytes_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Bytes);
    public AdroitProfiler_StateMetrics Render_Textures_Changes_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Render_Textures_Changes_Count);
    public AdroitProfiler_StateMetrics Render_Textures_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Render_Textures_Count);
    public AdroitProfiler_StateMetrics SetPass_Calls_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.SetPass_Calls_Count);
    public AdroitProfiler_StateMetrics Shadow_Casters_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Shadow_Casters_Count);
    public AdroitProfiler_StateMetrics Triangles_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Triangles_Count);
    public AdroitProfiler_StateMetrics Used_Buffers_Bytes_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Used_Buffers_Bytes);
    public AdroitProfiler_StateMetrics Used_Buffers_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Used_Buffers_Count);
    public AdroitProfiler_StateMetrics Vertex_Buffer_Upload_In_Frame_Bytes_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Vertex_Buffer_Upload_In_Frame_Bytes);
    public AdroitProfiler_StateMetrics Vertex_Buffer_Upload_In_Frame_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Vertex_Buffer_Upload_In_Frame_Count);
    public AdroitProfiler_StateMetrics Vertices_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Vertices_Count);
    public AdroitProfiler_StateMetrics Video_Memory_Bytes_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Video_Memory_Bytes);
    public AdroitProfiler_StateMetrics Visible_Skinned_Meshes_Count_Metrics = new AdroitProfiler_StateMetrics(AdroitProfiler_StateMetricType.Visible_Skinned_Meshes_Count);


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

    private int NumberOfFramesThis_TenthSecond = 0;
    private int NumberOfFramesThis_QuarterSecond = 0;
    private int NumberOfFramesThis_HalfSecond = 0;
    private int NumberOfFramesThis_5Seconds = 0;
    private int NumberOfFramesThis_10Seconds = 0;

    private ProfilerRecorder App_Committed_Memory_Recorder;
    private ProfilerRecorder App_Resident_Memory_Recorder;
    private ProfilerRecorder Audio_Reserved_Memory_Recorder;
    private ProfilerRecorder Audio_Used_Memory_Recorder;
    private ProfilerRecorder GC_Reserved_Memory_Recorder;
    private ProfilerRecorder GC_Used_Memory_Recorder;
    private ProfilerRecorder Profiler_Reserved_Memory_Recorder;
    private ProfilerRecorder Profiler_Used_Memory_Recorder;
    private ProfilerRecorder System_Total_Used_Recorder;
    private ProfilerRecorder System_Used_Memory_Recorder;
    private ProfilerRecorder Total_Reserved_Memory_Recorder;
    private ProfilerRecorder Total_Used_Memory_Recorder;
    private ProfilerRecorder Video_Reserved_Memory_Recorder;
    private ProfilerRecorder Video_Used_Memory_Recorder;

    private ProfilerRecorder Batches_Count_Recorder;
    private ProfilerRecorder CPU_Main_Thread_Frame_Time_Recorder;
    private ProfilerRecorder CPU_Render_Thread_Frame_Time_Recorder;
    private ProfilerRecorder CPU_Total_Frame_Time_Recorder;
    private ProfilerRecorder Draw_Calls_Count_Recorder;
    private ProfilerRecorder GPU_Frame_Time_Recorder;
    private ProfilerRecorder Index_Buffer_Upload_In_Frame_Bytes_Recorder;
    private ProfilerRecorder Index_Buffer_Upload_In_Frame_Count_Recorder;
    private ProfilerRecorder Bytes_Recorder;
    private ProfilerRecorder Render_Textures_Changes_Count_Recorder;
    private ProfilerRecorder Render_Textures_Count_Recorder;
    private ProfilerRecorder SetPass_Calls_Count_Recorder;
    private ProfilerRecorder Shadow_Casters_Count_Recorder;
    private ProfilerRecorder Triangles_Count_Recorder;
    private ProfilerRecorder Used_Buffers_Bytes_Recorder;
    private ProfilerRecorder Used_Buffers_Count_Recorder;
    private ProfilerRecorder Vertex_Buffer_Upload_In_Frame_Bytes_Recorder;
    private ProfilerRecorder Vertex_Buffer_Upload_In_Frame_Count_Recorder;
    private ProfilerRecorder Vertices_Count_Recorder;
    private ProfilerRecorder Video_Memory_Bytes_Recorder;
    private ProfilerRecorder Visible_Skinned_Meshes_Count_Recorder;

    private long App_Committed_Memory_lastValue;
    private long App_Resident_Memory_lastValue;
    private long Audio_Reserved_Memory_lastValue;
    private long Audio_Used_Memory_lastValue;
    private long GC_Reserved_Memory_lastValue;
    private long GC_Used_Memory_lastValue;
    private long Profiler_Reserved_Memory_lastValue;
    private long Profiler_Used_Memory_lastValue;
    private long System_Total_Used_lastValue;
    private long System_Used_Memory_lastValue;
    private long Total_Reserved_Memory_lastValue;
    private long Total_Used_Memory_lastValue;
    private long Video_Reserved_Memory_lastValue;
    private long Video_Used_Memory_lastValue;

    private long Batches_Count_lastValue;
    private long CPU_Main_Thread_Frame_Time_lastValue;
    private long CPU_Render_Thread_Frame_Time_lastValue;
    private long CPU_Total_Frame_Time_lastValue;
    private long Draw_Calls_Count_lastValue;
    private long GPU_Frame_Time_lastValue;
    private long Index_Buffer_Upload_In_Frame_Bytes_lastValue;
    private long Index_Buffer_Upload_In_Frame_Count_lastValue;
    private long Bytes_lastValue;
    private long Render_Textures_Changes_Count_lastValue;
    private long Render_Textures_Count_lastValue;
    private long SetPass_Calls_Count_lastValue;
    private long Shadow_Casters_Count_lastValue;
    private long Triangles_Count_lastValue;
    private long Used_Buffers_Bytes_lastValue;
    private long Used_Buffers_Count_lastValue;
    private long Vertex_Buffer_Upload_In_Frame_Bytes_lastValue;
    private long Vertex_Buffer_Upload_In_Frame_Count_lastValue;
    private long Vertices_Count_lastValue;
    private long Video_Memory_Bytes_lastValue;
    private long Visible_Skinned_Meshes_Count_lastValue;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        App_Committed_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "App Committed Memory");
        App_Resident_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "App Resident Memory");
        Audio_Reserved_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Audio Reserved Memory");
        Audio_Used_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Audio Used Memory");
        GC_Reserved_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        GC_Used_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Used Memory");
        Profiler_Reserved_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Profiler Reserved Memory");
        Profiler_Used_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Profiler Used Memory");
        System_Total_Used_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Total Used");
        System_Used_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        Total_Reserved_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
        Total_Used_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
        Video_Reserved_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Video Reserved Memory");
        Video_Used_Memory_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Video Used Memory");


        Batches_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
        CPU_Main_Thread_Frame_Time_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "CPU Main Thread Frame Time");
        CPU_Render_Thread_Frame_Time_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "CPU Render Thread Frame Time");
        CPU_Total_Frame_Time_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "CPU Total Frame Time");
        Draw_Calls_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        GPU_Frame_Time_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "GPU Frame Time");
        Index_Buffer_Upload_In_Frame_Bytes_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Index Buffer Upload In Frame Bytes");
        Index_Buffer_Upload_In_Frame_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Index Buffer Upload In Frame Count");
        Bytes_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Bytes");
        Render_Textures_Changes_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Render Textures Changes Count");
        Render_Textures_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Render Textures Count");
        SetPass_Calls_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
        Shadow_Casters_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Shadow Casters Count");
        Triangles_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
        Used_Buffers_Bytes_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Used Buffers Bytes");
        Used_Buffers_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Used Buffers Count");
        Vertex_Buffer_Upload_In_Frame_Bytes_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertex Buffer Upload In Frame Bytes");
        Vertex_Buffer_Upload_In_Frame_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertex Buffer Upload In Frame Count");
        Vertices_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        Video_Memory_Bytes_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Video Memory Bytes");
        Visible_Skinned_Meshes_Count_Recorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Visible Skinned Meshes Count");
 

    }

    void OnDisable()
    {

        App_Committed_Memory_Recorder.Dispose();
        App_Resident_Memory_Recorder.Dispose();
        Audio_Reserved_Memory_Recorder.Dispose();
        Audio_Used_Memory_Recorder.Dispose();
        GC_Reserved_Memory_Recorder.Dispose();
        GC_Used_Memory_Recorder.Dispose();
        Profiler_Reserved_Memory_Recorder.Dispose();
        Profiler_Used_Memory_Recorder.Dispose();
        System_Total_Used_Recorder.Dispose();
        System_Used_Memory_Recorder.Dispose();
        Total_Reserved_Memory_Recorder.Dispose();
        Total_Used_Memory_Recorder.Dispose();
        Video_Reserved_Memory_Recorder.Dispose();
        Video_Used_Memory_Recorder.Dispose();

        Batches_Count_Recorder.Dispose();
        CPU_Main_Thread_Frame_Time_Recorder.Dispose();
        CPU_Render_Thread_Frame_Time_Recorder.Dispose();
        CPU_Total_Frame_Time_Recorder.Dispose();
        Draw_Calls_Count_Recorder.Dispose();
        GPU_Frame_Time_Recorder.Dispose();
        Index_Buffer_Upload_In_Frame_Bytes_Recorder.Dispose();
        Index_Buffer_Upload_In_Frame_Count_Recorder.Dispose();
        Bytes_Recorder.Dispose();
        Render_Textures_Changes_Count_Recorder.Dispose();
        Render_Textures_Count_Recorder.Dispose();
        SetPass_Calls_Count_Recorder.Dispose();
        Shadow_Casters_Count_Recorder.Dispose();
        Triangles_Count_Recorder.Dispose();
        Used_Buffers_Bytes_Recorder.Dispose();
        Used_Buffers_Count_Recorder.Dispose();
        Vertex_Buffer_Upload_In_Frame_Bytes_Recorder.Dispose();
        Vertex_Buffer_Upload_In_Frame_Count_Recorder.Dispose();
        Vertices_Count_Recorder.Dispose();
        Video_Memory_Bytes_Recorder.Dispose();
        Visible_Skinned_Meshes_Count_Recorder.Dispose();

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
       App_Committed_Memory_lastValue = App_Committed_Memory_Recorder.LastValue;
       App_Resident_Memory_lastValue = App_Resident_Memory_Recorder.LastValue;
       Audio_Reserved_Memory_lastValue = Audio_Reserved_Memory_Recorder.LastValue;
       Audio_Used_Memory_lastValue = Audio_Used_Memory_Recorder.LastValue;
       GC_Reserved_Memory_lastValue = GC_Reserved_Memory_Recorder.LastValue;
       GC_Used_Memory_lastValue = GC_Used_Memory_Recorder.LastValue;
       Profiler_Reserved_Memory_lastValue = Profiler_Reserved_Memory_Recorder.LastValue;
       Profiler_Used_Memory_lastValue = Profiler_Used_Memory_Recorder.LastValue;
       System_Total_Used_lastValue = System_Total_Used_Recorder.LastValue;
       System_Used_Memory_lastValue = System_Used_Memory_Recorder.LastValue;
       Total_Reserved_Memory_lastValue = Total_Reserved_Memory_Recorder.LastValue;
       Total_Used_Memory_lastValue = Total_Used_Memory_Recorder.LastValue;
       Video_Reserved_Memory_lastValue = Video_Reserved_Memory_Recorder.LastValue;
       Video_Used_Memory_lastValue = Video_Used_Memory_Recorder.LastValue;
       Batches_Count_lastValue = Batches_Count_Recorder.LastValue;
       CPU_Main_Thread_Frame_Time_lastValue = CPU_Main_Thread_Frame_Time_Recorder.LastValue;
       CPU_Render_Thread_Frame_Time_lastValue = CPU_Render_Thread_Frame_Time_Recorder.LastValue;
       CPU_Total_Frame_Time_lastValue = CPU_Total_Frame_Time_Recorder.LastValue;
       Draw_Calls_Count_lastValue = Draw_Calls_Count_Recorder.LastValue;
       GPU_Frame_Time_lastValue = GPU_Frame_Time_Recorder.LastValue;
       Index_Buffer_Upload_In_Frame_Bytes_lastValue = Index_Buffer_Upload_In_Frame_Bytes_Recorder.LastValue;
       Index_Buffer_Upload_In_Frame_Count_lastValue = Index_Buffer_Upload_In_Frame_Count_Recorder.LastValue;
       Bytes_lastValue = Bytes_Recorder.LastValue;
       Render_Textures_Changes_Count_lastValue = Render_Textures_Changes_Count_Recorder.LastValue;
       Render_Textures_Count_lastValue = Render_Textures_Count_Recorder.LastValue;
       SetPass_Calls_Count_lastValue = SetPass_Calls_Count_Recorder.LastValue;
       Shadow_Casters_Count_lastValue = Shadow_Casters_Count_Recorder.LastValue;
       Triangles_Count_lastValue = Triangles_Count_Recorder.LastValue;
       Used_Buffers_Bytes_lastValue = Used_Buffers_Bytes_Recorder.LastValue;
       Used_Buffers_Count_lastValue = Used_Buffers_Count_Recorder.LastValue;
       Vertex_Buffer_Upload_In_Frame_Bytes_lastValue = Vertex_Buffer_Upload_In_Frame_Bytes_Recorder.LastValue;
       Vertex_Buffer_Upload_In_Frame_Count_lastValue = Vertex_Buffer_Upload_In_Frame_Count_Recorder.LastValue;
       Vertices_Count_lastValue = Vertices_Count_Recorder.LastValue;
       Video_Memory_Bytes_lastValue = Video_Memory_Bytes_Recorder.LastValue;
        Visible_Skinned_Meshes_Count_lastValue = Visible_Skinned_Meshes_Count_Recorder.LastValue;
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
        UpdateMetricWithTime(App_Committed_Memory_Metrics, App_Committed_Memory_lastValue);
        UpdateMetricWithTime(App_Resident_Memory_Metrics, App_Resident_Memory_lastValue);
        UpdateMetricWithTime(Audio_Reserved_Memory_Metrics, Audio_Reserved_Memory_lastValue);
        UpdateMetricWithTime(Audio_Used_Memory_Metrics, Audio_Used_Memory_lastValue);
        UpdateMetricWithTime(GC_Reserved_Memory_Metrics, GC_Reserved_Memory_lastValue);
        UpdateMetricWithTime(GC_Used_Memory_Metrics, GC_Used_Memory_lastValue);
        UpdateMetricWithTime(Profiler_Reserved_Memory_Metrics, Profiler_Reserved_Memory_lastValue);
        UpdateMetricWithTime(Profiler_Used_Memory_Metrics, Profiler_Used_Memory_lastValue);
        UpdateMetricWithTime(System_Total_Used_Metrics, System_Total_Used_lastValue);
        UpdateMetricWithTime(System_Used_Memory_Metrics, System_Used_Memory_lastValue);
        UpdateMetricWithTime(Total_Reserved_Memory_Metrics, Total_Reserved_Memory_lastValue);
        UpdateMetricWithTime(Total_Used_Memory_Metrics, Total_Used_Memory_lastValue);
        UpdateMetricWithTime(Video_Reserved_Memory_Metrics, Video_Reserved_Memory_lastValue);
        UpdateMetricWithTime(Video_Used_Memory_Metrics, Video_Used_Memory_lastValue);
        UpdateMetricWithTime(Batches_Count_Metrics, Batches_Count_lastValue);
        UpdateMetricWithTime(CPU_Main_Thread_Frame_Time_Metrics, CPU_Main_Thread_Frame_Time_lastValue);
        UpdateMetricWithTime(CPU_Render_Thread_Frame_Time_Metrics, CPU_Render_Thread_Frame_Time_lastValue);
        UpdateMetricWithTime(CPU_Total_Frame_Time_Metrics, CPU_Total_Frame_Time_lastValue);
        UpdateMetricWithTime(Draw_Calls_Count_Metrics, Draw_Calls_Count_lastValue);
        UpdateMetricWithTime(GPU_Frame_Time_Metrics, GPU_Frame_Time_lastValue);
        UpdateMetricWithTime(Index_Buffer_Upload_In_Frame_Bytes_Metrics, Index_Buffer_Upload_In_Frame_Bytes_lastValue);
        UpdateMetricWithTime(Index_Buffer_Upload_In_Frame_Count_Metrics, Index_Buffer_Upload_In_Frame_Count_lastValue);
        UpdateMetricWithTime(Bytes_Metrics, Bytes_lastValue);
        UpdateMetricWithTime(Render_Textures_Changes_Count_Metrics, Render_Textures_Changes_Count_lastValue);
        UpdateMetricWithTime(Render_Textures_Count_Metrics, Render_Textures_Count_lastValue);
        UpdateMetricWithTime(SetPass_Calls_Count_Metrics, SetPass_Calls_Count_lastValue);
        UpdateMetricWithTime(Shadow_Casters_Count_Metrics, Shadow_Casters_Count_lastValue);
        UpdateMetricWithTime(Triangles_Count_Metrics, Triangles_Count_lastValue);
        UpdateMetricWithTime(Used_Buffers_Bytes_Metrics, Used_Buffers_Bytes_lastValue);
        UpdateMetricWithTime(Used_Buffers_Count_Metrics, Used_Buffers_Count_lastValue);
        UpdateMetricWithTime(Vertex_Buffer_Upload_In_Frame_Bytes_Metrics, Vertex_Buffer_Upload_In_Frame_Bytes_lastValue);
        UpdateMetricWithTime(Vertex_Buffer_Upload_In_Frame_Count_Metrics, Vertex_Buffer_Upload_In_Frame_Count_lastValue);
        UpdateMetricWithTime(Vertices_Count_Metrics, Vertices_Count_lastValue);
        UpdateMetricWithTime(Video_Memory_Bytes_Metrics, Video_Memory_Bytes_lastValue);
        UpdateMetricWithTime(Visible_Skinned_Meshes_Count_Metrics, Visible_Skinned_Meshes_Count_lastValue);
    }

    private static void UpdateMetricWithTime(AdroitProfiler_StateMetrics aMetric, float value)
    {
        aMetric.MaxValueInLast_TenthSecond = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_TenthSecond, value);
        aMetric.MaxValueInLast_QuarterSecond = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_QuarterSecond, value);
        aMetric.MaxValueInLast_HalfSecond = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_HalfSecond, value);
        aMetric.MaxValueInLast_5Seconds = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_5Seconds, value);
        aMetric.MaxValueInLast_10Seconds = AdroitProfiler_Service.UpdateMetric(aMetric.MaxValueInLast_10Seconds, value);
    }

    private void OnTenthHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_TenthSecond = 1;
        TimePerFrame_Metrics.MaxValueInLast_TenthSecond = timeNow;
        TotalTimeForFPSAvg_TenthSecond = timeNow;

        App_Committed_Memory_Metrics.MaxValueInLast_TenthSecond = App_Committed_Memory_lastValue;
        App_Resident_Memory_Metrics.MaxValueInLast_TenthSecond = App_Resident_Memory_lastValue;
        Audio_Reserved_Memory_Metrics.MaxValueInLast_TenthSecond = Audio_Reserved_Memory_lastValue;
        Audio_Used_Memory_Metrics.MaxValueInLast_TenthSecond = Audio_Used_Memory_lastValue;
        GC_Reserved_Memory_Metrics.MaxValueInLast_TenthSecond = GC_Reserved_Memory_lastValue;
        GC_Used_Memory_Metrics.MaxValueInLast_TenthSecond = GC_Used_Memory_lastValue;
        Profiler_Reserved_Memory_Metrics.MaxValueInLast_TenthSecond = Profiler_Reserved_Memory_lastValue;
        Profiler_Used_Memory_Metrics.MaxValueInLast_TenthSecond = Profiler_Used_Memory_lastValue;
        System_Total_Used_Metrics.MaxValueInLast_TenthSecond = System_Total_Used_lastValue;
        System_Used_Memory_Metrics.MaxValueInLast_TenthSecond = System_Used_Memory_lastValue;
        Total_Reserved_Memory_Metrics.MaxValueInLast_TenthSecond = Total_Reserved_Memory_lastValue;
        Total_Used_Memory_Metrics.MaxValueInLast_TenthSecond = Total_Used_Memory_lastValue;
        Video_Reserved_Memory_Metrics.MaxValueInLast_TenthSecond = Video_Reserved_Memory_lastValue;
        Video_Used_Memory_Metrics.MaxValueInLast_TenthSecond = Video_Used_Memory_lastValue;
        Batches_Count_Metrics.MaxValueInLast_TenthSecond = Batches_Count_lastValue;
        CPU_Main_Thread_Frame_Time_Metrics.MaxValueInLast_TenthSecond = CPU_Main_Thread_Frame_Time_lastValue;
        CPU_Render_Thread_Frame_Time_Metrics.MaxValueInLast_TenthSecond = CPU_Render_Thread_Frame_Time_lastValue;
        CPU_Total_Frame_Time_Metrics.MaxValueInLast_TenthSecond = CPU_Total_Frame_Time_lastValue;
        Draw_Calls_Count_Metrics.MaxValueInLast_TenthSecond = Draw_Calls_Count_lastValue;
        GPU_Frame_Time_Metrics.MaxValueInLast_TenthSecond = GPU_Frame_Time_lastValue;
        Index_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_TenthSecond = Index_Buffer_Upload_In_Frame_Bytes_lastValue;
        Index_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_TenthSecond = Index_Buffer_Upload_In_Frame_Count_lastValue;
        Bytes_Metrics.MaxValueInLast_TenthSecond = Bytes_lastValue;
        Render_Textures_Changes_Count_Metrics.MaxValueInLast_TenthSecond = Render_Textures_Changes_Count_lastValue;
        Render_Textures_Count_Metrics.MaxValueInLast_TenthSecond = Render_Textures_Count_lastValue;
        SetPass_Calls_Count_Metrics.MaxValueInLast_TenthSecond = SetPass_Calls_Count_lastValue;
        Shadow_Casters_Count_Metrics.MaxValueInLast_TenthSecond = Shadow_Casters_Count_lastValue;
        Triangles_Count_Metrics.MaxValueInLast_TenthSecond = Triangles_Count_lastValue;
        Used_Buffers_Bytes_Metrics.MaxValueInLast_TenthSecond = Used_Buffers_Bytes_lastValue;
        Used_Buffers_Count_Metrics.MaxValueInLast_TenthSecond = Used_Buffers_Count_lastValue;
        Vertex_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_TenthSecond = Vertex_Buffer_Upload_In_Frame_Bytes_lastValue;
        Vertex_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_TenthSecond = Vertex_Buffer_Upload_In_Frame_Count_lastValue;
        Vertices_Count_Metrics.MaxValueInLast_TenthSecond = Vertices_Count_lastValue;
        Video_Memory_Bytes_Metrics.MaxValueInLast_TenthSecond = Video_Memory_Bytes_lastValue;
        Visible_Skinned_Meshes_Count_Metrics.MaxValueInLast_TenthSecond = Visible_Skinned_Meshes_Count_lastValue;

    }

    private void OnQuarterHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_QuarterSecond = 1;
        TotalTimeForFPSAvg_QuarterSecond = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_QuarterSecond = timeNow;

        App_Committed_Memory_Metrics.MaxValueInLast_QuarterSecond = App_Committed_Memory_lastValue;
        App_Resident_Memory_Metrics.MaxValueInLast_QuarterSecond = App_Resident_Memory_lastValue;
        Audio_Reserved_Memory_Metrics.MaxValueInLast_QuarterSecond = Audio_Reserved_Memory_lastValue;
        Audio_Used_Memory_Metrics.MaxValueInLast_QuarterSecond = Audio_Used_Memory_lastValue;
        GC_Reserved_Memory_Metrics.MaxValueInLast_QuarterSecond = GC_Reserved_Memory_lastValue;
        GC_Used_Memory_Metrics.MaxValueInLast_QuarterSecond = GC_Used_Memory_lastValue;
        Profiler_Reserved_Memory_Metrics.MaxValueInLast_QuarterSecond = Profiler_Reserved_Memory_lastValue;
        Profiler_Used_Memory_Metrics.MaxValueInLast_QuarterSecond = Profiler_Used_Memory_lastValue;
        System_Total_Used_Metrics.MaxValueInLast_QuarterSecond = System_Total_Used_lastValue;
        System_Used_Memory_Metrics.MaxValueInLast_QuarterSecond = System_Used_Memory_lastValue;
        Total_Reserved_Memory_Metrics.MaxValueInLast_QuarterSecond = Total_Reserved_Memory_lastValue;
        Total_Used_Memory_Metrics.MaxValueInLast_QuarterSecond = Total_Used_Memory_lastValue;
        Video_Reserved_Memory_Metrics.MaxValueInLast_QuarterSecond = Video_Reserved_Memory_lastValue;
        Video_Used_Memory_Metrics.MaxValueInLast_QuarterSecond = Video_Used_Memory_lastValue;
        Batches_Count_Metrics.MaxValueInLast_QuarterSecond = Batches_Count_lastValue;
        CPU_Main_Thread_Frame_Time_Metrics.MaxValueInLast_QuarterSecond = CPU_Main_Thread_Frame_Time_lastValue;
        CPU_Render_Thread_Frame_Time_Metrics.MaxValueInLast_QuarterSecond = CPU_Render_Thread_Frame_Time_lastValue;
        CPU_Total_Frame_Time_Metrics.MaxValueInLast_QuarterSecond = CPU_Total_Frame_Time_lastValue;
        Draw_Calls_Count_Metrics.MaxValueInLast_QuarterSecond = Draw_Calls_Count_lastValue;
        GPU_Frame_Time_Metrics.MaxValueInLast_QuarterSecond = GPU_Frame_Time_lastValue;
        Index_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_QuarterSecond = Index_Buffer_Upload_In_Frame_Bytes_lastValue;
        Index_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_QuarterSecond = Index_Buffer_Upload_In_Frame_Count_lastValue;
        Bytes_Metrics.MaxValueInLast_QuarterSecond = Bytes_lastValue;
        Render_Textures_Changes_Count_Metrics.MaxValueInLast_QuarterSecond = Render_Textures_Changes_Count_lastValue;
        Render_Textures_Count_Metrics.MaxValueInLast_QuarterSecond = Render_Textures_Count_lastValue;
        SetPass_Calls_Count_Metrics.MaxValueInLast_QuarterSecond = SetPass_Calls_Count_lastValue;
        Shadow_Casters_Count_Metrics.MaxValueInLast_QuarterSecond = Shadow_Casters_Count_lastValue;
        Triangles_Count_Metrics.MaxValueInLast_QuarterSecond = Triangles_Count_lastValue;
        Used_Buffers_Bytes_Metrics.MaxValueInLast_QuarterSecond = Used_Buffers_Bytes_lastValue;
        Used_Buffers_Count_Metrics.MaxValueInLast_QuarterSecond = Used_Buffers_Count_lastValue;
        Vertex_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_QuarterSecond = Vertex_Buffer_Upload_In_Frame_Bytes_lastValue;
        Vertex_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_QuarterSecond = Vertex_Buffer_Upload_In_Frame_Count_lastValue;
        Vertices_Count_Metrics.MaxValueInLast_QuarterSecond = Vertices_Count_lastValue;
        Video_Memory_Bytes_Metrics.MaxValueInLast_QuarterSecond = Video_Memory_Bytes_lastValue;
        Visible_Skinned_Meshes_Count_Metrics.MaxValueInLast_QuarterSecond = Visible_Skinned_Meshes_Count_lastValue;


    }

    private void OnHalfHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_HalfSecond = 1;
        TotalTimeForFPSAvg_HalfSecond = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_HalfSecond = timeNow;

        App_Committed_Memory_Metrics.MaxValueInLast_HalfSecond = App_Committed_Memory_lastValue;
        App_Resident_Memory_Metrics.MaxValueInLast_HalfSecond = App_Resident_Memory_lastValue;
        Audio_Reserved_Memory_Metrics.MaxValueInLast_HalfSecond = Audio_Reserved_Memory_lastValue;
        Audio_Used_Memory_Metrics.MaxValueInLast_HalfSecond = Audio_Used_Memory_lastValue;
        GC_Reserved_Memory_Metrics.MaxValueInLast_HalfSecond = GC_Reserved_Memory_lastValue;
        GC_Used_Memory_Metrics.MaxValueInLast_HalfSecond = GC_Used_Memory_lastValue;
        Profiler_Reserved_Memory_Metrics.MaxValueInLast_HalfSecond = Profiler_Reserved_Memory_lastValue;
        Profiler_Used_Memory_Metrics.MaxValueInLast_HalfSecond = Profiler_Used_Memory_lastValue;
        System_Total_Used_Metrics.MaxValueInLast_HalfSecond = System_Total_Used_lastValue;
        System_Used_Memory_Metrics.MaxValueInLast_HalfSecond = System_Used_Memory_lastValue;
        Total_Reserved_Memory_Metrics.MaxValueInLast_HalfSecond = Total_Reserved_Memory_lastValue;
        Total_Used_Memory_Metrics.MaxValueInLast_HalfSecond = Total_Used_Memory_lastValue;
        Video_Reserved_Memory_Metrics.MaxValueInLast_HalfSecond = Video_Reserved_Memory_lastValue;
        Video_Used_Memory_Metrics.MaxValueInLast_HalfSecond = Video_Used_Memory_lastValue;
        Batches_Count_Metrics.MaxValueInLast_HalfSecond = Batches_Count_lastValue;
        CPU_Main_Thread_Frame_Time_Metrics.MaxValueInLast_HalfSecond = CPU_Main_Thread_Frame_Time_lastValue;
        CPU_Render_Thread_Frame_Time_Metrics.MaxValueInLast_HalfSecond = CPU_Render_Thread_Frame_Time_lastValue;
        CPU_Total_Frame_Time_Metrics.MaxValueInLast_HalfSecond = CPU_Total_Frame_Time_lastValue;
        Draw_Calls_Count_Metrics.MaxValueInLast_HalfSecond = Draw_Calls_Count_lastValue;
        GPU_Frame_Time_Metrics.MaxValueInLast_HalfSecond = GPU_Frame_Time_lastValue;
        Index_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_HalfSecond = Index_Buffer_Upload_In_Frame_Bytes_lastValue;
        Index_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_HalfSecond = Index_Buffer_Upload_In_Frame_Count_lastValue;
        Bytes_Metrics.MaxValueInLast_HalfSecond = Bytes_lastValue;
        Render_Textures_Changes_Count_Metrics.MaxValueInLast_HalfSecond = Render_Textures_Changes_Count_lastValue;
        Render_Textures_Count_Metrics.MaxValueInLast_HalfSecond = Render_Textures_Count_lastValue;
        SetPass_Calls_Count_Metrics.MaxValueInLast_HalfSecond = SetPass_Calls_Count_lastValue;
        Shadow_Casters_Count_Metrics.MaxValueInLast_HalfSecond = Shadow_Casters_Count_lastValue;
        Triangles_Count_Metrics.MaxValueInLast_HalfSecond = Triangles_Count_lastValue;
        Used_Buffers_Bytes_Metrics.MaxValueInLast_HalfSecond = Used_Buffers_Bytes_lastValue;
        Used_Buffers_Count_Metrics.MaxValueInLast_HalfSecond = Used_Buffers_Count_lastValue;
        Vertex_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_HalfSecond = Vertex_Buffer_Upload_In_Frame_Bytes_lastValue;
        Vertex_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_HalfSecond = Vertex_Buffer_Upload_In_Frame_Count_lastValue;
        Vertices_Count_Metrics.MaxValueInLast_HalfSecond = Vertices_Count_lastValue;
        Video_Memory_Bytes_Metrics.MaxValueInLast_HalfSecond = Video_Memory_Bytes_lastValue;
        Visible_Skinned_Meshes_Count_Metrics.MaxValueInLast_HalfSecond = Visible_Skinned_Meshes_Count_lastValue;
    }

    private void On5SecondHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_5Seconds = 1;
        TotalTimeForFPSAvg_5Seconds = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_5Seconds = timeNow;

        App_Committed_Memory_Metrics.MaxValueInLast_5Seconds = App_Committed_Memory_lastValue;
        App_Resident_Memory_Metrics.MaxValueInLast_5Seconds = App_Resident_Memory_lastValue;
        Audio_Reserved_Memory_Metrics.MaxValueInLast_5Seconds = Audio_Reserved_Memory_lastValue;
        Audio_Used_Memory_Metrics.MaxValueInLast_5Seconds = Audio_Used_Memory_lastValue;
        GC_Reserved_Memory_Metrics.MaxValueInLast_5Seconds = GC_Reserved_Memory_lastValue;
        GC_Used_Memory_Metrics.MaxValueInLast_5Seconds = GC_Used_Memory_lastValue;
        Profiler_Reserved_Memory_Metrics.MaxValueInLast_5Seconds = Profiler_Reserved_Memory_lastValue;
        Profiler_Used_Memory_Metrics.MaxValueInLast_5Seconds = Profiler_Used_Memory_lastValue;
        System_Total_Used_Metrics.MaxValueInLast_5Seconds = System_Total_Used_lastValue;
        System_Used_Memory_Metrics.MaxValueInLast_5Seconds = System_Used_Memory_lastValue;
        Total_Reserved_Memory_Metrics.MaxValueInLast_5Seconds = Total_Reserved_Memory_lastValue;
        Total_Used_Memory_Metrics.MaxValueInLast_5Seconds = Total_Used_Memory_lastValue;
        Video_Reserved_Memory_Metrics.MaxValueInLast_5Seconds = Video_Reserved_Memory_lastValue;
        Video_Used_Memory_Metrics.MaxValueInLast_5Seconds = Video_Used_Memory_lastValue;
        Batches_Count_Metrics.MaxValueInLast_5Seconds = Batches_Count_lastValue;
        CPU_Main_Thread_Frame_Time_Metrics.MaxValueInLast_5Seconds = CPU_Main_Thread_Frame_Time_lastValue;
        CPU_Render_Thread_Frame_Time_Metrics.MaxValueInLast_5Seconds = CPU_Render_Thread_Frame_Time_lastValue;
        CPU_Total_Frame_Time_Metrics.MaxValueInLast_5Seconds = CPU_Total_Frame_Time_lastValue;
        Draw_Calls_Count_Metrics.MaxValueInLast_5Seconds = Draw_Calls_Count_lastValue;
        GPU_Frame_Time_Metrics.MaxValueInLast_5Seconds = GPU_Frame_Time_lastValue;
        Index_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_5Seconds = Index_Buffer_Upload_In_Frame_Bytes_lastValue;
        Index_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_5Seconds = Index_Buffer_Upload_In_Frame_Count_lastValue;
        Bytes_Metrics.MaxValueInLast_5Seconds = Bytes_lastValue;
        Render_Textures_Changes_Count_Metrics.MaxValueInLast_5Seconds = Render_Textures_Changes_Count_lastValue;
        Render_Textures_Count_Metrics.MaxValueInLast_5Seconds = Render_Textures_Count_lastValue;
        SetPass_Calls_Count_Metrics.MaxValueInLast_5Seconds = SetPass_Calls_Count_lastValue;
        Shadow_Casters_Count_Metrics.MaxValueInLast_5Seconds = Shadow_Casters_Count_lastValue;
        Triangles_Count_Metrics.MaxValueInLast_5Seconds = Triangles_Count_lastValue;
        Used_Buffers_Bytes_Metrics.MaxValueInLast_5Seconds = Used_Buffers_Bytes_lastValue;
        Used_Buffers_Count_Metrics.MaxValueInLast_5Seconds = Used_Buffers_Count_lastValue;
        Vertex_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_5Seconds = Vertex_Buffer_Upload_In_Frame_Bytes_lastValue;
        Vertex_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_5Seconds = Vertex_Buffer_Upload_In_Frame_Count_lastValue;
        Vertices_Count_Metrics.MaxValueInLast_5Seconds = Vertices_Count_lastValue;
        Video_Memory_Bytes_Metrics.MaxValueInLast_5Seconds = Video_Memory_Bytes_lastValue;
        Visible_Skinned_Meshes_Count_Metrics.MaxValueInLast_5Seconds = Visible_Skinned_Meshes_Count_lastValue;

    }

    private void On10SecondHeartbeat()
    {
        var timeNow = Mathf.FloorToInt(Time.unscaledDeltaTime * 1000.0f);
        NumberOfFramesThis_10Seconds = 1;
        TotalTimeForFPSAvg_10Seconds = timeNow;
        TimePerFrame_Metrics.MaxValueInLast_10Seconds = timeNow;

        App_Committed_Memory_Metrics.MaxValueInLast_10Seconds = App_Committed_Memory_lastValue;
        App_Resident_Memory_Metrics.MaxValueInLast_10Seconds = App_Resident_Memory_lastValue;
        Audio_Reserved_Memory_Metrics.MaxValueInLast_10Seconds = Audio_Reserved_Memory_lastValue;
        Audio_Used_Memory_Metrics.MaxValueInLast_10Seconds = Audio_Used_Memory_lastValue;
        GC_Reserved_Memory_Metrics.MaxValueInLast_10Seconds = GC_Reserved_Memory_lastValue;
        GC_Used_Memory_Metrics.MaxValueInLast_10Seconds = GC_Used_Memory_lastValue;
        Profiler_Reserved_Memory_Metrics.MaxValueInLast_10Seconds = Profiler_Reserved_Memory_lastValue;
        Profiler_Used_Memory_Metrics.MaxValueInLast_10Seconds = Profiler_Used_Memory_lastValue;
        System_Total_Used_Metrics.MaxValueInLast_10Seconds = System_Total_Used_lastValue;
        System_Used_Memory_Metrics.MaxValueInLast_10Seconds = System_Used_Memory_lastValue;
        Total_Reserved_Memory_Metrics.MaxValueInLast_10Seconds = Total_Reserved_Memory_lastValue;
        Total_Used_Memory_Metrics.MaxValueInLast_10Seconds = Total_Used_Memory_lastValue;
        Video_Reserved_Memory_Metrics.MaxValueInLast_10Seconds = Video_Reserved_Memory_lastValue;
        Video_Used_Memory_Metrics.MaxValueInLast_10Seconds = Video_Used_Memory_lastValue;
        Batches_Count_Metrics.MaxValueInLast_10Seconds = Batches_Count_lastValue;
        CPU_Main_Thread_Frame_Time_Metrics.MaxValueInLast_10Seconds = CPU_Main_Thread_Frame_Time_lastValue;
        CPU_Render_Thread_Frame_Time_Metrics.MaxValueInLast_10Seconds = CPU_Render_Thread_Frame_Time_lastValue;
        CPU_Total_Frame_Time_Metrics.MaxValueInLast_10Seconds = CPU_Total_Frame_Time_lastValue;
        Draw_Calls_Count_Metrics.MaxValueInLast_10Seconds = Draw_Calls_Count_lastValue;
        GPU_Frame_Time_Metrics.MaxValueInLast_10Seconds = GPU_Frame_Time_lastValue;
        Index_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_10Seconds = Index_Buffer_Upload_In_Frame_Bytes_lastValue;
        Index_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_10Seconds = Index_Buffer_Upload_In_Frame_Count_lastValue;
        Bytes_Metrics.MaxValueInLast_10Seconds = Bytes_lastValue;
        Render_Textures_Changes_Count_Metrics.MaxValueInLast_10Seconds = Render_Textures_Changes_Count_lastValue;
        Render_Textures_Count_Metrics.MaxValueInLast_10Seconds = Render_Textures_Count_lastValue;
        SetPass_Calls_Count_Metrics.MaxValueInLast_10Seconds = SetPass_Calls_Count_lastValue;
        Shadow_Casters_Count_Metrics.MaxValueInLast_10Seconds = Shadow_Casters_Count_lastValue;
        Triangles_Count_Metrics.MaxValueInLast_10Seconds = Triangles_Count_lastValue;
        Used_Buffers_Bytes_Metrics.MaxValueInLast_10Seconds = Used_Buffers_Bytes_lastValue;
        Used_Buffers_Count_Metrics.MaxValueInLast_10Seconds = Used_Buffers_Count_lastValue;
        Vertex_Buffer_Upload_In_Frame_Bytes_Metrics.MaxValueInLast_10Seconds = Vertex_Buffer_Upload_In_Frame_Bytes_lastValue;
        Vertex_Buffer_Upload_In_Frame_Count_Metrics.MaxValueInLast_10Seconds = Vertex_Buffer_Upload_In_Frame_Count_lastValue;
        Vertices_Count_Metrics.MaxValueInLast_10Seconds = Vertices_Count_lastValue;
        Video_Memory_Bytes_Metrics.MaxValueInLast_10Seconds = Video_Memory_Bytes_lastValue;
        Visible_Skinned_Meshes_Count_Metrics.MaxValueInLast_10Seconds = Visible_Skinned_Meshes_Count_lastValue;

    }

    private void UpdateGPUStats()
    {
        //TODO fix this
      // GPUStats = AdroitProfiler_Service.UpdateGPUStats(drawCallsCountRecorder_lastValue, polyCountRecorder_lastValue);
    }
}




