
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdroitProfiler_Logger))]
[RequireComponent(typeof(AdroitProfiler_Heartbeat))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoChangeScene))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoBroadcaster))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoChooseDialogChoice))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoClicker))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoClickTarget))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoLookAt))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoMover))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoMoveTo))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoTeleportTo))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoChangeTestCase))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoPause))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoTest))]
#if UNITY_WEBGL

[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoUpdateWebpage))]
#endif

public class AdroitProfiler_AutomatedTester : MonoBehaviour
{
    [HideInInspector]
    public AdroitProfiler_AutomatedTester_Configuration_TestCase CurrentTestCase;

    public List<AdroitProfiler_AutomatedTester_Configuration_TestCase> GlobalTestCases = new List<AdroitProfiler_AutomatedTester_Configuration_TestCase>();
    public List<AdroitProfiler_AutomatedTester_Configuration_TestCase> TestCaseQueue = new List<AdroitProfiler_AutomatedTester_Configuration_TestCase>();

    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;
    private AdroitProfiler_Logger AdroitProfiler_Logger;
    private List<AdroitProfiler_AutomatedTester_Configuration> OnTenthSecond_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> OnQuarterSecond_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> OnHalfSecond_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> On1Second_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> On5Seconds_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> On10Seconds_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> AtTime_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> DuringTimespan_Configurations;

    private AdroitProfiler_AutomatedTester_AutoBroadcaster AutoBroadcaster;
    private AdroitProfiler_AutomatedTester_AutoChooseDialogChoice AutoChooseDialogChoice;
    private AdroitProfiler_AutomatedTester_AutoClicker AutoClicker;
    private AdroitProfiler_AutomatedTester_AutoClickTarget AutoClickTarget;
    private AdroitProfiler_AutomatedTester_AutoLookAt AutoLookAt;
    private AdroitProfiler_AutomatedTester_AutoMover AutoMover;
    private AdroitProfiler_AutomatedTester_AutoMoveTo AutoMoveTo;
    private AdroitProfiler_AutomatedTester_AutoTeleportTo AutoTeleportTo;
    private AdroitProfiler_AutomatedTester_AutoChangeTestCase AutoChangeTestCase;
    private AdroitProfiler_AutomatedTester_AutoChangeScene AutoChangeScene;
    private AdroitProfiler_AutomatedTester_AutoPause AutoPause;
    private AdroitProfiler_AutomatedTester_AutoTest AutoTest;

#if UNITY_WEBGL && !UNITY_EDITOR 
    private AdroitProfiler_AutomatedTester_AutoUpdateWebpage AutoUpdateWebpage;
#endif

    private int TestCaseIndex;

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {

        SceneManager.sceneLoaded += OnSceneLoaded_Static;
    }

    private static void OnSceneLoaded_Static(Scene scene, LoadSceneMode mode)
    {
        var _this = FindObjectOfType<AdroitProfiler_AutomatedTester>();
        _this.OnSceneLoaded(scene, mode);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupConfigLists();

        var testCases = new List<AdroitProfiler_AutomatedTester_Configuration_TestCase>();
        testCases.AddRange(GlobalTestCases);
        testCases.Add(CurrentTestCase);
        var nonNullTestCases = testCases.Where(x => x != null);
        var allConfigs = nonNullTestCases.SelectMany(x => x.ConfigurableActions);
        var allTests = nonNullTestCases.SelectMany(x => x.ConfigurableTests);
        var allConfigsAndTests = allConfigs.Concat(allTests);
        var dictionaryOfConfigsGroupedByTypeForThisScene = allConfigsAndTests.ToList()
            .Where(x => x.StartInEveryScene || x.StartInScene == scene.name)
            .GroupBy(x => x.ConfigType)
            .ToDictionary(group=>group.Key,group=>group.ToList());

        foreach (var configGroup in dictionaryOfConfigsGroupedByTypeForThisScene)
        {
            switch (configGroup.Key)
            {
                case AdroitProfiler_AutomatedTester_Configuration_Type.Unselected:
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoLookAt:
                    AutoLookAt.OnSceneLoaded(configGroup.Value, scene);
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoChangeTestCase:
                    AutoChangeTestCase.OnSceneLoaded(configGroup.Value, scene);
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoMoveTo:
                    AutoMoveTo.OnSceneLoaded(configGroup.Value, scene);
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoTeleportTo:
                    AutoTeleportTo.OnSceneLoaded(configGroup.Value, scene);
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoClickTarget:
                    AutoClickTarget.OnSceneLoaded(configGroup.Value, scene);
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoClicker:
                    AutoClicker.OnSceneLoaded(configGroup.Value, scene);
                    break;
#if UNITY_WEBGL && !UNITY_EDITOR 
                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoUpdateWebpage:
                    AutoUpdateWebpage.OnSceneLoaded(configGroup.Value, scene);
                    break;
#endif

            }
        }
    }

