using UnityEngine;
using AdroitStudios;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(AdroitProfiler_AutomatedTester_AutoMover))]
public class AdroitProfiler_AutomatedTester_AutoMover_Editor : Editor
{
    override public void OnInspectorGUI()
    {
        AdroitProfiler_AutomatedTester_AutoMover autoMover = (AdroitProfiler_AutomatedTester_AutoMover)target;
        //TODO Fix recording
        //if (autoMover.IsRecording == false && GUILayout.Button("Start Recording"))
        //{
        //    autoMover.StartRecording();
        //}
        //if (autoMover.IsRecording == true && GUILayout.Button("Stop Recording"))
        //{
        //    autoMover.StopRecording();
        //}
        DrawDefaultInspector();
    }
}

#endif

[RequireComponent(typeof(AdroitProfiler_AutomatedTester))]


public class AdroitProfiler_AutomatedTester_AutoMover : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    [HideInInspector]
    public GameObject Camera;
    [HideInInspector]
    public CharacterController CharacterController;
    [HideInInspector]
    private string CurrentSceneName;
    private Vector2 KeyInputTrack_W = Vector2.zero;
    private Vector2 KeyInputTrack_A = Vector2.zero;
    private Vector2 KeyInputTrack_S = Vector2.zero;
    private Vector2 KeyInputTrack_D = Vector2.zero;
    [HideInInspector]
    public bool IsRecording = false;
    private AdroitProfiler_AutomatedTester AdroitProfiler_AutomatedTester;
    public float DefaultMoveSpeed = 1.0f;
    public float DefaultTurnSpeed = 1.0f;

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (Camera == null)
        {
            Debug.Log("Camera Is Null");
            SetCamera();
            return;
        }
        if (CharacterController == null)
        {
            Debug.Log("characterController Is Null");

            CharacterController = FindObjectsOfType<CharacterController>().FirstOrDefault(x => x.gameObject.activeInHierarchy);
            return;
        }

        // if recording, disable normal movement and move the character controller directly
        MoveCharacter(config);
      //  UpdateRecordings(config);

    }

    public void OnSceneLoaded(AdroitProfiler_AutomatedTester_Configuration config, UnityEngine.SceneManagement.Scene scene)
    {
        CurrentSceneName = scene.path;
        SetCamera();
    }

    private void SetCamera()
    {
        Camera = GameObject.Find("Camera Pivot");
        if (Camera != null)
        {
            var _PlayerCameraRotation = Camera.GetComponent<PlayerCameraRotation>();
            _PlayerCameraRotation.DisableMouseLook = true;
            //  _PlayerCameraRotation.enabled = IsRecording;
        }
    }

    private void MoveCharacter(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (config.StartTime <= Time.timeSinceLevelLoad && config.EndTime >= Time.timeSinceLevelLoad)
        {
            MoveForConfig(config);
        }
        else if (config.StartTime <= Time.timeSinceLevelLoad && config.EndTime == 0)
        {
            MoveForConfig(config);
        }
    }

    private void MoveForConfig(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (Camera == null) return;
        Debug.Log("MoveForConfig | " + config.StartInScene);
        switch (config.Movement)
        {
            case AdroitProfiler_AutomatedTester_Configuration_MovementType.MoveForward:
                CharacterController.SimpleMove(Camera.transform.forward * config.MoveSpeed * Time.deltaTime);
                break;
            case AdroitProfiler_AutomatedTester_Configuration_MovementType.TurnLeft:
                Camera.transform.Rotate(Vector3.up, -1.0f * config.TurnSpeed * Time.deltaTime);
                break;
            case AdroitProfiler_AutomatedTester_Configuration_MovementType.MoveBackwards:
                CharacterController.SimpleMove(-1.0f * Camera.transform.forward * config.MoveSpeed * Time.deltaTime);
                break;
            case AdroitProfiler_AutomatedTester_Configuration_MovementType.TurnRight:
                Camera.transform.Rotate(Vector3.up, config.TurnSpeed * Time.deltaTime);
                break;
        }
    }

    private void UpdateRecordings(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (IsRecording)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                KeyInputTrack_W.x = Time.timeSinceLevelLoad;
                KeyInputTrack_W.y = 0;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                KeyInputTrack_A.x = Time.timeSinceLevelLoad;
                KeyInputTrack_A.y = 0;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                KeyInputTrack_S.x = Time.timeSinceLevelLoad;
                KeyInputTrack_S.y = 0;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                KeyInputTrack_D.x = Time.timeSinceLevelLoad;
                KeyInputTrack_D.y = 0;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                KeyInputTrack_W.y = Time.timeSinceLevelLoad;
                CreateConfigFromTrack(KeyCode.W, KeyInputTrack_W, DefaultMoveSpeed);
                KeyInputTrack_W = Vector2.zero;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                KeyInputTrack_A.y = Time.timeSinceLevelLoad;
                CreateConfigFromTrack(KeyCode.A, KeyInputTrack_A, DefaultTurnSpeed);
                KeyInputTrack_A = Vector2.zero;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                KeyInputTrack_S.y = Time.timeSinceLevelLoad;
                CreateConfigFromTrack(KeyCode.S, KeyInputTrack_S, DefaultMoveSpeed);
                KeyInputTrack_S = Vector2.zero;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                KeyInputTrack_D.y = Time.timeSinceLevelLoad;
                CreateConfigFromTrack(KeyCode.D, KeyInputTrack_D, DefaultTurnSpeed);
                KeyInputTrack_D = Vector2.zero;
            }
        }
    }

    private AdroitProfiler_AutomatedTester_Configuration CreateConfigFromTrack(KeyCode keyCode, Vector2 track, float moveSpeed)
    {
        AdroitProfiler_AutomatedTester_Configuration config = new AdroitProfiler_AutomatedTester_Configuration();
        config.MoveSpeed = moveSpeed;
        switch (keyCode)
        {
            case KeyCode.W:
                config.Movement = AdroitProfiler_AutomatedTester_Configuration_MovementType.MoveForward;
                break;
            case KeyCode.A:
                config.Movement = AdroitProfiler_AutomatedTester_Configuration_MovementType.TurnLeft;
                break;
            case KeyCode.S:
                config.Movement = AdroitProfiler_AutomatedTester_Configuration_MovementType.MoveBackwards;
                break;
            case KeyCode.D:
                config.Movement = AdroitProfiler_AutomatedTester_Configuration_MovementType.TurnRight;
                break;
        }
        config.StartInScene = CurrentSceneName;
        config.StartTime = track.x;
        config.EndTime = track.y;
        return config;
    }

    public void StartRecording()
    {
        IsRecording = true;
        if (Camera != null)
        {
            Camera.GetComponent<PlayerCameraRotation>().enabled = true;
        }
    }

    public void StopRecording()
    {
        IsRecording = false;
        if (Camera != null)
        {
            Camera.GetComponent<PlayerCameraRotation>().enabled = false;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
