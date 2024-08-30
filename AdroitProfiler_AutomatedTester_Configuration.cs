using UnityEngine;


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
    public string Label ;


}