    public void GotoNextTestCase()
    {

        TestCaseIndex++;
        if (TestCaseIndex >= TestCaseQueue.Count())
        {
            TestCaseIndex = TestCaseQueue.Count()-1;
        };
        CurrentTestCase = TestCaseQueue[TestCaseIndex];
        AdroitProfiler_Logger.LogTestCaseInfo(CurrentTestCase);
        SetupConfigLists();

    }
    public void GotoPreviousTestCase()
    {

        TestCaseIndex--;
        if (TestCaseIndex < 0) {
            TestCaseIndex = 0;
        }
        CurrentTestCase = TestCaseQueue[TestCaseIndex];
        AdroitProfiler_Logger.LogTestCaseInfo(CurrentTestCase);
        SetupConfigLists();

    }

    private void Start()
    {
        CurrentTestCase = TestCaseQueue.FirstOrDefault();

        TestCaseIndex = 0;

        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        AdroitProfiler_Heartbeat = gameObject.GetComponent<AdroitProfiler_Heartbeat>();
        AutoBroadcaster = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoBroadcaster>();
        AutoChooseDialogChoice = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoChooseDialogChoice>();
        AutoClicker = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoClicker>();
        AutoClickTarget = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoClickTarget>();
        AutoLookAt = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoLookAt>();
        AutoMover = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoMover>();
        AutoMoveTo = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoMoveTo>();
        AutoTeleportTo = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoTeleportTo>();
        AutoChangeScene = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoChangeScene>();
        AutoChangeTestCase = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoChangeTestCase>();
        AutoPause = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoPause>();
        AutoTest = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoTest>();
#if UNITY_WEBGL && !UNITY_EDITOR 
        AutoUpdateWebpage = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoUpdateWebpage>();

#endif
        AdroitProfiler_Heartbeat.onTenth_Heartbeat_delegates.Add(OnTenthHeartbeat);
        AdroitProfiler_Heartbeat.onQuarter_Heartbeat_delegates.Add(OnQuarterHeartbeat);
        AdroitProfiler_Heartbeat.onHalf_Heartbeat_delegates.Add(OnHalfHeartbeat);
        AdroitProfiler_Heartbeat.on1s_Heartbeat_delegates.Add(On1SecondHeartbeat);
        AdroitProfiler_Heartbeat.on5s_Heartbeat_delegates.Add(On5SecondHeartbeat);
        AdroitProfiler_Heartbeat.on10s_Heartbeat_delegates.Add(On10SecondHeartbeat);
        GlobalTestCases.ForEach(x => AdroitProfiler_Logger.LogTestCaseInfo(x));
        SetupConfigLists();
    }

