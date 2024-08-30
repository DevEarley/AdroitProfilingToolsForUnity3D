
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class AdroitProfiler_AutomatedTester : MonoBehaviour
{
    public AdroitProfiler_AutomatedTester_Configuration_TestCase TestCase;

    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;
    private List<AdroitProfiler_AutomatedTester_Configuration> OnTenthSecond_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> OnQuarterSecond_Configurations;
    private List<AdroitProfiler_AutomatedTester_Configuration> OnHalfSecond_Configurations;
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
    private AdroitProfiler_AutomatedTester_AutoChangeScene AutoChangeScene;


    [HideInInspector]
    public string CurrentSceneName = "";

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
        CurrentSceneName = scene.path;
      
        Debug.Log("scene.path : " + scene.path);

        var dictionaryOfConfigsGroupedByTypeForThisScene = TestCase.Configs
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

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoMover:
                    AutoMover.OnSceneLoaded(configGroup.Value, scene);
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

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoBroadcaster:
                    AutoBroadcaster.OnSceneLoaded(configGroup.Value, scene);
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoChangeScene:
                    AutoChangeScene.OnSceneLoaded(configGroup.Value, scene);

                    break;
                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoChooseDialogChoice:
                    AutoChooseDialogChoice.OnSceneLoaded(configGroup.Value, scene);
                    break;

                case AdroitProfiler_AutomatedTester_Configuration_Type.AutoClicker:
                    AutoClicker.OnSceneLoaded(configGroup.Value, scene);
                    break;
            }
        }
    }



    private void Start()
    {
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

        AdroitProfiler_Heartbeat.onTenth_Heartbeat_delegates.Add(OnTenthHeartbeat);
        AdroitProfiler_Heartbeat.onQuarter_Heartbeat_delegates.Add(OnQuarterHeartbeat);
        AdroitProfiler_Heartbeat.onHalf_Heartbeat_delegates.Add(OnHalfHeartbeat);
        AdroitProfiler_Heartbeat.on5s_Heartbeat_delegates.Add(On5SecondHeartbeat);
        AdroitProfiler_Heartbeat.on10s_Heartbeat_delegates.Add(On10SecondHeartbeat);

        OnTenthSecond_Configurations = TestCase.Configs.Where(x => x.Heartbeat_Timing == AdroitProfiler_Timing.EveryTenthSecond).ToList();
        OnQuarterSecond_Configurations = TestCase.Configs.Where(x => x.Heartbeat_Timing == AdroitProfiler_Timing.EveryQuarterSecond).ToList();
        OnHalfSecond_Configurations = TestCase.Configs.Where(x => x.Heartbeat_Timing == AdroitProfiler_Timing.EveryHalfSecond).ToList();
        On5Seconds_Configurations = TestCase.Configs.Where(x => x.Heartbeat_Timing == AdroitProfiler_Timing.Every5Seconds).ToList();
        On10Seconds_Configurations = TestCase.Configs.Where(x => x.Heartbeat_Timing == AdroitProfiler_Timing.Every10Seconds).ToList();
        AtTime_Configurations = TestCase.Configs.Where(x =>  x.Heartbeat_Timing == AdroitProfiler_Timing.InvokeAtTime).ToList();
        DuringTimespan_Configurations = TestCase.Configs.Where(x => x.Heartbeat_Timing == AdroitProfiler_Timing.InvokeDuringTimespan).ToList();
    }
    private void Update()
    {
        ProcessConfigurations(AtTime_Configurations.Where(x=>x.Sent == false && x.InvokeAtTime < Time.timeSinceLevelLoad).ToList());
        ProcessConfigurations(DuringTimespan_Configurations.Where(x => x.StartTime < Time.timeSinceLevelLoad && x.EndTime > Time.timeSinceLevelLoad).ToList());


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
            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoChooseDialogChoice:
                AutoChooseDialogChoice.ProcessConfiguration(config);
                break;

            case AdroitProfiler_AutomatedTester_Configuration_Type.AutoClicker:
                AutoClicker.ProcessConfiguration(config);
                break;
        }
    }
}





