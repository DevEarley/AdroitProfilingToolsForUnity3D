using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdroitProfiler))]
public class AdroitProfiler_ProfileController : MonoBehaviour
{
    public readonly string ProfileHeader = "Scene Name, Event Description, Worst Frame in 10 Seconds, Worst Frame in 5 Seconds, Worst Frame in 0.5 Seconds, FPS, Time";
    public List<string> Profile = new List<string>();

    private AdroitProfiler AdroitProfiler;

    private bool InConversation = false;

    void Start()
    {
        AdroitProfiler = this.gameObject.GetComponent<AdroitProfiler>();
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
        var line = SceneManager.GetActiveScene().name+", ";
        if(eventDescription == ""){
            eventDescription += "Dialog Event";
        }
        line += eventDescription+ ", ";
        line += AdroitProfiler.MaxInThis_10Seconds_TimePerFrame + ", ";
        line += AdroitProfiler.MaxInThis_5Seconds_TimePerFrame + "  , " ;
        line += AdroitProfiler.MaxInThis_HalfSecond_TimePerFrame + " , ";
        line += AdroitProfiler.PreviousAverage + ", ";
        line += AdroitProfiler_Service.FormatTime(Time.time) +"\n\r";
        Profile.Add(line);
        Debug.Log(line);
    }
}
