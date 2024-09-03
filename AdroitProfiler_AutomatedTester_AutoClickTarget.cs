using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdroitProfiler_AutomatedTester_AutoClickTarget : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private List<GameObject> GameObjectsForThisScene;


    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var GO = GameObjectsForThisScene.FirstOrDefault(x => (x != null && x.name == config.Target));
        if (GO == null)
        {
            GO = GameObject.Find(config.Target);
            if (GO == null) return;
        }
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, Scene scene)
    {
        GameObjectsForThisScene = configs.Select(x => GameObject.Find(x.Target)).ToList();

    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
