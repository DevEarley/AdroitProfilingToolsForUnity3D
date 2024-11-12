using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AdroitProfiler_UIBehaviour_State
{
    TimePerFrame,
    Summary,
   // SystemMemory,
    PolyCount,
    DrawCalls,
    None
}

[RequireComponent(typeof(AdroitProfiler_State))]
[RequireComponent(typeof(AdroitProfiler_Heartbeat))]
public class AdroitProfiler_UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI TMProGUI_TestCaseName;
    public TextMeshProUGUI TMProGUI_FPS;
    public TextMeshProUGUI TMProGUI_GPU;
    public TextMeshProUGUI TMProGUI_TenthSecMax;
    public TextMeshProUGUI TMProGUI_QuarterSecMax;
    public TextMeshProUGUI TMProGUI_HalfSecMax;
    public TextMeshProUGUI TMProGUI_5SecMax;
    public TextMeshProUGUI TMProGUI_10SecMax;

    private bool ShowingUI = false;

    private AdroitProfiler_UIBehaviour_State AdroitProfiler_UIBehaviour_State;
    private AdroitProfiler_AutomatedTester AdroitProfiler_AutomatedTester;
    private AdroitProfiler_State AdroitProfiler_State;
    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;

    private void Start()
    {
        AdroitProfiler_State = this.gameObject.GetComponent<AdroitProfiler_State>();
        AdroitProfiler_Heartbeat = this.gameObject.GetComponent<AdroitProfiler_Heartbeat>();
        AdroitProfiler_AutomatedTester = this.gameObject.GetComponent<AdroitProfiler_AutomatedTester>();
        HideUI();
    }
    public void ToggleUI()
    {
        if (ShowingUI == true)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }

    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //RESET GO LIST
    }
    void LateUpdate()
    {
        if (AdroitProfiler_AutomatedTester != null && AdroitProfiler_AutomatedTester.CurrentTestCase != null)
        {
            TMProGUI_TestCaseName.text = AdroitProfiler_AutomatedTester.CurrentTestCase.name;
        }
        if (AdroitProfiler_Heartbeat.Paused == true && ShowingUI == true)
        {
            HideUI();

        }
        else if (AdroitProfiler_Heartbeat.Paused == false && ShowingUI == false)
        {
            ShowUI();

        }
        if (AdroitProfiler_Heartbeat.Paused == true)
        {
            return;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        var MBD = (1024) * (1024);
        TMProGUI_GPU.text = AdroitProfiler_State.GPUStats;
        switch (AdroitProfiler_UIBehaviour_State)
        {
            case AdroitProfiler_UIBehaviour_State.Summary:
                UpdateTimeAndCurrentFrame();
                Update_TimePerFrame_UIWithTime("Tenth Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_TenthSecond, AdroitProfiler_State.AverageFPSFor_TenthSecond, TMProGUI_TenthSecMax);
                Update_TimePerFrame_UIWithTime("Quarter Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_QuarterSecond, AdroitProfiler_State.AverageFPSFor_QuarterSecond, TMProGUI_QuarterSecMax);
                Update_TimePerFrame_UIWithTime("Half Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_HalfSecond, AdroitProfiler_State.AverageFPSFor_HalfSecond, TMProGUI_HalfSecMax);
                Update_TimePerFrame_UIWithTime("5 Seconds", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_5Seconds, AdroitProfiler_State.AverageFPSFor_5Seconds, TMProGUI_5SecMax);
                Update_TimePerFrame_UIWithTime("10 Seconds", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_10Seconds, AdroitProfiler_State.AverageFPSFor_10Seconds, TMProGUI_10SecMax);
                break;
            //case AdroitProfiler_UIBehaviour_State.SystemMemory:
            //    UpdateTimeAndCurrentFrame();
            //    Update_Metric_UIWithTime("Tenth Second", AdroitProfiler_Heartbeat.SystemMemory_Metrics.MaxValueInLast_TenthSecond / MBD,  TMProGUI_TenthSecMax);
            //    Update_Metric_UIWithTime("Quarter Second", AdroitProfiler_Heartbeat.SystemMemory_Metrics.MaxValueInLast_QuarterSecond/ MBD,  TMProGUI_QuarterSecMax);
            //    Update_Metric_UIWithTime("Half Second", AdroitProfiler_Heartbeat.SystemMemory_Metrics.MaxValueInLast_HalfSecond/ MBD, TMProGUI_HalfSecMax);
            //    Update_Metric_UIWithTime("5 Seconds", AdroitProfiler_Heartbeat.SystemMemory_Metrics.MaxValueInLast_5Seconds/ MBD,  TMProGUI_5SecMax);
            //    Update_Metric_UIWithTime("10 Seconds", AdroitProfiler_Heartbeat.SystemMemory_Metrics.MaxValueInLast_10Seconds/ MBD, TMProGUI_10SecMax);
            //    break;
            case AdroitProfiler_UIBehaviour_State.TimePerFrame:
                UpdateTimeAndCurrentFrame();
                Update_TimePerFrame_UIWithTime("Tenth Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_TenthSecond, AdroitProfiler_State.AverageFPSFor_TenthSecond, TMProGUI_TenthSecMax);
                Update_TimePerFrame_UIWithTime("Quarter Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_QuarterSecond, AdroitProfiler_State.AverageFPSFor_QuarterSecond, TMProGUI_QuarterSecMax);
                Update_TimePerFrame_UIWithTime("Half Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_HalfSecond, AdroitProfiler_State.AverageFPSFor_HalfSecond, TMProGUI_HalfSecMax);
                Update_TimePerFrame_UIWithTime("5 Seconds", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_5Seconds, AdroitProfiler_State.AverageFPSFor_5Seconds, TMProGUI_5SecMax);
                Update_TimePerFrame_UIWithTime("10 Seconds", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_10Seconds, AdroitProfiler_State.AverageFPSFor_10Seconds, TMProGUI_10SecMax);
                break;
            //case AdroitProfiler_UIBehaviour_State.PolyCount:
            //    UpdateTimeAndCurrentFrame();
            //    Update_Metric_UIWithTime("Tenth Second", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_TenthSecond, TMProGUI_TenthSecMax);
            //    Update_Metric_UIWithTime("Quarter Second", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_QuarterSecond, TMProGUI_QuarterSecMax);
            //    Update_Metric_UIWithTime("Half Second", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_HalfSecond, TMProGUI_HalfSecMax);
            //    Update_Metric_UIWithTime("5 Seconds", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_5Seconds, TMProGUI_5SecMax);
            //    Update_Metric_UIWithTime("10 Seconds", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_10Seconds, TMProGUI_10SecMax);
            //    break;
            //case AdroitProfiler_UIBehaviour_State.DrawCalls:
            //    UpdateTimeAndCurrentFrame();
            //    Update_Metric_UIWithTime("Tenth Second", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_TenthSecond, TMProGUI_TenthSecMax);
            //    Update_Metric_UIWithTime("Quarter Second", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_QuarterSecond, TMProGUI_QuarterSecMax);
            //    Update_Metric_UIWithTime("Half Second", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_HalfSecond, TMProGUI_HalfSecMax);
            //    Update_Metric_UIWithTime("5 Seconds", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_5Seconds, TMProGUI_5SecMax);
            //    Update_Metric_UIWithTime("10 Seconds", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_10Seconds, TMProGUI_10SecMax);
            //    break;
        }
    }

    private void ShowUI()
    {
        ShowingUI = true;
        TMProGUI_FPS.gameObject.SetActive(true);
        TMProGUI_GPU.gameObject.SetActive(true);
        TMProGUI_TenthSecMax.gameObject.SetActive(true);
        TMProGUI_QuarterSecMax.gameObject.SetActive(true);
        TMProGUI_HalfSecMax.gameObject.SetActive(true);
        TMProGUI_5SecMax.gameObject.SetActive(true);
        TMProGUI_10SecMax.gameObject.SetActive(true);
    }

    private void HideUI()
    {
        ShowingUI = false;
        TMProGUI_FPS.gameObject.SetActive(false);
        TMProGUI_GPU.gameObject.SetActive(false);
        TMProGUI_TenthSecMax.gameObject.SetActive(false);
        TMProGUI_QuarterSecMax.gameObject.SetActive(false);
        TMProGUI_HalfSecMax.gameObject.SetActive(false);
        TMProGUI_5SecMax.gameObject.SetActive(false);
        TMProGUI_10SecMax.gameObject.SetActive(false);
    }

    public void UpdateState(AdroitProfiler_UIBehaviour_State _AdroitProfiler_UIBehaviour_State)
    {
        AdroitProfiler_UIBehaviour_State = _AdroitProfiler_UIBehaviour_State;
    }

    private void UpdateTimeAndCurrentFrame()
    {
        var text = "";
        text += "This Frame: " + AdroitProfiler_Heartbeat.TimeThisFrame.ToString("000") + " ms \n\r";
        text += "Current Time: "+ Mathf.FloorToInt(Time.timeSinceLevelLoad) + "s (" + AdroitProfiler_Service.FormatTime(Time.timeSinceLevelLoad) + ")\n\r";
        TMProGUI_FPS.text = text;
    }

    private void Update_TimePerFrame_UIWithTime(string name, float time, int FPS, TextMeshProUGUI TMProGUI_label)
    {
        var text = "";
        text += name + ": " + time.ToString("000") + " ms | " + FPS.ToString("000") + "FPS";
        TMProGUI_label.text = text;
    }
    private void Update_Metric_UIWithTime(string name, float value, TextMeshProUGUI TMProGUI_label)
    {
        var text = "";
        text += name + ": " + value ;
        TMProGUI_label.text = text;
    }
}
