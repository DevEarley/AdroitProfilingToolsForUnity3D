using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


 
public class AdroitProfiler_AutomatedTester_AutoClicker : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    [HideInInspector]
    public AdroitProfiler_AutoClicker_StandaloneInputModule AutoClicker;
    [HideInInspector]
    public bool disableClickers = false;

    public List<GameObject> Targets = new List<GameObject>();

    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (disableClickers == true) return;
        if (AutoClicker == null) return;
        var vector = Vector2.zero;
        if(config.Target!=null && config.Target != "")
        {
            var targetGO = Targets.FirstOrDefault(x => x.name == config.Target);
            vector = targetGO.GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("AdroitProfiler_AutomatedTester_AutoClicker | AnchoredPosition of GO: " + vector);
        }
        else 
        {
            vector = GetPoint(config);
        }
        AutoClicker.ClickAt(vector.x, vector.y);
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> configs, UnityEngine.SceneManagement.Scene scene)
    {
        Targets = new List<GameObject>();
        CacheTargetGameObjects_ForThisScene(configs);

        AutoClicker = null;
        var eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            AutoClicker = eventSystem.gameObject.AddComponent<AdroitProfiler_AutoClicker_StandaloneInputModule>();
        }
    }

    private void CacheTargetGameObjects_ForThisScene(List<AdroitProfiler_AutomatedTester_Configuration> configs)
    {
        foreach (var config in configs)
        {
            if (config.Target != null && config.Target != "")
            {
                var GO = GameObject.Find(config.Target);
                if (GO == null)
                {
                    Debug.LogErrorFormat("AdroitProfiler_AutomatedTester_AutoClicker | Could Not find Target");
                }
                else
                {
                    Targets.Add(GO);
                }
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public Vector2 GetPoint(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var anchorX = config.MousePosition.x == 0 ? 0 : (Screen.width / config.MousePosition.x);
        var anchorY = config.MousePosition.y == 0 ? 0 : (Screen.height / config.MousePosition.y);
        return new Vector2(
             anchorX + config.Offset.x,
          (anchorY - config.Offset.y));
    }
}

/*
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
        Configurations.Clear();
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
 
 */
