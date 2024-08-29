using UnityEngine.SceneManagement;

interface AdroitProfiler_AutomatedTester_IAutomate
{
    public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config);
    public void OnSceneLoaded(AdroitProfiler_AutomatedTester_Configuration config, Scene scene);
}