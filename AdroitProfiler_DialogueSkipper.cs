using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem.ChatMapper;
using System.Linq;

[RequireComponent(typeof(AdroitProfiler_Logger))]

public class AdroitProfiler_DialogueSkipper : MonoBehaviour
{
    public bool SkipConversation = true;
    public bool CaptureConversation = true;
    private bool InConversation = false;
    private string LastEventName = "";
    private AdroitProfiler_Logger AdroitProfiler_Logger;

    private void Start()
    {
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();

    }

    void Update()
    {
        if (DialogueManager.instance == null) return;
        if (DialogueManager.instance.isConversationActive)
        { 
            var objects = FindObjectsOfType<StandardUIResponseButton>().Where(x=>x.gameObject.activeInHierarchy);
            if (objects!=null && objects.Count() > 0 && objects.First() != null)
            {
                objects.First().OnClick();
            }
        }
        //if (SkipConversation == true 
        //    && DialogueManager.instance.ConversationController != null 
        //    && DialogueManager.instance.ConversationController.currentState != null
        //    && DialogueManager.instance.ConversationController.currentState.HasPCResponses == true
        //    && DialogueManager.instance.ConversationController.currentState.hasForceAutoResponse == false)
        //{
        //    Debug.Log("hasPCResponses | " + DialogueManager.instance.ConversationController.currentState.HasPCResponses);
        //    DialogueManager.instance.ConversationController.SetCurrentResponse(DialogueManager.instance.ConversationController.currentState.pcResponses.First());
        //    DialogueManager.instance.ConversationController.GotoCurrentResponse();

        //}
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
