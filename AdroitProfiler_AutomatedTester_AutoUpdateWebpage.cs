#if UNITY_WEBGL
using AdroitStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_WebPageInterface))]
public class AdroitProfiler_AutomatedTester_AutoUpdateWebpage : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private AdroitProfiler_WebPageInterface WebPageInterface;

    private void Start()
    {
        WebPageInterface = gameObject.GetComponent<AdroitProfiler_WebPageInterface>();
    }

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.Sent = true;
        WebPageInterface.UpdateWebPage();
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {
        configs.ForEach(x => x.Sent = false);  
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
#endif