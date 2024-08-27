using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using AdroitStudios;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(AdroitProfiler_AutoMover))]
public class AdroitProfiler_AutoMover_Heartbeat_Button : Editor
{

    override public void OnInspectorGUI()
    {
        AdroitProfiler_AutoMover autoMover = (AdroitProfiler_AutoMover)target;
        if (autoMover.Configurations.Count > 0 && GUILayout.Button("Export Settings"))
        {
            autoMover.ExportSettings();
        }
        if (autoMover.Configurations_CSV != null && GUILayout.Button("Import Settings"))
        {
            autoMover.GetConfigsFromSettingsFile();
        }
        if (autoMover.IsRecording == false && GUILayout.Button("Start Recording"))
        {
            autoMover.StartRecording();
        }
        if (autoMover.IsRecording == true && GUILayout.Button("Stop Recording"))
        {
            autoMover.StopRecording();
        }
        DrawDefaultInspector();
    }
}
#endif
public enum AdroitProfiler_AutoMover_Configuration_MovementType
{
    MoveForward,
    MoveBackwards,
    TurnLeft,
    TurnRight
}
[System.Serializable]
public class AdroitProfiler_AutoMover_Configuration
{
    public AdroitProfiler_AutoMover_Configuration_MovementType Movement;
    public string StartInScene;
    public float Speed = 1.0f;
    public float StartTime;
    public float EndTime;
}

public class AdroitProfiler_AutoMover : MonoBehaviour
{
    public float MoveSpeed = 100.0f;
    public float TurnSpeed = 35.0f;
    public GameObject Camera;
    public CharacterController characterController;
    public TextAsset Configurations_CSV;
    public List<AdroitProfiler_AutoMover_Configuration> Configurations = new List<AdroitProfiler_AutoMover_Configuration>();
    private AdroitProfiler_AutoMover_Configuration CurrentProfile;
    public bool IsRecording = false;
    private Vector2 KeyInputTrack_W = Vector2.zero;
    private Vector2 KeyInputTrack_A = Vector2.zero;
    private Vector2 KeyInputTrack_S = Vector2.zero;
    private Vector2 KeyInputTrack_D = Vector2.zero;
    private string CurrentSceneName = "";

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {

        SceneManager.sceneLoaded += OnSceneLoaded_Static;
    }

    private static void OnSceneLoaded_Static(Scene scene, LoadSceneMode mode)
    {
        var _this = FindObjectOfType<AdroitProfiler_AutoMover>();
        _this.OnScenLoaded(scene, mode);
    }

    private void Start()
    {
        CurrentSceneName = SceneManager.GetActiveScene().path;
        if (Configurations_CSV != null && Configurations.Count == 0)
        {
            GetConfigsFromSettingsFile();
        }

    }

    private void OnScenLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneName = scene.path;
        SetCamera();
        // Camera.transform.LookAt()
        

        Debug.Log("scene.path : " + scene.path);


        CurrentProfile = Configurations.FirstOrDefault(x => x.StartInScene == "" || x.StartInScene == scene.path);
        if (CurrentProfile != null)
        {

            characterController = FindObjectOfType<CharacterController>();
        }
        else
        {
            characterController = null;
        }
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

    private void Update()
    {
        if (Camera == null)
        {
            Debug.Log("Camera Is Null");
            SetCamera();
            return;
        }
        if (characterController == null)
        {
            Debug.Log("characterController Is Null");

            characterController = FindObjectsOfType<CharacterController>().FirstOrDefault(x => x.gameObject.activeInHierarchy);
            return;
        }

        MoveCharacter();

        UpdateRecordings();
    }

    private void MoveCharacter()
    {
        foreach (var config in Configurations.Where(x => x.StartInScene == CurrentSceneName))
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
    }

