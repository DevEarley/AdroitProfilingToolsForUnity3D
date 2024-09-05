using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdroitProfiler_AutomatedTester_AutoBroadcaster : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    // add list of game objects
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        BroadcastMessageToGO(config);
    }


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void BroadcastMessageToGO(AdroitProfiler_AutomatedTester_Configuration config)
    {
        Debug.Log("Try to BroadcastMessage To GO");
        var GO = GameObject.Find(config.GameObjectPath);
        if(GO!= null)
        { 
            //config.Sent = true;
            GO.SendMessage(config.Function, config.Value, SendMessageOptions.DontRequireReceiver);
        }
    }
}
