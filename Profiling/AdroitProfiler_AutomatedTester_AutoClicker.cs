using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


 
public class AdroitProfiler_AutomatedTester_AutoClicker : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
{
    [HideInInspector]
    public AdroitProfiler_AutoClicker_StandaloneInputModule AutoClicker;


    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        if (AutoClicker == null) return;
        var vector = GetPoint(config);
        AutoClicker.ClickAt(vector.x, vector.y);
    }

    public void OnSceneLoaded(List<AdroitProfiler_AutomatedTester_Configuration> config, UnityEngine.SceneManagement.Scene scene)
    {
        AutoClicker = null;
        var eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            AutoClicker = eventSystem.gameObject.AddComponent<AdroitProfiler_AutoClicker_StandaloneInputModule>();
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
