using System.Linq;
using PixelCrushers.DialogueSystem;
using UnityEngine;
[RequireComponent(typeof(AdroitProfiler_Logger))]

public class AdroitProfiler_AutomatedTester_AutoChooseDialogChoice : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private bool InConversation = false;
    private string LastEventName = "";
    private AdroitProfiler_Logger AdroitProfiler_Logger;

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        Debug.Log("AutoChooseDialogChoice | " + config.name);

        if (DialogueManager.instance == null) return;
        if (DialogueManager.instance.isConversationActive)
        {
            var objects = FindObjectsOfType<StandardUIResponseButton>().Where(x => x.gameObject.activeInHierarchy);
            if (objects != null && objects.Count() > 0 && objects.First() != null)
            {
                if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.SortAlphabetically_PickFirst)
                {
                    var option = objects.OrderBy(x => x.text).FirstOrDefault();
                    ChooseOption(option);
                }
                else if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.SortAlphabetically_PickAtIndex)
                {
                    if (objects.Count() <= config.DialogOptionIndex)
                    {
                        var lastOption = objects.OrderBy(x => x.text).LastOrDefault();
                        ChooseOption(lastOption);
                    }
                    else
                    {
                        var option = objects.OrderBy(x => x.text).ElementAt(config.DialogOptionIndex);
                        ChooseOption(option);
                    }
                }
                else if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.SortAlphabetically_PickLast)
                {
                    var option = objects.OrderBy(x => x.text).LastOrDefault();
                    ChooseOption(option);
                }
                else if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.Unsorted_PickFirst)
                {
                    var option = objects.FirstOrDefault();
                    ChooseOption(option);
                }
                else if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.Unsorted_PickAtIndex)
                {
                    if (objects.Count() <= config.DialogOptionIndex)
                    {
                        var lastOption = objects.LastOrDefault();
                        ChooseOption(lastOption);
                    }
                    else
                    {
                        var option = objects.ElementAt(config.DialogOptionIndex);
                        ChooseOption(option);
                    }
                }
                else if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.Unsorted_PickLast)
                {
                    var option = objects.LastOrDefault();
                    ChooseOption(option);
                }
                else if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.PickRandom)
                {
                    var option = objects.FirstOrDefault();
                    ChooseOption(option);
                }
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

    private void ChooseOption(StandardUIResponseButton option)
    {
        if (option != null)
        {
            AdroitProfiler_Logger.Log(option.text);
            option.OnClick();
        }
        else
        {
            AdroitProfiler_Logger.Log("AutoChooseDialogChoice | Option was null");
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
