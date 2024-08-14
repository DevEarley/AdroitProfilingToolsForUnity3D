using PixelCrushers.DialogueSystem;
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
    public readonly static string ProfileHeader_SystemMemory = "MEM: 10s, MEM: 5s, MEM: 0.5s, FPSMEM 0.25s, MEM: 0.1s, ";
    public readonly static string ProfileHeader_DrawsCount = "Draws: 10s, Draws: 5s, Draws: 0.5s, Draws: 0.25s, Draws: 0.1s, ";
    public readonly static string ProfileHeader_PolyCount = "Polys: 10s, Polys: 5s, Polys: 0.5s, Polys: 0.25s, Polys: 0.1s, \n ";
    public  readonly static string  ProfileHeader =  ProfileHeader_LFT + ProfileHeader_FPS + ProfileHeader_SystemMemory  + ProfileHeader_DrawsCount + ProfileHeader_PolyCount;
    public List<string> CurrentRun = new List<string>();
    
    public List<List<string>> Runs = new List<List<string>>();

    private AdroitProfiler_State AdroitProfiler;

    private bool InConversation = false;

    void Start()
    {
        AdroitProfiler = this.gameObject.GetComponent<AdroitProfiler_State>();
    }

     void Update()
    {
        if (DialogueManager.instance == null) return;
        if (InConversation != DialogueManager.instance.isConversationActive)
        {
            InConversation = DialogueManager.instance.isConversationActive;
            CapturePerformanceForEvent(DialogueManager.instance.conversationModel.conversationTitle );
           
        }
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
        AddCurrentRunToListAndResetIt();
    }

    private void AddCurrentRunToListAndResetIt()
    {
        Runs.Add(new List<string>(CurrentRun));
        CurrentRun = new List<string>();
    }

    public void CapturePerformanceForEvent(string eventDescription)
    {
        var MBDivisor = (1024 * 1024);
        var formattedTime = AdroitProfiler_Service.FormatTime(Time.time);
        var line = "";
        line += AdroitProfiler.RunName + ", ";
        line += formattedTime + ",";
        line += SceneManager.GetActiveScene().name+", ";
        if(eventDescription == "" || eventDescription == null){
            eventDescription = "Dialog Event @ "+ formattedTime;
        }
        line += eventDescription+ ", ";
        line += AdroitProfiler.TimePerFrame_Metrics.MaxValueInLast_10Seconds + ", ";
        line += AdroitProfiler.TimePerFrame_Metrics.MaxValueInLast_5Seconds + "  , " ;
        line += AdroitProfiler.TimePerFrame_Metrics.MaxValueInLast_TenthSecond + " , ";
        line += AdroitProfiler.TimePerFrame_Metrics.MaxValueInLast_HalfSecond + " , ";
        line += AdroitProfiler.TimePerFrame_Metrics.MaxValueInLast_QuarterSecond + " , ";

        line += AdroitProfiler.AverageFPSFor_10Seconds + ", ";
        line += AdroitProfiler.AverageFPSFor_5Seconds + ", ";
        line += AdroitProfiler.AverageFPSFor_HalfSecond + ", ";
        line += AdroitProfiler.AverageFPSFor_QuarterSecond + ", ";
        line += AdroitProfiler.AverageFPSFor_TenthSecond + ", ";

        line += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_10Seconds / MBDivisor + "MB , ";
        line += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_5Seconds / MBDivisor + "MB , ";
        line += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_TenthSecond / MBDivisor + "MB , ";
        line += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_HalfSecond / MBDivisor + "MB , ";
        line += AdroitProfiler.SystemMemory_Metrics.MaxValueInLast_QuarterSecond / MBDivisor + "MB , ";

        line += AdroitProfiler.DrawCalls_Metrics.MaxValueInLast_10Seconds + ", ";
        line += AdroitProfiler.DrawCalls_Metrics.MaxValueInLast_5Seconds + "  , ";
        line += AdroitProfiler.DrawCalls_Metrics.MaxValueInLast_TenthSecond + " , ";
        line += AdroitProfiler.DrawCalls_Metrics.MaxValueInLast_HalfSecond + " , ";
        line += AdroitProfiler.DrawCalls_Metrics.MaxValueInLast_QuarterSecond + " , ";

        line += AdroitProfiler.PolyCount_Metrics.MaxValueInLast_10Seconds + ", ";
        line += AdroitProfiler.PolyCount_Metrics.MaxValueInLast_5Seconds + "  , ";
        line += AdroitProfiler.PolyCount_Metrics.MaxValueInLast_TenthSecond + " , ";
        line += AdroitProfiler.PolyCount_Metrics.MaxValueInLast_HalfSecond + " , ";
        line += AdroitProfiler.PolyCount_Metrics.MaxValueInLast_QuarterSecond + " , ";

        line += "\n";
        CurrentRun.Add(line);
        Debug.Log(line);
    }


    public void SaveLogs()
    {
        string jsonLogData = "";
        Runs.ForEach(run =>
        {
            jsonLogData += ProfileHeader;
            run.ForEach(x =>
            { 
                jsonLogData += x;
            }); 
        });
        Debug.Log("Saved JSON Data from Unity Project: " + jsonLogData);
#if UNITY_WEBGL && !UNITY_EDITOR
            downloadToFile(jsonLogData, "AdroitProfilier_Log");
#endif
    }

   

}

