using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AdroitProfiler_AutomatedTester))]

public class AdroitProfiler_AutomatedTester_AutoChangeTestCase : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private AdroitProfiler_AutomatedTester AdroitProfiler_AutomatedTester;

    private void Start()
    {
        AdroitProfiler_AutomatedTester = gameObject.GetComponent<AdroitProfiler_AutomatedTester>();
    }
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        Debug.Log("AutoChangeTestCase | " + config.name);

        AdroitProfiler_AutomatedTester.GotoNextTestCase();

    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
