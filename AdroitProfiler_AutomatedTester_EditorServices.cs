#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AdroitProfiler_AutomatedTester_EditorServices
{
    public static void OnInspectorGUI_AutomatedTester_Config(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.Label = EditorGUILayout.TextField("Label", config.Label);
     
        config.ConfigType = (AdroitProfiler_AutomatedTester_Configuration_Type)EditorGUILayout.EnumPopup("Type", config.ConfigType);

        if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoBroadcaster)
        {
            OnInspectorGUI_AutoBroadcaster(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoChangeScene)
        {
            OnInspectorGUI_AutoChangeScene(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoMover)
        {
            OnInspectorGUI_AutoMover(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoLookAt)
        {
            OnInspectorGUI_AutoLookAt(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoMoveTo)
        {
            OnInspectorGUI_AutoMoveTo(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoTeleportTo)
        {
            OnInspectorGUI_AutoTeleportTo(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoClicker)
        {
            OnInspectorGUI_AutoClicker(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoClickTarget)
        {
            OnInspectorGUI_AutoClickTarget(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoChooseDialogChoice)
        {
            OnInspectorGUI_AutoChooseDialogChoice(config);
        }
    }

    public static void OnInspectorGUI_AutoBroadcaster(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureHeartbeat(config) == false) return;
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.Function = EditorGUILayout.TextField("Function", config.Function);
        config.Value = EditorGUILayout.TextField("Value", config.Value);


        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
          InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
           StartTime_Field(config);
           EndTime_Field(config);
        }
    }

    public static void OnInspectorGUI_AutoChangeScene(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        SetTimingTo_InvokeAtTime(config);

        config.Target = EditorGUILayout.TextField("Target Scene", config.Target);
      InvokeAtTime_Field(config);

    }

    public static void OnInspectorGUI_AutoMover(AdroitProfiler_AutomatedTester_Configuration config)
    {
        SetTimingTo_InvokeDuringTimespan(config);

        config.Movement = (AdroitProfiler_AutomatedTester_Configuration_MovementType)EditorGUILayout.EnumPopup("Movement Type", config.Movement);
        if (config.Movement == AdroitProfiler_AutomatedTester_Configuration_MovementType.Unselected)
        {
            return;
        }
        config.MoveSpeed = EditorGUILayout.FloatField("Movement Speed", config.MoveSpeed);
        config.TurnSpeed = EditorGUILayout.FloatField("Turn Speed", config.TurnSpeed);
        StartTime_Field(config);
        EndTime_Field(config);
    }

    private static void SetTimingTo_InvokeDuringTimespan(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.Heartbeat_Timing = AdroitProfiler_Timing.InvokeDuringTimespan;
        GUILayout.Label("Timing");
        GUILayout.Label("InvokeDuringTimespan");
        GUILayout.Space(20);
    }

    private static void SetTimingTo_InvokeAtTime(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.Heartbeat_Timing = AdroitProfiler_Timing.InvokeAtTime;
        GUILayout.Label("Timing");
        GUILayout.Label("InvokeAtTime");
        GUILayout.Space(20);

    }

    public static void OnInspectorGUI_AutoChooseDialogChoice(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureHeartbeat(config) == false) return;

      
            if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
          InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
           StartTime_Field(config);
           EndTime_Field(config);
        }

        config.DialogOption = (AdroitProfiler_AutomatedTester_DialogOptions)EditorGUILayout.EnumPopup("Dialog Option", config.DialogOption);
        if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.Unselected)
        {
            return;
        }

        if (config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.SortAlphabetically_PickAtIndex ||
           config.DialogOption == AdroitProfiler_AutomatedTester_DialogOptions.Unsorted_PickAtIndex)
        {
            config.DialogOptionIndex = EditorGUILayout.IntField("Dialog Option Index", config.DialogOptionIndex);
        }
    }

    public static void OnInspectorGUI_AutoMoveTo(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        SetTimingTo_InvokeDuringTimespan(config);
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.MoveSpeed = EditorGUILayout.FloatField("Movement Speed", config.MoveSpeed);
       StartTime_Field(config);
       EndTime_Field(config);
    }

    public static void OnInspectorGUI_AutoTeleportTo(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        SetTimingTo_InvokeAtTime(config);
        config.Target = EditorGUILayout.TextField("Target", config.Target);
      InvokeAtTime_Field(config);
    }

    public static void OnInspectorGUI_AutoLookAt(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        SetTimingTo_InvokeAtTime(config);

        config.Target = EditorGUILayout.TextField("Target", config.Target);
      InvokeAtTime_Field(config);
    }

    public static void OnInspectorGUI_AutoClicker(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureHeartbeat(config) == false) return;
        config.MousePosition = EditorGUILayout.Vector2IntField("Mouse Position", config.MousePosition);
        config.Offset = EditorGUILayout.Vector2IntField("Offset", config.Offset);
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
          InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            StartTime_Field(config);
            EndTime_Field(config);
        }
    }

    public static void OnInspectorGUI_AutoClickTarget(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureHeartbeat(config) == false) return;
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.Offset = EditorGUILayout.Vector2IntField("Offset", config.Offset);
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
          InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
           StartTime_Field(config);
           EndTime_Field(config);
        }
    }

    public static void StartTime_Field(AdroitProfiler_AutomatedTester_Configuration config)
    {
        EditorGUILayout.BeginHorizontal();
        
        config.StartTime = Mathf.Max(0,EditorGUILayout.FloatField("Start", config.StartTime,GUILayout.ExpandWidth(true)));
      

        if (GUILayout.Button("Capture Start Time"))
        {

            config.StartTime = Time.timeSinceLevelLoad;
        }
        EditorGUILayout.EndHorizontal();
    }
    public static void EndTime_Field(AdroitProfiler_AutomatedTester_Configuration config)
    {
        EditorGUILayout.BeginHorizontal();
        config.EndTime = Mathf.Max(0, EditorGUILayout.FloatField("End", config.EndTime, GUILayout.ExpandWidth(true)));
        if (GUILayout.Button("Capture End Time"))
        {
            config.EndTime = Time.timeSinceLevelLoad;
        }
        EditorGUILayout.EndHorizontal();
    }
    public static void InvokeAtTime_Field(AdroitProfiler_AutomatedTester_Configuration config)
    {
        EditorGUILayout.BeginHorizontal();

        config.InvokeAtTime = Mathf.Max(0, EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime, GUILayout.ExpandWidth(true)));

        if (GUILayout.Button("Capture Current Time"))
        {
            config.InvokeAtTime = Time.timeSinceLevelLoad;
        }
        EditorGUILayout.EndHorizontal();

    }
    public static void ConfigureScene(AdroitProfiler_AutomatedTester_Configuration config)
    {
        EditorGUILayout.BeginHorizontal();
        config.StartInEveryScene = EditorGUILayout.Toggle(config.StartInEveryScene?"Scene (uncheck for specific scene)": "Scene (check for every scene)", config.StartInEveryScene);
        if (config.StartInEveryScene == false)
        {
            EditorGUILayout.LabelField("Scene Name", GUILayout.MaxWidth(120));

            if(config.StartInScene == "")
            {
                if (GUILayout.Button("Use Current Scene"))
                {
                    config.StartInScene = SceneManager.GetActiveScene().name;
                }
            }

            if (config.StartInScene != "")
            {
                var sceneName = EditorGUILayout.TextField( config.StartInScene);
                 if (sceneName.EndsWith(".unity") == true && sceneName != "")
                {
                    config.StartInScene = sceneName.Replace(".unity", "");
                }
                else
                {
                    config.StartInScene = sceneName;
                }
            }
        }
        else { 
        EditorGUILayout.LabelField("(This will run in every scene.)");
        }
        EditorGUILayout.EndHorizontal();

    }

    public static bool ConfigureHeartbeat(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.Heartbeat_Timing = (AdroitProfiler_Timing)EditorGUILayout.EnumPopup("Timing", config.Heartbeat_Timing);
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.Unselected)
        {
            return false;
        }
        return true;

    }

}
#endif