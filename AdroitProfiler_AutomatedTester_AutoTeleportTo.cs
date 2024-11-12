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
            Debug.LogErrorFormat("AutoTeleportTo | Could not find target | Attempt 1");
            GO = GameObject.Find(config.Target);
            if (GO == null) {
                Debug.LogErrorFormat("AutoTeleportTo | Could not find target | Attempt 2");
                return;
            };
        }
        if (GO != null)
        {
            Debug.Log("AutoTeleportTo | Teleporting");

            CharacterInterface.CharacterController.transform.position = GO.transform.position;
        }
        else
        {
            Debug.LogErrorFormat("AutoTeleportTo | Could not find target");

        }

    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {
        GameObjectsForThisScene = configs.Select(x => GameObject.Find(x.Target)).ToList();
    }
}
