using AdroitStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_AutomatedTester_CharacterInterface))]
public class AdroitProfiler_AutomatedTester_AutoMoveTo : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private List<GameObject> GameObjectsForThisScene;
    private AdroitProfiler_AutomatedTester_CharacterInterface CharacterInterface;

    private void Start()
    {
        CharacterInterface = gameObject.GetComponent<AdroitProfiler_AutomatedTester_CharacterInterface>();
    }

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (GameObjectsForThisScene == null) return;


        var GO = GameObjectsForThisScene.FirstOrDefault(x => (x != null && x.name == config.Target));
        if (GO == null)
        {
            GO = GameObject.Find(config.Target);
            if (GO == null) return;
        }


        var CharacterController = CharacterInterface.CharacterController;
        var Camera = CharacterInterface.Camera;
        if (CharacterController == null) CharacterInterface.SetCharacterController();
        if (Camera == null) CharacterInterface.SetCamera();
        if (CharacterController == null) return;
        if (Camera == null) return;
        Camera.GetComponent<PlayerCameraRotation>().enabled = false;
        Camera.transform.LookAt(GO.transform);
        CharacterController.SimpleMove(Camera.transform.forward * config.MoveSpeed * Time.deltaTime);
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {
        GameObjectsForThisScene = configs.Select(x => GameObject.Find(x.Target)).ToList();
    }
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
