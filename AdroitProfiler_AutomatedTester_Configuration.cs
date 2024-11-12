using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[Icon("Assets/Profiling/Icons/test-config.png")]

[CreateAssetMenu(fileName = "Configurable Action", menuName = "Adroit Profiler/Configurable Action", order = 2)]

public class AdroitProfiler_AutomatedTester_Configuration : ScriptableObject
{
    public AdroitProfiler_AutomatedTester_Configuration_Type ConfigType = AdroitProfiler_AutomatedTester_Configuration_Type.Unselected;
    public AdroitProfiler_Timing Heartbeat_Timing = AdroitProfiler_Timing.Unselected;
    public AdroitProfiler_AutomatedTester_Configuration_MovementType Movement = AdroitProfiler_AutomatedTester_Configuration_MovementType.Unselected;
    public AdroitProfiler_AutomatedTester_DialogOptions DialogOption = AdroitProfiler_AutomatedTester_DialogOptions.Unselected;
    public AdroitProfiler_AutomatedTester_AutoTestType TestType = AdroitProfiler_AutomatedTester_AutoTestType.Unselected;
    public AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint AutoClickerAnchorPoint = AdroitProfiler_AutomatedTester_AutoClickerAnchorPoint.Unselected;
    public string Target = "";

    public List<string> Targets = new List<string>() ;
    public string Source = "";
    public bool UseCharacterInterface = true;
    public bool Enabled = true;
    public bool UsingSignal = false;
    public bool TargetMultiple = false;
    public Vector2Int MousePosition = Vector2Int.one * 2;
    public Vector2Int Offset = Vector2Int.zero;
    public Vector3 WorldPosition = Vector3.zero;
    public string StartInScene;
    public float MoveSpeed = 1.0f;
    public float TurnSpeed = 1.0f;
    public float StartTime;
    public float EndTime;
    public string Function;
    public string LuaScript;
    public string Value;
    public float InvokeAtTime = 0.0f;
    public bool StartInEveryScene = false;
    public int DialogOptionIndex = 0;
    public bool SkipDialog = true;
    public bool CaptureDialog = true;
    public string StartingSignal;
    public string EndingSignal;

}
