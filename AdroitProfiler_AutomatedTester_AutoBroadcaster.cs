using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (config.TargetMultiple) {
            config.Targets.ForEach(x => {
                var GO = GameObject.Find(x);
                if (GO != null)
                {
                    Debug.Log("AutoBroadcaster | Sent Message | "+GO.name+"."+ config.Function+"("+ config.Value+")");

                    GO.SendMessage(config.Function, config.Value, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    Debug.LogErrorFormat("Could not find GO");

                }
            });
        }
        else
        {
            var GO = GameObject.Find(config.Target);
            if (GO != null)
            {
                Debug.Log("AutoBroadcaster | Sent Message | " + GO.name + "." + config.Function + "(" + config.Value + ")");
                GO.SendMessage(config.Function, config.Value, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogErrorFormat("Could not find GO");
            }
        }
        
    }
}
