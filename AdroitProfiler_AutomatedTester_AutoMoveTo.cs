using AdroitStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_AutomatedTester_CharacterInterface))]
public class AdroitProfiler_AutomatedTester_AutoMoveTo : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    private static float LookAtDistance = 1.0f;
    private static float MoveToDistance = 0.9f;
    private List<GameObject> TargetGOForThisScene;
    private List<GameObject> SourceGOForThisScene;
    private AdroitProfiler_AutomatedTester_CharacterInterface CharacterInterface;

    private void Start()
    {
        CharacterInterface = gameObject.GetComponent<AdroitProfiler_AutomatedTester_CharacterInterface>();
    }

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (TargetGOForThisScene == null) return;
        if (SourceGOForThisScene == null) return;

        var targetPostition = Vector3.zero;
        if (config.Target == null || config.Target == "")
        {
            targetPostition = config.WorldPosition;
        }

        var TargetGameObject = GetTargetGO(config);
        if (TargetGameObject != null)
        {
            targetPostition = TargetGameObject.transform.position;
        }
        else
        {
            Debug.LogErrorFormat("AdroitProfiler_AutomatedTester_AutoMoveTo | Target GO missing");
            return;
        }

        var SourceGameObject = GetSourceGO(config);
        if (SourceGameObject == null)
        {
            Debug.LogErrorFormat("AdroitProfiler_AutomatedTester_AutoMoveTo | Source GO missing");
            return;
        }
        var CharacterController = CharacterInterface.CharacterController;
        var Camera = CharacterInterface.Camera;
        if (CharacterController == null) CharacterInterface.SetCharacterController();
        if (Camera == null) CharacterInterface.SetCamera();
        if (CharacterController == null) return;
        if (Camera == null) return;
        var SourceVectorWitoutY = Vector3.zero;
        if (config.UseCharacterInterface)
        {
            SourceVectorWitoutY = new Vector3(CharacterController.transform.position.x, 0, CharacterController.transform.position.z);

        }
        else
        {
            SourceVectorWitoutY = new Vector3(SourceGameObject.transform.position.x, 0, SourceGameObject.transform.position.z);

        }
        var GOVectorWithoutY = new Vector3(targetPostition.x, 0, targetPostition.z);
        var distance = Vector3.Distance(SourceVectorWitoutY, GOVectorWithoutY);
        if (distance > LookAtDistance)
        {
            Camera.transform.LookAt(TargetGameObject.transform);
        }
        if (distance > MoveToDistance)
        {

            if (config.UseCharacterInterface)
            {

                CharacterController.SimpleMove(Camera.transform.forward * config.MoveSpeed * Time.deltaTime);

            }
            else
            {
                SourceGameObject.transform.position = Vector3.Lerp(SourceGameObject.transform.position, TargetGameObject.transform.position, config.MoveSpeed * Time.deltaTime);
            }

        }
    }

    private GameObject GetTargetGO(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var TargetGameObject = TargetGOForThisScene.FirstOrDefault(x => (x != null && x.name == config.Target));
        if (TargetGameObject == null)
        {
            TargetGameObject = GameObject.Find(config.Target);
            if (TargetGameObject == null)
            {
                Debug.LogErrorFormat("AdroitProfiler_AutomatedTester_AutoMoveTo | Could not find Target GO");
            }
        }

        return TargetGameObject;
    }

    private GameObject GetSourceGO(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var SourceGameObject = TargetGOForThisScene.FirstOrDefault(x => (x != null && x.name == config.Source));
        if (SourceGameObject == null)
        {
            SourceGameObject = GameObject.Find(config.Source);
            if (SourceGameObject == null)
            {
                Debug.LogErrorFormat("AdroitProfiler_AutomatedTester_AutoMoveTo | Could not find Source GO");
            }
        }


        return SourceGameObject;
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {
        TargetGOForThisScene = configs.Select(x => GameObject.Find(x.Target)).ToList();
        SourceGOForThisScene = configs.Select(x => GameObject.Find(x.Source)).ToList();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
