#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdroitProfiler_AutomatedTester_Configuration))]
public class AdroitProfiler_AutomatedTester_Configuration_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        var config = target as AdroitProfiler_AutomatedTester_Configuration;

        AdroitProfiler_AutomatedTester_EditorServices.OnInspectorGUI_AutomatedTester_Config((config));
       

        serializedObject.ApplyModifiedProperties();

    }
}
#endif

[CreateAssetMenu(fileName = "Adroit Profiler Configuration", menuName = "Adroit Profiler/Configuration", order = 2)]
public class AdroitProfiler_AutomatedTester_Configuration : ScriptableObject
{
    public AdroitProfiler_AutomatedTester_Configuration_Type ConfigType = AdroitProfiler_AutomatedTester_Configuration_Type.Unselected;
    public AdroitProfiler_Timing Heartbeat_Timing = AdroitProfiler_Timing.Unselected;
    public AdroitProfiler_AutomatedTester_Configuration_MovementType Movement = AdroitProfiler_AutomatedTester_Configuration_MovementType.Unselected;
    public AdroitProfiler_AutomatedTester_DialogOptions DialogOption = AdroitProfiler_AutomatedTester_DialogOptions.Unselected;
    public string Target = "";
    public bool Enabled = true;
    public Vector2Int MousePosition = Vector2Int.one * 2;
    public Vector2Int Offset = Vector2Int.zero;
    public Vector3 WorldPosition = Vector3.zero;
    public string StartInScene;
    public float MoveSpeed = 1.0f;
    public float TurnSpeed = 1.0f;
    public float StartTime;
    public float EndTime;
    public string GameObjectPath;
    public string Function;
    public string Value;
    public float InvokeAtTime = 0.0f;
    public bool Sent = false;
    public bool StartInEveryScene = false;
    public int DialogOptionIndex = 0;
    public bool SkipDialog = true;
    public bool CaptureDialog = true;
    public AdroitProfiler_AutomatedTester_Configuration()
    {
        
    }

    public AdroitProfiler_AutomatedTester_Configuration(AdroitProfiler_AutomatedTester_Configuration config)
    {
        ConfigType = config.ConfigType;
        Heartbeat_Timing = config.Heartbeat_Timing;
        Movement = config.Movement;
        DialogOption = config.DialogOption;
        Target = config.Target;
        Enabled = config.Enabled;
        MousePosition = config.MousePosition;
        Offset = config.Offset;
        WorldPosition = config.WorldPosition;
        StartInScene = config.StartInScene;
        MoveSpeed = config.MoveSpeed;
        TurnSpeed = config.TurnSpeed;
        StartTime = config.StartTime;
        EndTime = config.EndTime;
        GameObjectPath = config.GameObjectPath;
        Function = config.Function;
        Value = config.Value;
        InvokeAtTime = config.InvokeAtTime;
        Sent = config.Sent;
        StartInEveryScene = config.StartInEveryScene;
        DialogOptionIndex = config.DialogOptionIndex;
        SkipDialog = config.SkipDialog;
        CaptureDialog = config.CaptureDialog;
    }

}
