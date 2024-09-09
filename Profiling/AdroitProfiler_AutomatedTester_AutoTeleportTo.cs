using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AdroitProfiler_AutomatedTester_CharacterInterface))]

public class AdroitProfiler_AutomatedTester_AutoTeleportTo : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private List<GameObject> GameObjectsForThisScene;

    private AdroitProfiler_AutomatedTester_CharacterInterface CharacterInterface;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        CharacterInterface = gameObject.GetComponent<AdroitProfiler_AutomatedTester_CharacterInterface>();

    }
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var GO = GameObjectsForThisScene.FirstOrDefault(x => (x != null && x.name == config.Target));
        if(GO == null)
        {
            GO = GameObject.Find(config.Target);
            if(GO == null) return;
        }
        CharacterInterface.CharacterController.transform.position = GO.transform.position;
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {
        GameObjectsForThisScene = configs.Select(x => GameObject.Find(x.Target)).ToList();
    }
}