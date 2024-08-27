using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem.ChatMapper;
using System.Linq;

[RequireComponent(typeof(AdroitProfiler_State))]
[RequireComponent(typeof(AdroitProfiler_Logger))]
public class AdroitProfiler_DialogueSkipper : MonoBehaviour
{
    public bool SkipConversation = true;
    public bool CaptureConversation = true;
    private bool InConversation = false;
    private string LastEventName = "";
    private AdroitProfiler_Logger AdroitProfiler_Logger;

    public AdroitProfiler_Heartbeat_Timing Heartbeat_Timing = AdroitProfiler_Heartbeat_Timing.None;
    private AdroitProfiler_State AdroitProfiler_State;

    private void Start()
    {
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        AdroitProfiler_State = gameObject.GetComponent<AdroitProfiler_State>();

        switch (Heartbeat_Timing)
        {
            case AdroitProfiler_Heartbeat_Timing.EveryTenthSecond:
                AdroitProfiler_State.onTenth_Heartbeat_delegates.Add(OnHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryQuarterSecond:
                AdroitProfiler_State.onQuarter_Heartbeat_delegates.Add(OnHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryHalfSecond:
                AdroitProfiler_State.onHalf_Heartbeat_delegates.Add(OnHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every5Seconds:
                AdroitProfiler_State.on5s_Heartbeat_delegates.Add(OnHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every10Seconds:
                AdroitProfiler_State.on10s_Heartbeat_delegates.Add(OnHeartbeat);
                break;
        }
    }

    private void OnHeartbeat()
    {
        if (DialogueManager.instance == null) return;
        if (DialogueManager.instance.isConversationActive)
        { 
            var objects = FindObjectsOfType<StandardUIResponseButton>().Where(x=>x.gameObject.activeInHierarchy);
            if (objects!=null && objects.Count() > 0 && objects.First() != null)
            {
                objects.OrderBy(x=>x.text).First().OnClick();
            }
        }
       
        if (InConversation != DialogueManager.instance.isConversationActive)
        {
            if (CaptureConversation == false) return;
            InConversation = DialogueManager.instance.isConversationActive;
            if (DialogueManager.instance.conversationModel!= null && DialogueManager.instance.conversationModel.conversationTitle != null)
            {
                LastEventName = DialogueManager.instance.conversationModel.conversationTitle;
            }

            AdroitProfiler_Logger.CapturePerformanceForEvent(LastEventName + (InConversation ? " | START" : " | END"));
        }
    }

    }
