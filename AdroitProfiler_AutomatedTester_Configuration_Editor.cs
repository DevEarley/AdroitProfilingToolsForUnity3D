#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(AdroitProfiler_AutomatedTester_Configuration))]
public class AdroitProfiler_AutomatedTester_Configuration_Editor : Editor
{
  


    public override void OnInspectorGUI()
    {

        serializedObject.Update();
        var config = target as AdroitProfiler_AutomatedTester_Configuration;
        EditorGUI.BeginChangeCheck();

        AdroitProfiler_AutomatedTester_EditorServices.OnInspectorGUI_AutomatedTester_Config((config));


        EditorGUI.EndChangeCheck();
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(config);
    }
}
#endif

