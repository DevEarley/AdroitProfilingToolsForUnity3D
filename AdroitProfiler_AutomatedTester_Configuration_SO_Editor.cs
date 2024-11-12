
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[Icon("Assets/Profiling/Icons/test.png")]

[CustomEditor(typeof(AdroitProfiler_AutomatedTester_Configuration_TestCase))]
public class AdroitProfiler_AutomatedTester_Configuration_SO_Editor : Editor
{
    private Vector2 Scroll_IN = Vector2.zero;
    private Vector2 Scroll = Vector2.zero;
    private List<AdroitProfiler_AutomatedTester_Configuration> hidden = new List<AdroitProfiler_AutomatedTester_Configuration>();
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
            GUILayout.BeginHorizontal();
        GUILayout.Label("====== Configurable Actions ======");
        if (GUILayout.Button("Show All"))
        {
            hidden.Clear();
        }
        if (GUILayout.Button("Hide All"))
        {
            hidden = config_SO.ConfigurableActions.ToList();

        }
        GUILayout.EndHorizontal();

        foreach (var config in config_SO.ConfigurableActions)
        {
            if (config == null) return;
            bool showing = hidden.Contains(config) == false;
            var message = (config.Enabled ? "Enabled" : "Disabled");

            GUILayout.BeginHorizontal();
            GUILayout.Label(config.name + " (" + config.ConfigType.ToString() + ")");
            if (GUILayout.Button("Show"))
            {
                if (hidden.Contains(config))
                {
                    hidden.Remove(config);
                }
                showing = true;
            }
            if (GUILayout.Button("Hide"))
            {
                if (hidden.Contains(config) == false)
                {
                    hidden.Add(config);
                }
                showing = false;

            }

            GUILayout.EndHorizontal();
            config.Enabled = EditorGUILayout.BeginToggleGroup(message, config.Enabled);

            if (showing == true)
            {

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Remove from test case"))
                {
                    indexToDelete = index;
                }
                GUILayout.EndHorizontal();

                AdroitProfiler_AutomatedTester_EditorServices.OnInspectorGUI_AutomatedTester_Config(config, Scroll,out Scroll);
            }

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


