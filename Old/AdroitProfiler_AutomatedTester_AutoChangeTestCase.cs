using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AdroitProfiler_AutomatedTester))]

public class AdroitProfiler_AutomatedTester_AutoChangeTestcase : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private AdroitProfiler_AutomatedTester AdroitProfiler_AutomatedTester;

    private void Start()
    {
        AdroitProfiler_AutomatedTester = gameObject.GetComponent<AdroitProfiler_AutomatedTester>();
    }
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        AdroitProfiler_AutomatedTester.GotoNextTestCase();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
