using UnityEngine;
using UnityEngine.SceneManagement;

public class AdroitProfiler_State : MonoBehaviour
{
    [HideInInspector]
    public float TimeThisFrame = 0;
    [HideInInspector]
    public float MaxInThis_TenthSecond_TimePerFrame = 0;
    [HideInInspector]
    public float MaxInThis_QuarterSecond_TimePerFrame = 0;
    [HideInInspector]
    public float MaxInThis_HalfSecond_TimePerFrame = 0;
    [HideInInspector]
    public float MaxInThis_5Seconds_TimePerFrame = 0;
    [HideInInspector]
    public float MaxInThis_10Seconds_TimePerFrame = 0;
    [HideInInspector]
    public float TimerFor_TenthSecond_TimePerFrame = 0;
    [HideInInspector]
    public float TimerFor_QuarterSecond_TimePerFrame = 0;
    [HideInInspector]
    public float TimerFor_HalfSecond_TimePerFrame = 0;
    [HideInInspector]
    public float TimerFor_5Seconds_TimePerFrame = 0;
    [HideInInspector]
    public float TimerFor_10Seconds_TimePerFrame = 0;
    [HideInInspector]
    public int AverageFPSFor_TenthSecond = 0;
    [HideInInspector]
    public int AverageFPSFor_QuarterSecond = 0;
    [HideInInspector]
    public int AverageFPSFor_HalfSecond = 0;
    [HideInInspector]
    public int AverageFPSFor_5Seconds = 0;
    [HideInInspector]
    public int AverageFPSFor_10Seconds = 0;

    private float TotalTimeFor_TenthSecond = 0;
    private float TotalTimeFor_QuarterSecond = 0;
    private float TotalTimeFor_HalfSecond = 0;
    private float TotalTimeFor_5Seconds = 0;
    private float TotalTimeFor_10Seconds = 0;

    private int NumberOfFramesThis_TenthSecond = 0;
    private int NumberOfFramesThis_QuarterSecond = 0;
    private int NumberOfFramesThis_HalfSecond = 0;
    private int NumberOfFramesThis_5Seconds = 0;
    private int NumberOfFramesThis_10Seconds = 0;

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
        //RESET STATE
    }
    private void Update()
    {
        TimerFor_TenthSecond_TimePerFrame += Time.deltaTime;
        TimerFor_QuarterSecond_TimePerFrame += Time.deltaTime;
        TimerFor_HalfSecond_TimePerFrame += Time.deltaTime;
        TimerFor_5Seconds_TimePerFrame += Time.deltaTime;
        TimerFor_10Seconds_TimePerFrame += Time.deltaTime;
        TimeThisFrame = Time.deltaTime * 1000.0f;
        UpdateMetrics();
        UpdateAverages();
    }

    private void UpdateAverages()
    {
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_TenthSecond,
            TotalTimeFor_TenthSecond,
            out NumberOfFramesThis_TenthSecond,
            NumberOfFramesThis_TenthSecond,
            out AverageFPSFor_TenthSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_QuarterSecond,
            TotalTimeFor_QuarterSecond,
            out NumberOfFramesThis_QuarterSecond,
            NumberOfFramesThis_QuarterSecond,
            out AverageFPSFor_QuarterSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_HalfSecond,
            TotalTimeFor_HalfSecond,
            out NumberOfFramesThis_HalfSecond,
            NumberOfFramesThis_HalfSecond,
            out AverageFPSFor_HalfSecond);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_5Seconds,
            TotalTimeFor_5Seconds,
            out NumberOfFramesThis_5Seconds,
            NumberOfFramesThis_5Seconds,
            out AverageFPSFor_5Seconds);
        AdroitProfiler_Service.UpdateFPSForTimespan(
            TimeThisFrame,
            out TotalTimeFor_10Seconds,
            TotalTimeFor_10Seconds,
            out NumberOfFramesThis_10Seconds,
            NumberOfFramesThis_10Seconds,
            out AverageFPSFor_10Seconds);
    }

    private void UpdateMetrics()
    {

        AdroitProfiler_Service.UpdateMetric(
            out TimerFor_TenthSecond_TimePerFrame,
            out MaxInThis_TenthSecond_TimePerFrame,
            TimerFor_TenthSecond_TimePerFrame,
            MaxInThis_TenthSecond_TimePerFrame,
            TimeThisFrame,
            AdroitProfiler_Service.MaxTimeForTimer_TenthSecond_TimePerFrame);


        AdroitProfiler_Service.UpdateMetric(
            out TimerFor_QuarterSecond_TimePerFrame,
            out MaxInThis_QuarterSecond_TimePerFrame,
            TimerFor_QuarterSecond_TimePerFrame,
            MaxInThis_QuarterSecond_TimePerFrame,
            TimeThisFrame,
            AdroitProfiler_Service.MaxTimeForTimer_QuarterSecond_TimePerFrame);


        AdroitProfiler_Service.UpdateMetric(
            out TimerFor_HalfSecond_TimePerFrame,
            out MaxInThis_HalfSecond_TimePerFrame,
            TimerFor_HalfSecond_TimePerFrame,
            MaxInThis_HalfSecond_TimePerFrame,
            TimeThisFrame,
            AdroitProfiler_Service.MaxTimeForTimer_HalfSecond_TimePerFrame);

        AdroitProfiler_Service.UpdateMetric(
            out TimerFor_5Seconds_TimePerFrame,
            out MaxInThis_5Seconds_TimePerFrame,
            TimerFor_5Seconds_TimePerFrame,
            MaxInThis_5Seconds_TimePerFrame,
            TimeThisFrame,
            AdroitProfiler_Service.MaxTimeForTimer_5Seconds_TimePerFrame);

        AdroitProfiler_Service.UpdateMetric(
            out TimerFor_10Seconds_TimePerFrame,
            out MaxInThis_10Seconds_TimePerFrame,
            TimerFor_10Seconds_TimePerFrame,
            MaxInThis_10Seconds_TimePerFrame,
            TimeThisFrame,
            AdroitProfiler_Service.MaxTimeForTimer_10Seconds_TimePerFrame);

    }


}
