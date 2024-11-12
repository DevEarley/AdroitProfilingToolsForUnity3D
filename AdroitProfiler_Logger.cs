
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_Logger : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void saveJsonLog(string jsonLogData);

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void downloadToFile(string content, string filename);
#endif

    public readonly static string ProfileHeader_Times = " 10s,  5s,  0.5s, 0.25s, 0.1s, ";
    public readonly static string ProfileTitles = " , , ,Longest Frame Time,,,,," +
                                                  "Average FPS,,,,," +
                                                 // "App Committed Memory,,,,," +
                                                 // "App Resident Memory,,,,," +
                                                 // "Audio Reserved Memory,,,,," +
                                                 // "Audio Used Memory,,,,," +
                                                  "GC Reserved Memory,,,,," +
                                                  "GC Used Memory,,,,," +
                                                  "Profiler Reserved Memory,,,,," +
                                                  "Profiler Used Memory,,,,," +
                                                 // "System Total Used ,,,,," +
                                                 // "System Used Memory,,,,," +
                                                  "Total Reserved Memory,,,,," +
                                                  "Total Used Memory,,,,," +
                                                  // "Video Reserved Memory,,,,," +
                                                  // /"Video Used Memory,,,,," +
                                                  "Batches Count  ,,,,," +
                                                  "CPU Main Thread Frame Time,,,,," +
                                                  "CPU Render Thread Frame Time,,,,," +
                                                  "CPU Total Frame Time,,,,," +
                                                  "Draw Calls Count,,,,," +
                                                  // "GPU Frame Time,,,,," +
                                                  "Index Buffer Upload In Frame Bytes,,,,," +
                                                  "Index Buffer Upload In Frame Count,,,,," +
                                                  // "Bytes ,,,,," +
                                                  "Render Textures Changes Count,,,,," +
                                                  "Render Textures Count,,,,," +
                                                  "SetPass Calls Count,,,,," +
                                                 // "Shadow Casters Count,,,,," +
                                                  "Triangles Count ,,,,," +
                                                  "Used Buffers Bytes,,,,," +
                                                  "Used Buffers Count,,,,," +
                                                  "Vertex Buffer Upload In Frame Bytes,,,,," +
                                                  "Vertex Buffer Upload In Frame Count ,,,,," +
                                                  "Vertices Count ,,,,," +
                                                  "Video Memory Bytes,,,,," +
                                                  "Visible Skinned Meshes Count \n";

    public readonly static string ProfileHeader = "Scene Time, Scene Name, Event Description, " +
                                    ProfileHeader_Times + //LFT
                                    ProfileHeader_Times + //FPS
                                   // ProfileHeader_Times + // App Committed Memory
                                   // ProfileHeader_Times + // App Resident Memory
                                   // ProfileHeader_Times + // Audio Reserved Memory
                                   // ProfileHeader_Times + // Audio Used Memory
                                    ProfileHeader_Times + // GC Reserved Memory
                                    ProfileHeader_Times + // GC Used Memory
                                    ProfileHeader_Times + // Profiler Reserved Memory
                                    ProfileHeader_Times + // Profiler Used Memory
                                   // ProfileHeader_Times + // System Total Used
                                   // ProfileHeader_Times + // System Used Memory
                                    ProfileHeader_Times + // Total Reserved Memory
                                    ProfileHeader_Times + // Total Used Memory
                                   // ProfileHeader_Times + // Video Reserved Memory
                                   // ProfileHeader_Times + // Video Used Memory
                                    ProfileHeader_Times + // Batches Count
                                    ProfileHeader_Times + // CPU Main Thread Frame Time
                                    ProfileHeader_Times + // CPU Render Thread Frame Time
                                    ProfileHeader_Times + // CPU Total Frame Time
                                    ProfileHeader_Times + // Draw Calls Count
                                    // ProfileHeader_Times + // GPU Frame Time
                                    ProfileHeader_Times + // Index Buffer Upload In Frame Bytes
                                    ProfileHeader_Times + // Index Buffer Upload In Frame Count
                                    // ProfileHeader_Times + // Bytes
                                    ProfileHeader_Times + // Render Textures Changes Count
                                    ProfileHeader_Times + // Render Textures Count
                                    ProfileHeader_Times + // SetPass Calls Count
                                    // ProfileHeader_Times + // Shadow Casters Count
                                    ProfileHeader_Times + // Triangles Count
                                    ProfileHeader_Times + // Used Buffers Bytes
                                    ProfileHeader_Times + // Used Buffers Count
                                    ProfileHeader_Times + // Vertex Buffer Upload In Frame Bytes
                                    ProfileHeader_Times + // Vertex Buffer Upload In Frame Count
                                    ProfileHeader_Times + // Vertices Count
                                    ProfileHeader_Times + // Video Memory Bytes
                                    ProfileHeader_Times + "\n"; // Visible Skinned Meshes Count




    [HideInInspector]
    public int CurrentRunIndex = 0;
    public Dictionary<int, List<string>> Runs = new Dictionary<int, List<string>>();

    private AdroitProfiler_State AdroitProfiler_State;

    void Start()
    {
        AdroitProfiler_State = this.gameObject.GetComponent<AdroitProfiler_State>();
        CurrentRunIndex = 0;
        Runs.Add(CurrentRunIndex, new List<string>());
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded_Static;
    }

    private static void OnSceneLoaded_Static(Scene scene, LoadSceneMode mode)
    {
        var _this = GameObject.FindObjectOfType<AdroitProfiler_Logger>();
        _this.OnScenLoaded(scene, mode);
    }

    private void OnScenLoaded(Scene scene, LoadSceneMode mode)
    {
        //TODO below is an example of why dictionary may be the wrong choice for this
        CurrentRunIndex++;
        if (Runs.ContainsKey(CurrentRunIndex))
        {
            CurrentRunIndex++;
        }
        if (Runs.ContainsKey(CurrentRunIndex) == false)
        {
            Runs.Add(CurrentRunIndex, new List<string>());
        }
        else // it still exists?!
        {
            CurrentRunIndex++;
            if (Runs.ContainsKey(CurrentRunIndex) == false) // LAST TRY!
            {
                Runs.Add(CurrentRunIndex, new List<string>());
            }
        }
    }

    public void CapturePerformanceForEvent(string eventDescription)
    {
        var formattedTime = AdroitProfiler_Service.FormatTime(Time.timeSinceLevelLoad);
        var performanceEventLog = "";
        performanceEventLog += formattedTime + ",";
        performanceEventLog += SceneManager.GetActiveScene().path + ", ";
        if (eventDescription == "" || eventDescription == null)
        {
            eventDescription = "Event @ " + formattedTime;
        }

        eventDescription.Replace(',', '|');
        eventDescription.Replace('\n', '|');
        eventDescription.Replace('\r', '|');
        performanceEventLog += eventDescription + ", ";

        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.TimePerFrame_Metrics);

        performanceEventLog += AdroitProfiler_State.AverageFPSFor_10Seconds + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_5Seconds + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_HalfSecond + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_QuarterSecond + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_TenthSecond + ", ";

       // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.App_Committed_Memory_Metrics);
       // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.App_Resident_Memory_Metrics);
       // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Audio_Reserved_Memory_Metrics);
       // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Audio_Used_Memory_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.GC_Reserved_Memory_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.GC_Used_Memory_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Profiler_Reserved_Memory_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Profiler_Used_Memory_Metrics);
        // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.System_Total_Used_Metrics);
        // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.System_Used_Memory_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Total_Reserved_Memory_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Total_Used_Memory_Metrics);
        // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Video_Reserved_Memory_Metrics);
        // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Video_Used_Memory_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Batches_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.CPU_Main_Thread_Frame_Time_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.CPU_Render_Thread_Frame_Time_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.CPU_Total_Frame_Time_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Draw_Calls_Count_Metrics);
        // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.GPU_Frame_Time_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Index_Buffer_Upload_In_Frame_Bytes_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Index_Buffer_Upload_In_Frame_Count_Metrics);
        // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Bytes_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Render_Textures_Changes_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Render_Textures_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.SetPass_Calls_Count_Metrics);
        // performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Shadow_Casters_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Triangles_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Used_Buffers_Bytes_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Used_Buffers_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Vertex_Buffer_Upload_In_Frame_Bytes_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Vertex_Buffer_Upload_In_Frame_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Vertices_Count_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Video_Memory_Bytes_Metrics);
        performanceEventLog = logMetricMaxTimes(performanceEventLog, AdroitProfiler_State.Visible_Skinned_Meshes_Count_Metrics);


        performanceEventLog += "\n";
        AddLogToRun(performanceEventLog);

        Debug.Log(performanceEventLog);

    }

    private static string logMetricMaxTimes(string performanceEventLog, AdroitProfiler_StateMetrics metric)
    {
        performanceEventLog += metric.MaxValueInLast_10Seconds + ", ";
        performanceEventLog += metric.MaxValueInLast_5Seconds + "  , ";
        performanceEventLog += metric.MaxValueInLast_TenthSecond + " , ";
        performanceEventLog += metric.MaxValueInLast_HalfSecond + " , ";
        performanceEventLog += metric.MaxValueInLast_QuarterSecond + " , ";
        return performanceEventLog;
    }

    private void AddLogToRun(string log)
    {
        if (Runs.ContainsKey(CurrentRunIndex) == false)
        {
            CurrentRunIndex++;
            Runs.Add(CurrentRunIndex, new List<string>());

        }
        Runs[CurrentRunIndex].Add(log);
    }

    public void Log(string message)
    {
        var performanceEventLog = "";
        performanceEventLog += DateTime.Today.ToString("MM/dd/yy HH:mm:ss") + ",";
        performanceEventLog += message + ",";
        performanceEventLog += "\n";
        AddLogToRun(performanceEventLog);

        Debug.Log(performanceEventLog);
    }

    public void LogTestCaseInfo(AdroitProfiler_AutomatedTester_Configuration_TestCase testCase)
    {

        var performanceEventLog = "";
        performanceEventLog += DateTime.Today.ToString("MM/dd/yy HH:mm:ss") + ",";
        performanceEventLog += testCase.name + ",";
        performanceEventLog += "\n";
        AddLogToRun(performanceEventLog);

        Debug.Log(performanceEventLog);
    }

    public void DownloadLogs()
    {
        SaveLogs();
    }

    public void SaveLogs()
    {
        string jsonLogData = "";
        jsonLogData += DateTime.Today.ToString("MM/dd/yy") + "\n";

        foreach (var run in Runs)
        {
            jsonLogData += ProfileTitles;

            jsonLogData += ProfileHeader;
            run.Value.ForEach(performanceEventLog =>
            {
                jsonLogData += performanceEventLog;
            });
        };
        Debug.Log("Saved JSON Data from Unity Project: " + jsonLogData);
#if UNITY_WEBGL && !UNITY_EDITOR
        downloadToFile(jsonLogData, "AdroitProfilier_Log");
#endif
    }
}

