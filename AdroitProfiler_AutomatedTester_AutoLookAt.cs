using UnityEngine;

public class AdroitProfiler_AutomatedTester_AutoLookAt : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    [HideInInspector]
    private string CurrentSceneName;
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {

    }


    public void OnSceneLoaded(AdroitProfiler_AutomatedTester_Configuration config, UnityEngine.SceneManagement.Scene scene)
    {

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}