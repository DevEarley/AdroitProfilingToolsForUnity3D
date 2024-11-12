#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[Icon("Assets/Profiling/Icons/test-config.png")]

[CustomEditor(typeof(AdroitProfiler_AutomatedTester_Configuration))]
public class AdroitProfiler_AutomatedTester_Configuration_Editor : Editor
{


    private Vector2 Scroll_IN = Vector2.zero;
    private Vector2 Scroll = Vector2.zero;
    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        var config = target as AdroitProfiler_AutomatedTester_Configuration;
        EditorGUI.BeginChangeCheck();

        AdroitProfiler_AutomatedTester_EditorServices.OnInspectorGUI_AutomatedTester_Config((config), Scroll, out Scroll);


        EditorGUI.EndChangeCheck();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(config);
    }
}
#endif