    private void MoveForConfig(AdroitProfiler_AutoMover_Configuration config)
    {
        if (Camera == null) return;
        Debug.Log("MoveForConfig | " + config.StartInScene);
        switch (config.Movement)
        {
            case AdroitProfiler_AutoMover_Configuration_MovementType.MoveForward:
                characterController.SimpleMove(Camera.transform.forward * config.Speed * Time.deltaTime);
                break;
            case AdroitProfiler_AutoMover_Configuration_MovementType.TurnLeft:
                //characterController.SimpleMove(Camera.transform.right * config.Speed * Time.deltaTime);
                Camera.transform.Rotate(Vector3.up, -1.0f*config.Speed * Time.deltaTime);
                break;
            case AdroitProfiler_AutoMover_Configuration_MovementType.MoveBackwards:
                characterController.SimpleMove(-1.0f * Camera.transform.forward * config.Speed * Time.deltaTime);
                break;
            case AdroitProfiler_AutoMover_Configuration_MovementType.TurnRight:
                Camera.transform.Rotate(Vector3.up, config.Speed * Time.deltaTime);
                //characterController.SimpleMove(-1.0f * Camera.transform.right * config.Speed * Time.deltaTime);
                break;
        }
    }

    private void UpdateRecordings()
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
                CreateConfigFromTrack(KeyCode.W, KeyInputTrack_W, MoveSpeed);
                KeyInputTrack_W = Vector2.zero;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                KeyInputTrack_A.y = Time.timeSinceLevelLoad;
                CreateConfigFromTrack(KeyCode.A, KeyInputTrack_A, TurnSpeed);
                KeyInputTrack_A = Vector2.zero;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                KeyInputTrack_S.y = Time.timeSinceLevelLoad;
                CreateConfigFromTrack(KeyCode.S, KeyInputTrack_S, MoveSpeed);
                KeyInputTrack_S = Vector2.zero;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                KeyInputTrack_D.y = Time.timeSinceLevelLoad;
                CreateConfigFromTrack(KeyCode.D, KeyInputTrack_D, TurnSpeed);
                KeyInputTrack_D = Vector2.zero;
            }
        }
    }

    private void CreateConfigFromTrack(KeyCode keyCode, Vector2 track, float speed)
    {
        var config = new AdroitProfiler_AutoMover_Configuration();
        config.Speed = speed;
        switch (keyCode){
            case KeyCode.W:
                config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.MoveForward;
                break;
            case KeyCode.A:
                config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.TurnLeft;
                break;
            case KeyCode.S:
                config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.MoveBackwards;

                break;
            case KeyCode.D:
                config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.TurnRight;
                break;
        }
       
        config.StartInScene = CurrentSceneName;
        config.StartTime = track.x;
        config.EndTime = track.y;
        Configurations.Add(config);
    }

    public void GetConfigsFromSettingsFile()
    {
        Configurations.Clear();
        var profiles_strings = Configurations_CSV.text.Split("\n");
        foreach (var profile_string in profiles_strings)
        {
            var settings = profile_string.Split(",");
            if (settings.Length < 5) return;
            var config = new AdroitProfiler_AutoMover_Configuration();
            switch (settings[0])
            {
                case "w":
                case "W":
                    config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.MoveForward;

                    break;

                case "a":
                case "A":
                    config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.TurnLeft;

                    break;

                case "s":
                case "S":
                    config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.MoveBackwards;

                    break;

                case "d":
                case "D":
                    config.Movement = AdroitProfiler_AutoMover_Configuration_MovementType.TurnRight;


                    break;
            }
            config.StartInScene = settings[1];
            config.Speed = float.Parse(settings[2]);
            config.StartTime = float.Parse(settings[3]);
            config.EndTime = float.Parse(settings[4]);
            Configurations.Add(config);
        }
    }

    public void ExportSettings()
    {
        var fileName = "AdroitProfiler_Configurations_AutoMover.csv";
        var Path = Application.streamingAssetsPath.Replace("/StreamingAssets", "/") + "Profiling/" + fileName;
        StreamWriter writer = new StreamWriter(Path);
        var text = "";
        foreach (var config in Configurations)
        {
            var line = "";
            switch (config.Movement)
            {
                case AdroitProfiler_AutoMover_Configuration_MovementType.MoveForward:
                    line += "W,";
                    break;
                case AdroitProfiler_AutoMover_Configuration_MovementType.TurnLeft:
                    line += "A,";
                    break;
                case AdroitProfiler_AutoMover_Configuration_MovementType.MoveBackwards:
                    line += "S,";
                    break;
                case AdroitProfiler_AutoMover_Configuration_MovementType.TurnRight:
                    line += "D,";
                    break;
            }
            line += config.StartInScene + ",";
            line += config.Speed + ",";
            line += config.StartTime + ",";
            line += config.EndTime + ",\n";
            text += line;
        }
        writer.Write(text);
        writer.Close();
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
}
