using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdroitProfiler_AutomatedTester_AutoClickTarget : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    [HideInInspector]
    private string CurrentSceneName;
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {

    }


    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> config, Scene scene)
    {

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
