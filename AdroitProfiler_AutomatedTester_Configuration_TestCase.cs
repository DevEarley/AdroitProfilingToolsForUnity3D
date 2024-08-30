using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Adroit Profiler Test Case", menuName = "Adroit Profiler/Test Case", order = 1)]
public class AdroitProfiler_AutomatedTester_Configuration_TestCase : ScriptableObject
{
    public AdroitProfiler_AutomatedTester_Configuration_TestCase TestCaseToImport;
    public AdroitProfiler_AutomatedTester_Configuration ConfigToAdd;
   
    public List<AdroitProfiler_AutomatedTester_Configuration> Configs = new List<AdroitProfiler_AutomatedTester_Configuration>();

   
}
