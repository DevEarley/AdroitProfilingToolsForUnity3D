
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

    public readonly static string ProfileHeader_LFT = "Run,Time, Scene Name, Event Description, LFT: 10s, LFT: 5s, LFT: 0.5s, LFT: 0.25s, LFT: 0.1s, ";
    public readonly static string ProfileHeader_FPS = "FPS: 10s, FPS: 5s, FPS: 0.5s, FPS: 0.25s, FPS: 0.1s, ";
   // public readonly static string ProfileHeader_SystemMemory = "MEM: 10s, MEM: 5s, MEM: 0.5s, FPSMEM 0.25s, MEM: 0.1s, ";
    public readonly static string ProfileHeader_DrawsCount = "Draws: 10s, Draws: 5s, Draws: 0.5s, Draws: 0.25s, Draws: 0.1s, ";
    public readonly static string ProfileHeader_PolyCount = "Polys: 10s, Polys: 5s, Polys: 0.5s, Polys: 0.25s, Polys: 0.1s, \n ";
    //public  readonly static string  ProfileHeader =  ProfileHeader_LFT + ProfileHeader_FPS + ProfileHeader_SystemMemory  + ProfileHeader_DrawsCount + ProfileHeader_PolyCount;
    public  readonly static string  ProfileHeader =  ProfileHeader_LFT + ProfileHeader_FPS  + ProfileHeader_DrawsCount + ProfileHeader_PolyCount;

    public int CurrentRuntIndex = 0;
    public Dictionary<int,List<string>> Runs = new Dictionary<int, List<string>>();

    private AdroitProfiler_State AdroitProfiler_State;

    void Start()
    {
        AdroitProfiler_State = this.gameObject.GetComponent<AdroitProfiler_State>();
        Runs.Add(CurrentRuntIndex, new List<string>());
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
        CurrentRuntIndex++;
        AdroitProfiler_State.RunName = "Run #" + CurrentRuntIndex;
        Runs.Add(CurrentRuntIndex, new List<string>());
    }

    public void CapturePerformanceForEvent(string eventDescription)
    {
        if (AdroitProfiler_State.Paused) { return; }
     //  var MBDivisor = (1024 * 1024);
        var formattedTime = AdroitProfiler_Service.FormatTime(Time.time);
        var performanceEventLog = "";
        performanceEventLog += AdroitProfiler_State.RunName + ", ";
        performanceEventLog += formattedTime + ",";
        performanceEventLog += SceneManager.GetActiveScene().path+", ";
        if(eventDescription == "" || eventDescription == null){
            eventDescription = "Event @ "+ formattedTime;
        }
        performanceEventLog += eventDescription+ ", ";
        performanceEventLog += AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_10Seconds + ", ";
        performanceEventLog += AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_5Seconds + "  , " ;
        performanceEventLog += AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_TenthSecond + " , ";
        performanceEventLog += AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_HalfSecond + " , ";
        performanceEventLog += AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_QuarterSecond + " , ";

        performanceEventLog += AdroitProfiler_State.AverageFPSFor_10Seconds + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_5Seconds + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_HalfSecond + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_QuarterSecond + ", ";
        performanceEventLog += AdroitProfiler_State.AverageFPSFor_TenthSecond + ", ";

        //performanceEventLog += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_10Seconds / MBDivisor + "MB , ";
        //performanceEventLog += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_5Seconds / MBDivisor + "MB , ";
        //performanceEventLog += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_TenthSecond / MBDivisor + "MB , ";
        //performanceEventLog += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_HalfSecond / MBDivisor + "MB , ";
        //performanceEventLog += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_QuarterSecond / MBDivisor + "MB , ";

        performanceEventLog += AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_10Seconds + ", ";
        performanceEventLog += AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_5Seconds + "  , ";
        performanceEventLog += AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_TenthSecond + " , ";
        performanceEventLog += AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_HalfSecond + " , ";
        performanceEventLog += AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_QuarterSecond + " , ";

        performanceEventLog += AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_10Seconds + ", ";
        performanceEventLog += AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_5Seconds + "  , ";
        performanceEventLog += AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_TenthSecond + " , ";
        performanceEventLog += AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_HalfSecond + " , ";
        performanceEventLog += AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_QuarterSecond + " , ";

        performanceEventLog += "\n";
        Runs[CurrentRuntIndex].Add(performanceEventLog);
        Debug.Log(performanceEventLog);
    }

    public void SaveLogs()
    {
        string jsonLogData = "";
        foreach(var run in Runs)
        {
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

