using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using static AdroitProfiler_State;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(AdroitProfiler_AutoBroadcaster_Heartbeat))]
public class AdroitProfiler_AutoBroadcaster_Button : Editor
{

    override public void OnInspectorGUI()
    {
        AdroitProfiler_AutoBroadcaster_Heartbeat AutoBroadcaster = (AdroitProfiler_AutoBroadcaster_Heartbeat)target;
        if (AutoBroadcaster.Configurations.Count > 0 && GUILayout.Button("Export Settings"))
        {
            AutoBroadcaster.ExportSettings();
        }
        if (AutoBroadcaster.Configurations_CSV != null && GUILayout.Button("Import Settings"))
        {
            AutoBroadcaster.GetConfigsFromSettingsFile();
        }
        if ( GUILayout.Button("New Config At Current Time"))
        {
            AutoBroadcaster.NewConfigAtTime();
        }
        foreach (var broadcast in AutoBroadcaster.Configurations.Where(x=>x.StartInScene == AutoBroadcaster.CurrentSceneName &&  x.Sent == false))
        {
            if ( GUILayout.Button(broadcast.Function +" | "+broadcast.Value))
            {
                AutoBroadcaster.BroadcastMessageToGO(broadcast);
            }
        }
        DrawDefaultInspector();
    }
}
#endif

[System.Serializable]
public class AdroitProfiler_AutoBroadcaster_Configuration
{
    public string GameObjectPath;
    public string Function;
    public string Value;

    public string StartInScene;
    public float Time = 0.0f;
    public bool Sent = false;

}
[RequireComponent(typeof(AdroitProfiler_State))]

public class AdroitProfiler_AutoBroadcaster_Heartbeat : MonoBehaviour
{
    public AdroitProfiler_Heartbeat_Timing Heartbeat_Timing = AdroitProfiler_Heartbeat_Timing.None;
    private CharacterController characterController;
    public TextAsset Configurations_CSV;
    public List<AdroitProfiler_AutoBroadcaster_Configuration> Configurations = new List<AdroitProfiler_AutoBroadcaster_Configuration>();

    private AdroitProfiler_State AdroitProfiler_State;

    [HideInInspector]
    public string CurrentSceneName = "";

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {

        SceneManager.sceneLoaded += OnSceneLoaded_Static;
    }

    public void NewConfigAtTime()
    {
        var newConfig = new AdroitProfiler_AutoBroadcaster_Configuration();
        newConfig.Time = Time.timeSinceLevelLoad;
        newConfig.StartInScene = CurrentSceneName;
        Configurations.Add(newConfig);
    }

    private static void OnSceneLoaded_Static(Scene scene, LoadSceneMode mode)
    {
        var _this = FindObjectOfType<AdroitProfiler_AutoBroadcaster_Heartbeat>();
        _this.OnScenLoaded(scene, mode);
    }

    private void Start()
    {
        AdroitProfiler_State = gameObject.GetComponent<AdroitProfiler_State>();
        if (Configurations_CSV != null && Configurations.Count == 0)
        {
            GetConfigsFromSettingsFile();
        }
        switch (Heartbeat_Timing)
        {
            case AdroitProfiler_Heartbeat_Timing.EveryTenthSecond:
                AdroitProfiler_State.onTenth_Heartbeat_delegates.Add(UpdateMessages);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryQuarterSecond:
                AdroitProfiler_State.onQuarter_Heartbeat_delegates.Add(UpdateMessages);
                break;
            case AdroitProfiler_Heartbeat_Timing.EveryHalfSecond:
                AdroitProfiler_State.onHalf_Heartbeat_delegates.Add(UpdateMessages);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every5Seconds:
                AdroitProfiler_State.on5s_Heartbeat_delegates.Add(UpdateMessages);
                break;
            case AdroitProfiler_Heartbeat_Timing.Every10Seconds:
                AdroitProfiler_State.on10s_Heartbeat_delegates.Add(UpdateMessages);
                break;
        }
    }
    
    private void OnScenLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneName = scene.path;
        if (Configurations_CSV != null && Configurations.Count == 0)
        {
            GetConfigsFromSettingsFile();
        }

        Debug.Log("scene.path : " + scene.path);


       
    }


    private void UpdateMessages()
    {
        foreach (var config in Configurations.Where(x => x.StartInScene == CurrentSceneName && x.Sent == false))
        {
            if (config.Time <= Time.timeSinceLevelLoad)
            {
                BroadcastMessageToGO(config);
            }
           
        }
    }

    public void BroadcastMessageToGO(AdroitProfiler_AutoBroadcaster_Configuration config)
    {
        Debug.Log("Try to BroadcastMessage To GO");
        config.Sent = true;
        var GO = GameObject.Find(config.GameObjectPath);
        GO.SendMessage(config.Function, config.Value);
    }

   

    public void GetConfigsFromSettingsFile()
    {
        Configurations.Clear();
        var profiles_strings = Configurations_CSV.text.Split("\n");
        foreach (var profile_string in profiles_strings)
        {
            var settings = profile_string.Split(",");
            if (settings.Length < 5) return;
            var profile = new AdroitProfiler_AutoBroadcaster_Configuration();
          
            profile.GameObjectPath = settings[0];
            profile.Function = settings[1];
            profile.Value = settings[2];
            profile.StartInScene = settings[3];
            profile.Time = float.Parse(settings[4]);
          
            Configurations.Add(profile);
        }
    }

    public void ExportSettings()
    {
        var fileName = "AdroitProfiler_Configurations_AutoBroadcaster.csv";
        var Path = Application.streamingAssetsPath.Replace("/StreamingAssets", "/") + "Profiling/" + fileName;
        StreamWriter writer = new StreamWriter(Path);
        var text = "";
        foreach (var config in Configurations)
        {
            var line = "";
            line += config.GameObjectPath + ",";
            line += config.Function + ",";
            line += config.Value + ",";
            line += config.StartInScene + ",";
            line += config.Time + ",\n";
            text += line;
        }
        writer.Write(text);
        writer.Close();
    }

  
}
