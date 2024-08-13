using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_ProfileController : MonoBehaviour
{

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void downloadToFile(string content, string filename);
#endif

    public readonly string ProfileHeader = "Run,Time, Scene Name, Event Description, LFT: 10s, FPS: 10s, LFT: 5s, FPS: 5s, LFT: 0.5s, FPS: 0.5s, LFT: 0.25s, FPS: 0.25s, LFT: 0.1s, FPS: 0.1s, \n";
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
            CapturePerformanceForEvent(DialogueManager.instance.conversationModel.conversationTitle, "RUN" );
           
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
        var _this = GameObject.FindObjectOfType<AdroitProfiler_ProfileController>();
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

    public void CapturePerformanceForEvent(string eventDescription, string runDescription)
    {
        var formattedTime = AdroitProfiler_Service.FormatTime(Time.time);
        var line = "";
        line += runDescription + ", ";
        line += formattedTime + ",";
        line += SceneManager.GetActiveScene().name+", ";
        if(eventDescription == "" || eventDescription == null){
            eventDescription = "Dialog Event @ "+ formattedTime;
        }
        line += eventDescription+ ", ";
        line += AdroitProfiler.MaxInThis_10Seconds_TimePerFrame + ", ";
        line += AdroitProfiler.AverageFPSFor_10Seconds + ", ";
        line += AdroitProfiler.MaxInThis_5Seconds_TimePerFrame + "  , " ;
        line += AdroitProfiler.AverageFPSFor_5Seconds + ", ";
        line += AdroitProfiler.MaxInThis_HalfSecond_TimePerFrame + " , ";
        line += AdroitProfiler.AverageFPSFor_HalfSecond + ", ";
        line += AdroitProfiler.MaxInThis_QuarterSecond_TimePerFrame + " , ";
        line += AdroitProfiler.AverageFPSFor_QuarterSecond + ", ";
        line += AdroitProfiler.MaxInThis_TenthSecond_TimePerFrame + " , ";
        line += AdroitProfiler.AverageFPSFor_TenthSecond + ", ";

        line += "\n";
        CurrentRun.Add(line);
        Debug.Log(line);
    }


    public void SaveLogs()
    {
        string jsonLogData = ProfileHeader;
        CurrentRun.ForEach(x => { jsonLogData += x; });
        Debug.Log("Saved JSON Data from Unity Project: " + jsonLogData);
#if UNITY_WEBGL && !UNITY_EDITOR
            downloadToFile(jsonLogData, "AdroitProfilier_Log");
#endif
    }

   

}

