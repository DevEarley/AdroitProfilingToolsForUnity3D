#if UNITY_WEBGL
using AdroitStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_Logger))]
[RequireComponent(typeof(AdroitProfiler_WebPageInterface))]

public class AdroitProfiler_AutomatedTester_AutoUpdateWebpage : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private AdroitProfiler_Logger Logger;
    private AdroitProfiler_WebPageInterface WebPageInterface;
    private void Start()
    {
        WebPageInterface = gameObject.GetComponent<AdroitProfiler_WebPageInterface>();
        Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
    }
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        WebPageInterface.UpdateWebPage(Logger.Runs.SelectMany(x=>x.Value).ToArray());
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
#endif