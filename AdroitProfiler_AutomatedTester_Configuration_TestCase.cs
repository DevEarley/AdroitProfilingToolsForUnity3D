using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(AdroitProfiler_AutomatedTester_Configuration_TestCase))]
public class AdroitProfiler_AutomatedTester_Configuration_SO_Editor : Editor
{
    
    public override void OnInspectorGUI()
    {
        var config_SO = target as AdroitProfiler_AutomatedTester_Configuration_TestCase;
        if (config_SO.TestCaseToImport != null)
        {
            config_SO.Configs = new List<AdroitProfiler_AutomatedTester_Configuration>(config_SO.TestCaseToImport.Configs.Select(x=> { return Instantiate(x); }));
            config_SO.TestCaseToImport = null;
        }

        base.OnInspectorGUI();
        if (config_SO.ConfigToAdd != null)
        {

          
                config_SO.Configs.Add((config_SO.ConfigToAdd)); // type mis match
            
            config_SO.ConfigToAdd = null;

        }
        int? indexToDelete = null;
        int? indexToDuplicate = null;
        var index = 0;

        foreach (var config in config_SO.Configs)
        {
            if (config == null) return;
            config.Enabled = EditorGUILayout.BeginToggleGroup(config.ConfigType.ToString() + " #" + index, config.Enabled);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove from test case"))
            {
                indexToDelete = index;
            }
            if (GUILayout.Button("Duplicate"))
            {
                indexToDuplicate = index;
            }
        
            GUILayout.EndHorizontal();

            AdroitProfiler_AutomatedTester_EditorServices.OnInspectorGUI_AutomatedTester_Config(config);
       

            EditorGUILayout.EndToggleGroup();
            GUILayout.Space((20));

            index++;
        }
        if (indexToDelete != null)
        {
            config_SO.Configs.RemoveAt(indexToDelete.Value);
        }
        if (indexToDuplicate != null)
        {
            config_SO.Configs.RemoveAt(indexToDelete.Value);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif



[System.Serializable]
[CreateAssetMenu(fileName = "Adroit Profiler Test Case", menuName = "Adroit Profiler/Test Case", order = 1)]
public class AdroitProfiler_AutomatedTester_Configuration_TestCase : ScriptableObject
{
    public AdroitProfiler_AutomatedTester_Configuration_TestCase TestCaseToImport;
    public AdroitProfiler_AutomatedTester_Configuration ConfigToAdd;
   
    public List<AdroitProfiler_AutomatedTester_Configuration> Configs = new List<AdroitProfiler_AutomatedTester_Configuration>();

   
}

[System.Serializable]
public enum AdroitProfiler_AutomatedTester_Configuration_MovementType
{
    Unselected = 0,
    MoveForward,
    MoveBackwards,
    TurnLeft,
    TurnRight
}

[System.Serializable]
public enum AdroitProfiler_AutomatedTester_Configuration_Type
{
    Unselected = 0,
    AutoBroadcaster,
    AutoChangeScene,
    AutoChooseDialogChoice,
    AutoClicker,
    AutoClickTarget,
    AutoLookAt,
    AutoMover,
    AutoMoveTo,
    AutoTeleportTo,
}

[System.Serializable]
public enum AdroitProfiler_AutomatedTester_DialogOptions
{
    Unselected = 0,
    SortAlphabetically_PickFirst,
    SortAlphabetically_PickLast,
    SortAlphabetically_PickAtIndex,
    Unsorted_PickFirst,
    Unsorted_PickLast,
    Unsorted_PickAtIndex,
    PickRandom
}
