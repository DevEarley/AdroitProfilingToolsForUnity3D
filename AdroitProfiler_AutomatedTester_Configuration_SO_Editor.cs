
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Icon("Assets/Profiling/Icons/test.png")]

[CustomEditor(typeof(AdroitProfiler_AutomatedTester_Configuration_TestCase))]
public class AdroitProfiler_AutomatedTester_Configuration_SO_Editor : Editor
{
   
    public override void OnInspectorGUI()
    {
        var config_SO = target as AdroitProfiler_AutomatedTester_Configuration_TestCase;

        //if (config_SO.TestCaseToImport != null)
        //{
        //    config_SO.ConfigurableActions = new List<AdroitProfiler_AutomatedTester_Configuration>(config_SO.TestCaseToImport.ConfigurableActions.Select(x => { return Instantiate(x); }));
        //    config_SO.TestCaseToImport = null;
        //}

        base.OnInspectorGUI();
        serializedObject.Update();

        //if (config_SO.ConfigToAdd != null)
        //{
        //    config_SO.ConfigurableActions.Add((config_SO.ConfigToAdd)); // type mis match
        //    config_SO.ConfigToAdd = null;
        //}
        int? indexToDelete = null;
        int? indexToDuplicate = null;
        EditorGUI.BeginChangeCheck();
        var index = 0;
        foreach (var config in config_SO.ConfigurableActions)
        {
            if (config == null) return;
            config.Enabled = EditorGUILayout.BeginToggleGroup(config.name + " (" + config.ConfigType.ToString() + ")", config.Enabled);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove from test case"))
            {
                indexToDelete = index;
            }
          

            GUILayout.EndHorizontal();

            AdroitProfiler_AutomatedTester_EditorServices.OnInspectorGUI_AutomatedTester_Config(config);


            EditorUtility.SetDirty(config);

            EditorGUILayout.EndToggleGroup();
            GUILayout.Space((20));

            index++;
        }
        if (indexToDelete != null)
        {
            config_SO.ConfigurableActions.RemoveAt(indexToDelete.Value);
        }
        EditorGUI.EndChangeCheck();
        EditorUtility.SetDirty(config_SO);

        serializedObject.ApplyModifiedProperties();


    }
}
#endif


