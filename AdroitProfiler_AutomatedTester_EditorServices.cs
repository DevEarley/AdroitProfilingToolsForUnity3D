#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public static class AdroitProfiler_AutomatedTester_EditorServices
{
    public static void OnInspectorGUI_AutomatedTester_Config(AdroitProfiler_AutomatedTester_Configuration config)
    {

        config.ConfigType = (AdroitProfiler_AutomatedTester_Configuration_Type)EditorGUILayout.EnumPopup("Type", config.ConfigType);

        if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.Unselected)
        {
            GUILayout.Label("Select a type.");
            return;
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoTest)
        {
            OnInspectorGUI_AutoTest(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoBroadcaster)
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
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoPause)
        {
            OnInspectorGUI_AutoPause(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoChangeTestCase)
        {
            OnInspectorGUI_AutoChangeTestCase(config);
        }
        else if (config.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoUpdateWebpage)
        {
            OnInspectorGUI_AutoUpdateWebPage(config);
        }
    }

    public static void OnInspectorGUI_AutoBroadcaster(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureTimingOrSignal(config) == false) return;
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.Function = EditorGUILayout.TextField("Function", config.Function);
        config.Value = EditorGUILayout.TextField("Value", config.Value);


        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            StartAmdEndTime_Field(config);
       
        }
    }

    public static void OnInspectorGUI_AutoUpdateWebPage(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureTimingOrSignal(config) == false) return;
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            StartAmdEndTime_Field(config);
       
        }
    }

    public static void OnInspectorGUI_AutoTest(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.TestType = (AdroitProfiler_AutomatedTester_AutoTestType)EditorGUILayout.EnumPopup("Test Type", config.TestType);
        if (config.TestType == AdroitProfiler_AutomatedTester_AutoTestType.Unselected) return;
        ConfigureScene(config);
        if (config.TestType == AdroitProfiler_AutomatedTester_AutoTestType.PassAtTime ||
            config.TestType == AdroitProfiler_AutomatedTester_AutoTestType.FailAtTime)
        {
            SetTimingTo_InvokeAtTime(config);
            InvokeAtTime_Field(config);
        }
        else if (config.TestType == AdroitProfiler_AutomatedTester_AutoTestType.FunctionShouldReturnTrue || config.TestType == AdroitProfiler_AutomatedTester_AutoTestType.FunctionShouldReturnFalse)
        {
            if (ConfigureTimingOrSignal(config) == false) return;
            if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
            {
                InvokeAtTime_Field(config);
            }
            else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
            {
                StartAmdEndTime_Field(config);
          
            }
            config.Target = EditorGUILayout.TextField("Target", config.Target);
            config.Function = EditorGUILayout.TextField("Function", config.Function);
        }
    }

    public static void OnInspectorGUI_AutoChangeScene(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);

        ConfigureSignal(config);
   
        SetTimingTo_InvokeAtTime(config);
        InvokeAtTime_Field(config);
        

        config.Target = EditorGUILayout.TextField("Target Scene", config.Target);

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
        StartAmdEndTime_Field(config);
   
    }

    public static void OnInspectorGUI_AutoChooseDialogChoice(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureTimingOrSignal(config) == false) return;

        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            StartAmdEndTime_Field(config);
        
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

    public static void OnInspectorGUI_AutoPause(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);

        SetTimingTo_InvokeAtTime(config);
        InvokeAtTime_Field(config);

    }

    public static void OnInspectorGUI_AutoChangeTestCase(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        ConfigureSignal(config);

        SetTimingTo_InvokeAtTime(config);
        InvokeAtTime_Field(config);

    }

    public static void OnInspectorGUI_AutoMoveTo(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        ConfigureSignal(config);

        SetTimingTo_InvokeDuringTimespan(config);
        StartAmdEndTime_Field(config);
      
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.MoveSpeed = EditorGUILayout.FloatField("Movement Speed", config.MoveSpeed);
    }

    public static void OnInspectorGUI_AutoTeleportTo(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        ConfigureSignal(config);

        SetTimingTo_InvokeAtTime(config);

        config.Target = EditorGUILayout.TextField("Target", config.Target);
        InvokeAtTime_Field(config);
    }

    public static void OnInspectorGUI_AutoLookAt(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        ConfigureSignal(config);

        SetTimingTo_InvokeAtTime(config);


        config.Target = EditorGUILayout.TextField("Target", config.Target);
        InvokeAtTime_Field(config);
    }

    public static void OnInspectorGUI_AutoClicker(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureTimingOrSignal(config) == false) return;
        ConfigureMousePositionAndOffset(config);
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            StartAmdEndTime_Field(config);
         
        }
    }

    public static void OnInspectorGUI_AutoClickTarget(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureTimingOrSignal(config) == false) return;
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.Offset = EditorGUILayout.Vector2IntField("Offset", config.Offset);
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            InvokeAtTime_Field(config);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            StartAmdEndTime_Field(config);
  
        }
    }

    public static void StartAmdEndTime_Field(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var TimeContext = config.UsingSignal ? "(after signal recieved.)" : "(after scene loaded.)";

        GUILayout.Label(TimeContext);
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Start Time ", GUILayout.ExpandWidth(true));
        ShowFormattedTime(config.StartTime);
        config.StartTime = Mathf.Max(0, EditorGUILayout.FloatField(config.StartTime, GUILayout.ExpandWidth(false)));
        if (config.UsingSignal == false && GUILayout.Button("Capture Start Time"))
        {
            config.StartTime = Time.timeSinceLevelLoad;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("End Time", GUILayout.ExpandWidth(true));
        ShowFormattedTime(config.EndTime);
        config.EndTime = Mathf.Max(0, EditorGUILayout.FloatField(config.EndTime, GUILayout.ExpandWidth(false)));
        if (config.UsingSignal == false && GUILayout.Button("Capture End Time"))
        {
            config.EndTime = Time.timeSinceLevelLoad;
        }
        EditorGUILayout.EndHorizontal();
    }

   

    private static void ShowFormattedTime(float time)
    {
        var modSeconds = time % 60.0f;
        var minutes = (time - modSeconds) / 60.0f;
        GUILayout.Label("(" + minutes + ":" + modSeconds.ToString("00") + ")");
    }

    public static void InvokeAtTime_Field(AdroitProfiler_AutomatedTester_Configuration config)
    {
        var TimeContext = config.UsingSignal ? "after signal recieved." : "after scene loaded.";
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Invoke At Time " + TimeContext, GUILayout.ExpandWidth(true));
        ShowFormattedTime(config.InvokeAtTime);
        config.InvokeAtTime = Mathf.Max(0, EditorGUILayout.FloatField(config.InvokeAtTime, GUILayout.ExpandWidth(false)));
        if (config.UsingSignal == false)
        {
            if (GUILayout.Button("Capture Current Time"))
            {
                config.InvokeAtTime = Time.timeSinceLevelLoad;
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private static void SetTimingTo_InvokeDuringTimespan(AdroitProfiler_AutomatedTester_Configuration config)
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        config.Heartbeat_Timing = AdroitProfiler_Timing.InvokeDuringTimespan;
        GUILayout.Label("Timing");
        GUILayout.EndHorizontal();
    }

    private static void SetTimingTo_InvokeAtTime(AdroitProfiler_AutomatedTester_Configuration config)
    {
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        config.Heartbeat_Timing = AdroitProfiler_Timing.InvokeAtTime;
        GUILayout.Label("Timing");
        GUILayout.EndHorizontal();

    }

    public static void ConfigureScene(AdroitProfiler_AutomatedTester_Configuration config)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Start in every Scene");
        config.StartInEveryScene = EditorGUILayout.Toggle(config.StartInEveryScene);
        GUILayout.Label(config.StartInEveryScene ? "(This will run in every scene. Uncheck to choose scene)" : "");

        if (config.StartInEveryScene == false)
        {
            EditorGUILayout.LabelField("Scene Name", GUILayout.MaxWidth(120));

            var sceneName = EditorGUILayout.TextField(config.StartInScene);
            if (sceneName.EndsWith(".unity") == true && sceneName != "")
            {
                config.StartInScene = sceneName.Replace(".unity", "");
            }
            else
            {
                config.StartInScene = sceneName;
            }
            if (GUILayout.Button("Use Current Scene"))
            {
                config.StartInScene = SceneManager.GetActiveScene().name;
            }

        }


        EditorGUILayout.EndHorizontal();

    }

    public static bool ConfigureTimingOrSignal(AdroitProfiler_AutomatedTester_Configuration config)
    {
        GUILayout.Label("Using Signal");
        ConfigureSignal(config);
        config.Heartbeat_Timing = (AdroitProfiler_Timing)EditorGUILayout.EnumPopup("Timing", config.Heartbeat_Timing);
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.Unselected)
        {
            return false;
        }
        return true;

    }

    private static void ConfigureSignal(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.UsingSignal = EditorGUILayout.Toggle(config.UsingSignal);
        GUILayout.Label(config.UsingSignal ? "(Action will only happen after the signal is recieved)" : "");
        if (config.UsingSignal)
        {
            config.StartingSignal = EditorGUILayout.TextField("Starting Signal Message", config.StartingSignal);
            config.EndingSignal = EditorGUILayout.TextField("Ending Signal Message", config.EndingSignal);
        }
    }

    private static void ConfigureMousePositionAndOffset(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.AutoClickerAnchorPoint = (AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint)EditorGUILayout.EnumPopup("Anchor Point", config.AutoClickerAnchorPoint);
        switch (config.AutoClickerAnchorPoint)
        {
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.UpperLeft:
                config.MousePosition = new Vector2Int(0, 0);
                break;
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.UpperMiddle:
                config.MousePosition = new Vector2Int(2, 0);

                break;
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.UpperRight:
                config.MousePosition = new Vector2Int(1, 0);

                break;
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.CenterLeft:
                config.MousePosition = new Vector2Int(0, 2);

                break;
            default:
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.Unselected:
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.CenterMiddle:
                config.MousePosition = new Vector2Int(2, 2);
                break;
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.CenterRight:
                config.MousePosition = new Vector2Int(1, 2);
                break;
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.BottomLeft:
                config.MousePosition = new Vector2Int(0, 1);

                break;
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.BottomMiddle:
                config.MousePosition = new Vector2Int(2, 1);

                break;
            case AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.BottomRight:
                config.MousePosition = new Vector2Int(1, 1);
                break;
        }
        GUILayout.Label("Mouse Position: (" + config.MousePosition.x + ", " + config.MousePosition.y + ")");
        config.Offset = EditorGUILayout.Vector2IntField("Offset", config.Offset);
    }
}
#endif