using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(AdroitProfiler_AutoClicker_Heartbeat))]
public class AdroitProfiler_AutoClicker_Heartbeat_Button : Editor
{
    override public void OnInspectorGUI()
    {
        AdroitProfiler_AutoClicker_Heartbeat autoClicker = (AdroitProfiler_AutoClicker_Heartbeat)target;
        if (GUILayout.Button("Export Settings"))
        {
            autoClicker.ExportSettings();
        }
        if (GUILayout.Button("Import Settings"))
        {
            autoClicker.GetConfigurationsFromSettingsFile();
        }
        DrawDefaultInspector();
    }
}
#endif

[System.Serializable]
public class AdroitProfiler_AutoClicker_Configuration
{
    public string Label = "";
    public bool Enabled;
    public Vector2 MousePosition = Vector2.one * 2.0f;
    public Vector2 Offset = Vector2.zero;
    public string StartInScene;
    public AdroitProfiler_AutoClicker_Configuration()
    {
        MousePosition = Vector2.one * 2.0f;
    }
}
[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_AutoClicker_Heartbeat : MonoBehaviour
{
    public AdroitProfiler_Heartbeat_Timing Heartbeat_Timing = AdroitProfiler_Heartbeat_Timing.None;
    public List<AdroitProfiler_AutoClicker_Configuration> Configurations;
    public TextAsset Configurations_CSV;
    public AdroitProfiler_AutoClicker_StandaloneInputModule AutoClicker;
    private AdroitProfiler_State AdroitProfiler_State;
    public bool SettingSlot = false;
    private int CurrentSlotBeingSet = 0;
    [HideInInspector]
    public string CurrentSceneName = "";

    private void Start()
    {
        AdroitProfiler_State = gameObject.GetComponent<AdroitProfiler_State>();
        if (Configurations_CSV != null && Configurations.Count == 0)
        {
            GetConfigurationsFromSettingsFile();
        }
        switch (Heartbeat_Timing)
        {
            case AdroitProfiler_Heartbeat_Timing.EverySecond:
                AdroitProfiler_State.on1s_Heartbeat_delegates.Add(OnSecondHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryTenthSecond:
                AdroitProfiler_State.onTenth_Heartbeat_delegates.Add(OnTenthHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryQuarterSecond:
                AdroitProfiler_State.onQuarter_Heartbeat_delegates.Add( OnQuarterHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryHalfSecond:
                AdroitProfiler_State.onHalf_Heartbeat_delegates.Add(OnHalfHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every5Seconds:
                AdroitProfiler_State.on5s_Heartbeat_delegates.Add(On5SecondHeartbeat);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every10Seconds:
                AdroitProfiler_State.on10s_Heartbeat_delegates.Add(On10SecondHeartbeat);
                break;
        }
    }

    private void Update()
    {
        if (SettingSlot)
        {
            var currentProfile = Configurations[CurrentSlotBeingSet];
            if (Input.GetKey(KeyCode.UpArrow))
            {
                currentProfile.Offset = new Vector2(currentProfile.Offset.x, currentProfile.Offset.y - 1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentProfile.Offset = new Vector2(currentProfile.Offset.x-1, currentProfile.Offset.y);


            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                currentProfile.Offset = new Vector2(currentProfile.Offset.x + 1, currentProfile.Offset.y);


            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                currentProfile.Offset = new Vector2(currentProfile.Offset.x, currentProfile.Offset.y + 1);

            }
        }

    }

    public Vector2 GetPoint(AdroitProfiler_AutoClicker_Configuration profile)
    {
        return new Vector2(
            (Screen.width / profile.MousePosition.x) + profile.Offset.x,
          ((Screen.height / profile.MousePosition.y) - profile.Offset.y));
    }

    public void Click(AdroitProfiler_AutoClicker_Configuration profile)
    {
        if (profile.Enabled == false) return;
        if (AutoClicker == null) return;
        var vector = GetPoint(profile);
        Debug.Log("On 0.1s Heartbeat | AutoClicker | Click | x:" + vector.x + " y:" + vector.y);
        AutoClicker.ClickAt(vector.x, vector.y);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded_Static;
    }

    private static void OnSceneLoaded_Static(Scene scene, LoadSceneMode mode)
    {
        var _this = GameObject.FindObjectOfType<AdroitProfiler_AutoClicker_Heartbeat>();
        _this.OnScenLoaded(scene, mode);
    }

    private void OnScenLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneName = scene.path;
        if (Configurations_CSV != null && Configurations.Count == 0)
        {
            GetConfigurationsFromSettingsFile();
        }
        AutoClicker = null;
        Debug.Log("scene.path : " + scene.path);


        var eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            AutoClicker = eventSystem.gameObject.AddComponent<AdroitProfiler_AutoClicker_StandaloneInputModule>();
        }

    }

    private void OnTenthHeartbeat()
    {
        Configurations.ForEach(Profile =>
        {
            Click(Profile);
        });
    }
    private void OnSecondHeartbeat()
    {
        Configurations.ForEach(Profile =>
        {
            Click(Profile);
        });
    }

    private void OnQuarterHeartbeat()
    {
        Configurations.ForEach(Profile =>
        {
            Click(Profile);
        });
    }

    private void OnHalfHeartbeat()
    {
        Configurations.ForEach(Profile =>
        {
            Click(Profile);
        });
    }

    private void On5SecondHeartbeat()
    {
        Configurations.ForEach(Profile =>
        {
            Click(Profile);
        });
    }

    private void On10SecondHeartbeat()
    {
        Configurations.ForEach(Profile =>
        {
            Click(Profile);
        });
    }

    public void SetSlot(int number)
    {
        if (Configurations.Count <= number)
        {
            return; //TODO create a new one or something
        }
        if (SettingSlot == true)
        {
            SettingSlot = false;
        }
        else
        {
            SettingSlot = true;
            CurrentSlotBeingSet = number;
            Configurations[number].Enabled = true;
        }

    }

    public void GetConfigurationsFromSettingsFile()
    {
        var Configurations_strings = Configurations_CSV.text.Split("\n");
        foreach (var profile_string in Configurations_strings)
        {
            var settings = profile_string.Split(",");
            if (settings.Length < 6) return;
            var profile = new AdroitProfiler_AutoClicker_Configuration();
           
            profile.Label = settings[0];
            profile.Enabled = true;
            profile.MousePosition = new Vector2(float.Parse(settings[1]), float.Parse(settings[2]));
            profile.Offset = new Vector2(float.Parse(settings[3]), float.Parse(settings[4]));
            profile.StartInScene = settings[5];
            Configurations.Add(profile);
        }
    }
    public void ExportSettings()
    {
        var fileName = "AdroitProfiler_Configurations_AutoClicker.csv";
        var Path = Application.streamingAssetsPath.Replace("/StreamingAssets", "/") + "Profiling/" + fileName;
        StreamWriter writer = new StreamWriter(Path);
        var text = "";
        foreach (var config in Configurations)
        {
            var line = "";
            line += config.Label.ToString() + ",";
            line += config.MousePosition.x + ",";
            line += config.MousePosition.y + ",";
            line += config.Offset.x + ",";
            line += config.Offset.y + ",";
            line += config.StartInScene + ",\n";
            text += line;
        }
        writer.Write(text);
        writer.Close();
    }
}