using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AdroitProfiler_UIBehaviour_State
{
    TimePerFrame,
    Summary,
    SystemMemory,
    PolyCount,
    DrawCalls,
    None
}

[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI TMProGUI_FPS;
    public TextMeshProUGUI TMProGUI_GPU;
    public TextMeshProUGUI TMProGUI_TenthSecMax;
    public TextMeshProUGUI TMProGUI_QuarterSecMax;
    public TextMeshProUGUI TMProGUI_HalfSecMax;
    public TextMeshProUGUI TMProGUI_5SecMax;
    public TextMeshProUGUI TMProGUI_10SecMax;

    private AdroitProfiler_UIBehaviour_State AdroitProfiler_UIBehaviour_State;
    private AdroitProfiler_State AdroitProfiler_State;

    private void Start()
    {
        AdroitProfiler_State = this.gameObject.GetComponent<AdroitProfiler_State>();
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
            case AdroitProfiler_UIBehaviour_State.SystemMemory:
                UpdateTimeAndCurrentFrame();
                Update_Metric_UIWithTime("Tenth Second", AdroitProfiler_State.SystemMemory_Metrics.MaxValueInLast_TenthSecond,  TMProGUI_TenthSecMax);
                Update_Metric_UIWithTime("Quarter Second", AdroitProfiler_State.SystemMemory_Metrics.MaxValueInLast_QuarterSecond,  TMProGUI_QuarterSecMax);
                Update_Metric_UIWithTime("Half Second", AdroitProfiler_State.SystemMemory_Metrics.MaxValueInLast_HalfSecond, TMProGUI_HalfSecMax);
                Update_Metric_UIWithTime("5 Seconds", AdroitProfiler_State.SystemMemory_Metrics.MaxValueInLast_5Seconds,  TMProGUI_5SecMax);
                Update_Metric_UIWithTime("10 Seconds", AdroitProfiler_State.SystemMemory_Metrics.MaxValueInLast_10Seconds,TMProGUI_10SecMax);
                break;
            case AdroitProfiler_UIBehaviour_State.TimePerFrame:
                UpdateTimeAndCurrentFrame();
                Update_TimePerFrame_UIWithTime("Tenth Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_TenthSecond, AdroitProfiler_State.AverageFPSFor_TenthSecond, TMProGUI_TenthSecMax);
                Update_TimePerFrame_UIWithTime("Quarter Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_QuarterSecond, AdroitProfiler_State.AverageFPSFor_QuarterSecond, TMProGUI_QuarterSecMax);
                Update_TimePerFrame_UIWithTime("Half Second", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_HalfSecond, AdroitProfiler_State.AverageFPSFor_HalfSecond, TMProGUI_HalfSecMax);
                Update_TimePerFrame_UIWithTime("5 Seconds", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_5Seconds, AdroitProfiler_State.AverageFPSFor_5Seconds, TMProGUI_5SecMax);
                Update_TimePerFrame_UIWithTime("10 Seconds", AdroitProfiler_State.TimePerFrame_Metrics.MaxValueInLast_10Seconds, AdroitProfiler_State.AverageFPSFor_10Seconds, TMProGUI_10SecMax);
                break;
            case AdroitProfiler_UIBehaviour_State.PolyCount:
                UpdateTimeAndCurrentFrame();
                Update_Metric_UIWithTime("Tenth Second", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_TenthSecond,  TMProGUI_TenthSecMax);
                Update_Metric_UIWithTime("Quarter Second", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_QuarterSecond, TMProGUI_QuarterSecMax);
                Update_Metric_UIWithTime("Half Second", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_HalfSecond,  TMProGUI_HalfSecMax);
                Update_Metric_UIWithTime("5 Seconds", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_5Seconds,TMProGUI_5SecMax);
                Update_Metric_UIWithTime("10 Seconds", AdroitProfiler_State.PolyCount_Metrics.MaxValueInLast_10Seconds,  TMProGUI_10SecMax);
                break;
            case AdroitProfiler_UIBehaviour_State.DrawCalls:
                UpdateTimeAndCurrentFrame();
                Update_Metric_UIWithTime("Tenth Second", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_TenthSecond, TMProGUI_TenthSecMax);
                Update_Metric_UIWithTime("Quarter Second", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_QuarterSecond, TMProGUI_QuarterSecMax);
                Update_Metric_UIWithTime("Half Second", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_HalfSecond,TMProGUI_HalfSecMax);
                Update_Metric_UIWithTime("5 Seconds", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_5Seconds, TMProGUI_5SecMax);
                Update_Metric_UIWithTime("10 Seconds", AdroitProfiler_State.DrawCalls_Metrics.MaxValueInLast_10Seconds, TMProGUI_10SecMax);
                break;
        }
    }

    public void UpdateState(AdroitProfiler_UIBehaviour_State _AdroitProfiler_UIBehaviour_State)
    {
        AdroitProfiler_UIBehaviour_State = _AdroitProfiler_UIBehaviour_State;
    }

    private void UpdateTimeAndCurrentFrame()
    {
        var text = "";
        text += "This Frame: " + AdroitProfiler_State.TimeThisFrame.ToString("000") + " ms \n\r";
        text += "Current Time: " + AdroitProfiler_Service.FormatTime(Time.time) + "\n\r";
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
