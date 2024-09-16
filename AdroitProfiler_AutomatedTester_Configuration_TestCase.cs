using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[Icon("Assets/Profiling/Icons/test.png")]
[CreateAssetMenu(fileName = "Adroit Profiler Test Case", menuName = "Adroit Profiler/Test Case", order = 1)]
public class AdroitProfiler_AutomatedTester_Configuration_TestCase : ScriptableObject
{
    public List<AdroitProfiler_AutomatedTester_Configuration> ConfigurableTests = new List<AdroitProfiler_AutomatedTester_Configuration>();
    public List<AdroitProfiler_AutomatedTester_Configuration> ConfigurableActions = new List<AdroitProfiler_AutomatedTester_Configuration>();
}
