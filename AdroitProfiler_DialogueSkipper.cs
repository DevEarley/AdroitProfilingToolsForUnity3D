using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem.ChatMapper;
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
        if (InConversation != DialogueManager.instance.isConversationActive)
        {
            if (SkipConversation == true)
            {
                DialogueManager.instance.ConversationController.GotoFirstResponse();
            }

            if (CaptureConversation == false) return;
            InConversation = DialogueManager.instance.isConversationActive;
            if (DialogueManager.instance.conversationModel.conversationTitle != null)
            {
                LastEventName = DialogueManager.instance.conversationModel.conversationTitle;
            }

            AdroitProfiler_Logger.CapturePerformanceForEvent(LastEventName + (InConversation ? " | START" : " | END"));
        }
    }
}
