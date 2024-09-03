using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PixelCrushers.DialogueSystem;
using UnityEngine;
[RequireComponent(typeof(AdroitProfiler_Logger))]

public class AdroitProfiler_AutomatedTester_AutoChooseDialogChoice : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private bool InConversation = false;
    private string LastEventName = "";
    private AdroitProfiler_Logger AdroitProfiler_Logger;

    public void ProcessConfiguration(  AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (DialogueManager.instance == null) return;
        if (DialogueManager.instance.isConversationActive)
        {
            var objects = FindObjectsOfType<StandardUIResponseButton>().Where(x => x.gameObject.activeInHierarchy);
            if (objects != null && objects.Count() > 0 && objects.First() != null)
            {
                objects.OrderBy(x => x.text).First().OnClick();
            }
        }

        if (InConversation != DialogueManager.instance.isConversationActive)
        {
            if (config.CaptureDialog == false) return;
            InConversation = DialogueManager.instance.isConversationActive;
            if (DialogueManager.instance.conversationModel != null && DialogueManager.instance.conversationModel.conversationTitle != null)
            {
                LastEventName = DialogueManager.instance.conversationModel.conversationTitle;
            }

            AdroitProfiler_Logger.CapturePerformanceForEvent(LastEventName + (InConversation ? " | START" : " | END"));
        }
    }

    private void Start()
    {
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
