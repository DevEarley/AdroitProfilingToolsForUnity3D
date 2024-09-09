using AdroitStudios;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdroitProfiler_AutomatedTester_CharacterInterface : MonoBehaviour
{
    [HideInInspector]
    public GameObject Camera;
    [HideInInspector]
    public CharacterController CharacterController;
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded_Static;
    }
    private static void OnSceneLoaded_Static(Scene scene, LoadSceneMode mode)
    {
        var _this = FindObjectOfType<AdroitProfiler_AutomatedTester_CharacterInterface>();
        _this.OnSceneLoaded(scene, mode);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Camera = null;
        CharacterController = null;
        SetCamera();
        SetCharacterController();

    }

    public void SetCharacterController()
    {
        CharacterController = FindObjectsOfType<CharacterController>().FirstOrDefault(x => x.gameObject.activeInHierarchy);
    }

    public void SetCamera()
    {
        Camera = GameObject.Find("Camera Pivot");
        if (Camera != null)
        {
            var _PlayerCameraRotation = Camera.GetComponent<PlayerCameraRotation>();
            _PlayerCameraRotation.ForTesting_DisableMouseLook = true;
            //  _PlayerCameraRotation.enabled = IsRecording;
        }
    }
}