    private void SetupConfigLists()
    {
        var currentScene = SceneManager.GetActiveScene();
        var testCases = new List<AdroitProfiler_AutomatedTester_Configuration_TestCase>();
        AdroitProfiler_Logger.LogTestCaseInfo(CurrentTestCase);
        testCases.AddRange(GlobalTestCases);
        testCases.Add(CurrentTestCase);
        var nonNullTestCases = testCases.Where(x => x != null);
        var allConfigs = nonNullTestCases.SelectMany(x => x.ConfigurableActions);
        var allTests = nonNullTestCases.SelectMany(x => x.ConfigurableTests);
        var configs = allConfigs.Concat(allTests).ToList();

        OnTenthSecond_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene, AdroitProfiler_Timing.EveryTenthSecond)).ToList();
        OnQuarterSecond_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene, AdroitProfiler_Timing.EveryQuarterSecond)).ToList();
        OnHalfSecond_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene,  AdroitProfiler_Timing.EveryHalfSecond)).ToList();
        On1Second_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene, AdroitProfiler_Timing.EverySecond)).ToList();
        On5Seconds_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene, AdroitProfiler_Timing.Every5Seconds)).ToList();
        On10Seconds_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene,  AdroitProfiler_Timing.Every10Seconds)).ToList();
        AtTime_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene, AdroitProfiler_Timing.InvokeAtTime)).ToList();
        DuringTimespan_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene,  AdroitProfiler_Timing.InvokeDuringTimespan)).ToList();
    }

    private static bool ConfigListPredicate(AdroitProfiler_AutomatedTester_Configuration x, Scene currentScene, AdroitProfiler_Timing timing)
    {
        return x.Heartbeat_Timing == timing && (x.StartInEveryScene || x.StartInScene == currentScene.name);
    }

    private void Update()
    {
        if (AtTime_Configurations == null || DuringTimespan_Configurations == null) return;
        ProcessConfigurations(AtTime_Configurations.Where(x => x != null && x.Sent == false && x.InvokeAtTime < Time.timeSinceLevelLoad).ToList());
        ProcessConfigurations(DuringTimespan_Configurations.Where(x => x != null&&  x.StartTime < Time.timeSinceLevelLoad && x.EndTime > Time.timeSinceLevelLoad).ToList());
    }

    private void OnTenthHeartbeat()
    {
        ProcessConfigurations(OnTenthSecond_Configurations);
    }

    private void OnQuarterHeartbeat()
    {
        ProcessConfigurations(OnQuarterSecond_Configurations);

    }
    private void OnHalfHeartbeat()
    {
        ProcessConfigurations(OnHalfSecond_Configurations);
    }
    private void On1SecondHeartbeat()
    {
        ProcessConfigurations(On1Second_Configurations);
    }

    private void On5SecondHeartbeat()
    {
        ProcessConfigurations(On5Seconds_Configurations);
    }

    private void On10SecondHeartbeat()
    {
        ProcessConfigurations(On10Seconds_Configurations);
    }

    private void ProcessConfigurations(List<AdroitProfiler_AutomatedTester_Configuration> configs)
    {
        if (configs == null) return;
        foreach (var configuration in configs.Where(x=>x!=null))
        {
            ProcessConfiguration(configuration);
        }
    }

    private void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config) {

        switch (config.ConfigType)
        {
            case AdroitProfiler_AutomatedTester_Configuration_Type.Unselected:
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoLookAt:
                AutoLookAt.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoMover:
                AutoMover.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoMoveTo:
                AutoMoveTo.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoTeleportTo:
                AutoTeleportTo.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoClickTarget:
                AutoClickTarget.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoBroadcaster:
                AutoBroadcaster.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoChangeScene:
                AutoChangeScene.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoChangeTestCase:
                AutoChangeTestCase.ProcessConfiguration(config);

                break;
            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoChooseDialogChoice:
                AutoChooseDialogChoice.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoClicker:
                AutoClicker.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoPause:
                AutoPause.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoTest:
                AutoTest.ProcessConfiguration(config);
                break;
#if UNITY_WEBGL && !UNITY_EDITOR 
            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoUpdateWebpage:
                AutoUpdateWebpage.ProcessConfiguration(config);
                break;
#endif 


        }
    }
}





