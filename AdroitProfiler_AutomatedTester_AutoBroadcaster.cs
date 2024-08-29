using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdroitProfiler_AutomatedTester_AutoBroadcaster : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    [HideInInspector]
    private string CurrentSceneName;
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if ( config.StartInScene == CurrentSceneName && config.InvokeAtTime <= Time.timeSinceLevelLoad)
        {
            BroadcastMessageToGO(config);
        }
    }


    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> config, UnityEngine.SceneManagement.Scene scene)
    {
        CurrentSceneName = scene.name;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public void BroadcastMessageToGO(AdroitProfiler_AutomatedTester_Configuration config)
    {
        Debug.Log("Try to BroadcastMessage To GO");
        config.Sent = true;
        var GO = GameObject.Find(config.GameObjectPath);
        GO.SendMessage(config.Function, config.Value);
    }


}
