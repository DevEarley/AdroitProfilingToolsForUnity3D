using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdroitProfiler_AutomatedTester_AutoChangeScene : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        Debug.Log("AutoChangeScene | " + config.name);
        SceneManager.LoadScene(config.Target);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
