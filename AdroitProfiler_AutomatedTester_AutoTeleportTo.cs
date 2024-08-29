using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdroitProfiler_AutomatedTester_AutoTeleportTo : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    [HideInInspector]
    private string CurrentSceneName;
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {

    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> config, UnityEngine.SceneManagement.Scene scene)
    {

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
