
using UnityEditor;

public static class AdroitProfiler_AutomatedTester_EditorServices
{
    public static void OnInspectorGUI_AutomatedTester_Config(AdroitProfiler_AutomatedTester_Configuration config)
    {
        //config.Label = EditorGUILayout.TextField("Label", config.Label);
     
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
    public static void ConfigureScene(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.StartInEveryScene = EditorGUILayout.Toggle("Start In Every Scene", config.StartInEveryScene);

        if (config.StartInEveryScene == false)
        {
            config.StartInScene = EditorGUILayout.TextField("Invoke At Scene", config.StartInScene);
        }

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

    public static void OnInspectorGUI_AutoBroadcaster(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureHeartbeat(config) == false) return;
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.Function = EditorGUILayout.TextField("Function", config.Function);
        config.Value = EditorGUILayout.TextField("Value", config.Value);


        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            config.InvokeAtTime = EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            config.StartTime = EditorGUILayout.FloatField("Start", config.StartTime);
            config.EndTime = EditorGUILayout.FloatField("End", config.EndTime);
        }
    }

    public static void OnInspectorGUI_AutoChangeScene(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);

        config.Target = EditorGUILayout.TextField("Target Scene", config.Target);
        config.InvokeAtTime = EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime);

    }

    public static void OnInspectorGUI_AutoMover(AdroitProfiler_AutomatedTester_Configuration config)
    {
        config.Movement = (AdroitProfiler_AutomatedTester_Configuration_MovementType)EditorGUILayout.EnumPopup("Movement Type", config.Movement);
        if (config.Movement == AdroitProfiler_AutomatedTester_Configuration_MovementType.Unselected)
        {
            return;
        }
        config.MoveSpeed = EditorGUILayout.FloatField("Movement Speed", config.MoveSpeed);
        config.TurnSpeed = EditorGUILayout.FloatField("Turn Speed", config.TurnSpeed);
        config.StartTime = EditorGUILayout.FloatField("Start", config.StartTime);
        config.EndTime = EditorGUILayout.FloatField("End", config.EndTime);
    }

    public static void OnInspectorGUI_AutoChooseDialogChoice(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureHeartbeat(config) == false) return;

      
            if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            config.InvokeAtTime = EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            config.StartTime = EditorGUILayout.FloatField("Start", config.StartTime);
            config.EndTime = EditorGUILayout.FloatField("End", config.EndTime);
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
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.MoveSpeed = EditorGUILayout.FloatField("Movement Speed", config.MoveSpeed);
        config.StartTime = EditorGUILayout.FloatField("Start", config.StartTime);
        config.EndTime = EditorGUILayout.FloatField("End", config.EndTime);
    }

    public static void OnInspectorGUI_AutoTeleportTo(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.InvokeAtTime = EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime);
    }

    public static void OnInspectorGUI_AutoLookAt(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        config.Target = EditorGUILayout.TextField("Target", config.Target);
        config.InvokeAtTime = EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime);
    }

    public static void OnInspectorGUI_AutoClicker(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigureScene(config);
        if (ConfigureHeartbeat(config) == false) return;
        config.MousePosition = EditorGUILayout.Vector2IntField("Mouse Position", config.MousePosition);
        config.Offset = EditorGUILayout.Vector2IntField("Offset", config.Offset);
        if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        {
            config.InvokeAtTime = EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            config.StartTime = EditorGUILayout.FloatField("Start", config.StartTime);
            config.EndTime = EditorGUILayout.FloatField("End", config.EndTime);
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
            config.InvokeAtTime = EditorGUILayout.FloatField("Invoke At Time", config.InvokeAtTime);
        }
        else if (config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan)
        {
            config.StartTime = EditorGUILayout.FloatField("Start", config.StartTime);
            config.EndTime = EditorGUILayout.FloatField("End", config.EndTime);
        }
    }


}