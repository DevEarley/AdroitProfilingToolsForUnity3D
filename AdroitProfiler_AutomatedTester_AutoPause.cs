using AdroitStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_Heartbeat))]

public class AdroitProfiler_AutomatedTester_AutoPause : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{

    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;
    private void Start()
    {
        AdroitProfiler_Heartbeat = gameObject.GetComponent<AdroitProfiler_Heartbeat>();
    }

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        AdroitProfiler_Heartbeat.Pause();
    }

   
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
