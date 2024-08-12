using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_ProfileController : MonoBehaviour
{
    public readonly string ProfileHeader = "Time, Scene Name, Event Description, Longest FT in 10s, Longest FT in 5s, Longest FT in 0.5s, Longest FT in 0.25s, Longest FT in 0.1s,\n";
    public List<string> Profile = new List<string>();

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
            CapturePerformanceForEvent(DialogueManager.instance.conversationModel.conversationTitle);
           
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CapturePerformanceForEvent(string eventDescription)
    {
        var formattedTime = AdroitProfiler_Service.FormatTime(Time.time);
        var line = "";
        line += formattedTime + ",";
        line += SceneManager.GetActiveScene().name+", ";
        if(eventDescription == "" || eventDescription == null){
            eventDescription = "Dialog Event @ "+ formattedTime;
        }
        line += eventDescription+ ", ";
        line += AdroitProfiler.MaxInThis_10Seconds_TimePerFrame + ", ";
        line += AdroitProfiler.MaxInThis_5Seconds_TimePerFrame + "  , " ;
        line += AdroitProfiler.MaxInThis_HalfSecond_TimePerFrame + " , ";
        line += AdroitProfiler.MaxInThis_QuarterSecond_TimePerFrame + " , ";
        line += AdroitProfiler.MaxInThis_TenthSecond_TimePerFrame + " , ";
        line += "\n";
        Profile.Add(line);
        Debug.Log(line);
    }
}
