using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoMover))]
public class AdroitProfiler_AutomatedTester_AutoMoveTo : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private List<GameObject> GameObjectsForThisScene;
    private AdroitProfiler_AutomatedTester_AutoMover AdroitProfiler_AutomatedTester_AutoMover;

    private void Start()
    {
        AdroitProfiler_AutomatedTester_AutoMover = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoMover>();
    }

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var GameObjectForConfig = GameObjectsForThisScene.First(x => x.name == config.Target);
        var CharacterController = AdroitProfiler_AutomatedTester_AutoMover.CharacterController;
        var Camera = AdroitProfiler_AutomatedTester_AutoMover.Camera;
        Camera.transform.LookAt(GameObjectForConfig.transform);
        CharacterController.SimpleMove(Camera.transform.forward * config.MoveSpeed * Time.deltaTime);
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {
        GameObjectsForThisScene =  configs.Select(x => GameObject.Find(x.Target)).ToList();
    }
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
