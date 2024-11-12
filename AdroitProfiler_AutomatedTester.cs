using MHS;
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdroitProfiler_Logger))]
[RequireComponent(typeof(AdroitProfiler_Heartbeat))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoRunLua))]
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
    private List<AdroitProfiler_AutomatedTester_Configuration> AfterTime_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> DuringTimespan_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> OnSignal_Configurations;

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
    private AdroitProfiler_AutomatedTester_AutoRunLua AutoRunLua;

    private List<string> SignalsRecievedDuringThisScene = new List<string>();

#if UNITY_WEBGL && !UNITY_EDITOR 
    private AdroitProfiler_AutomatedTester_AutoUpdateWebpage AutoUpdateWebpage;
#endif

    private int TestCaseIndex;

    private List<AdroitProfiler_AutomatedTester_Configuration> SentConfigs = new List<AdroitProfiler_AutomatedTester_Configuration>();

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
        SentConfigs = new List<AdroitProfiler_AutomatedTester_Configuration>();
        SignalsRecievedDuringThisScene = new List<string>();
        SetupConfigLists();

        var testCases = new List<AdroitProfiler_AutomatedTester_Configuration_TestCase>();
        testCases.AddRange(GlobalTestCases);
        if (CurrentTestCase != null)
        {
         testCases.Add(CurrentTestCase);
        }
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
        if (TestCaseQueue == null || TestCaseQueue.Count() == 0) return;
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
        if (TestCaseQueue == null || TestCaseQueue.Count() == 0) return;

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
        AutoRunLua = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoRunLua>();
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
        AdroitProfiler_Heartbeat.onSignal_delegates.Add(ProcessConfigurations_OnSignal);
        GlobalTestCases.ForEach(x => AdroitProfiler_Logger.LogTestCaseInfo(x));
        SetupConfigLists();
    }

    private void SetupConfigLists()
    {
        var currentScene = SceneManager.GetActiveScene();
        var testCases = new List<AdroitProfiler_AutomatedTester_Configuration_TestCase>();
        testCases.AddRange(GlobalTestCases);
        if (CurrentTestCase != null)
        {

        AdroitProfiler_Logger.LogTestCaseInfo(CurrentTestCase);
        testCases.Add(CurrentTestCase);
        }
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
        AfterTime_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene, AdroitProfiler_Timing.StartAfterTime)).ToList();
        OnSignal_Configurations = configs.Where(x => ConfigListPredicateForSignal(x, currentScene)).ToList();
        DuringTimespan_Configurations = configs.Where(x => ConfigListPredicate(x, currentScene,  AdroitProfiler_Timing.InvokeDuringTimespan)).ToList();
    }

    private static bool ConfigListPredicate(AdroitProfiler_AutomatedTester_Configuration x, Scene currentScene, AdroitProfiler_Timing timing)
    {
        return x.Heartbeat_Timing == timing && (x.StartInEveryScene || x.StartInScene == currentScene.name);
    }

    private static bool ConfigListPredicateForSignal(AdroitProfiler_AutomatedTester_Configuration x, Scene currentScene)
    {
        return x.UsingSignal == true &&  (x.StartInEveryScene || x.StartInScene == currentScene.name);
    }
    private float TimeOfLastSignal = 0;
    private void Update()
    {
       // if (AtTime_Configurations == null || DuringTimespan_Configurations == null || AfterTime_Configurations == null) return;
    
        var AtTime_Configs_ThatNeedToBeSent = AtTime_Configurations.Where(x =>
        {
            var timeToConsider = (x.UsingSignal ? TimeOfLastSignal : Time.timeSinceLevelLoad);
            return x != null && SentConfigs.Contains(x) == false && x.InvokeAtTime < timeToConsider;
        }
        ).ToList();

        var DuringTime_Configs_ThatNeedToBeSent = DuringTimespan_Configurations.Where(x =>
        {
            var timeToConsider = (x.UsingSignal ? TimeOfLastSignal : Time.timeSinceLevelLoad);
            return x != null && x.StartTime < timeToConsider && x.EndTime > timeToConsider;
        }).ToList();

        var AfterTime_Configs_ThatNeedToBeSent
            = AfterTime_Configurations.Where(x =>
        {
            var timeToConsider = (x.UsingSignal ? TimeOfLastSignal : Time.timeSinceLevelLoad);
            return x != null && x.StartTime < timeToConsider ;
        }).ToList();



        ProcessConfigurations_ForHeartbeat(AtTime_Configs_ThatNeedToBeSent); 
        ProcessConfigurations_ForHeartbeat(DuringTime_Configs_ThatNeedToBeSent);
        ProcessConfigurations_ForHeartbeat(AfterTime_Configs_ThatNeedToBeSent);
    }

    private void ProcessConfigurations_OnSignal(string signalMessage)
    {
        Debug.Log("AdroitProfiler_AutomatedTester | ProcessConfigurations_OnSignal");
        TimeOfLastSignal = Time.timeSinceLevelLoad;
        SignalsRecievedDuringThisScene.Add(signalMessage);
        ProcessConfigurations_ForSignal(OnSignal_Configurations, signalMessage);
    }

    private void OnTenthHeartbeat()
    {
        ProcessConfigurations_ForHeartbeat(OnTenthSecond_Configurations);
    }

    private void OnQuarterHeartbeat()
    {
        ProcessConfigurations_ForHeartbeat(OnQuarterSecond_Configurations);

    }

    private void OnHalfHeartbeat()
    {
        ProcessConfigurations_ForHeartbeat(OnHalfSecond_Configurations);
    }

    private void On1SecondHeartbeat()
    {
        ProcessConfigurations_ForHeartbeat(On1Second_Configurations);
    }

    private void On5SecondHeartbeat()
    {
        ProcessConfigurations_ForHeartbeat(On5Seconds_Configurations);
    }

    private void On10SecondHeartbeat()
    {
        ProcessConfigurations_ForHeartbeat(On10Seconds_Configurations);
    }

    private void ProcessConfigurations_ForHeartbeat(List<AdroitProfiler_AutomatedTester_Configuration> configs)
    {
        if (configs == null) return;
        foreach (var config in configs.Where(x => x != null))
        {
            if(config.UsingSignal)
            {
                Debug.Log("ProcessConfigurations_ForHeartbeat | UsingSignal |" + config.name);
                if(SignalsRecievedDuringThisScene.Contains(config.StartingSignal) == true && (config.EndingSignal == "" || SignalsRecievedDuringThisScene.Contains(config.EndingSignal) == false))
                {
                    Debug.Log("ProcessConfigurations_ForHeartbeat | UsingSignal | IN");

                    ProcessConfiguration(config);
                }
            }
            else 
            {
                Debug.Log("ProcessConfigurations_ForHeartbeat | NOT UsingSignal");

                ProcessConfiguration(config);
            }
        }
    }

    private void ProcessConfigurations_ForSignal(List<AdroitProfiler_AutomatedTester_Configuration> configs, string signalMessage)
    {
        if (configs == null) return;
        foreach (var configuration in configs.Where(x => x != null && x.StartingSignal == signalMessage && x.Heartbeat_Timing == AdroitProfiler_Timing.InvokeOnSignal ))
        {
            ProcessConfiguration(configuration);
        }
    }

    private void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config) {
        if(config.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime)
        { 
            SentConfigs.Add(config);
        }
        Debug.Log("ProcessConfiguration | ConfigType |" + config.ConfigType);
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

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoRunLua:
                AutoRunLua.ProcessConfiguration(config);
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





