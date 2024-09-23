using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AdroitProfiler_Timing
{
    Unselected = 0,
    Every10Seconds,
    Every5Seconds,
    EveryHalfSecond,
    EverySecond,
    EveryQuarterSecond,
    EveryTenthSecond,
    InvokeAtTime,
    InvokeDuringTimespan

}
public class AdroitProfiler_Heartbeat: MonoBehaviour
{
    [HideInInspector]
    public bool Paused = false;
    public bool StartPaused = false;

    [HideInInspector]
    public float TimeThisFrame = 0;

    [HideInInspector]
    public float TimerFor_TenthSecond = 0;
    [HideInInspector]
    public float TimerFor_QuarterSecond = 0;
    [HideInInspector]
    public float TimerFor_HalfSecond = 0;
    [HideInInspector]
    public float TimerFor_5Seconds = 0;
    [HideInInspector]
    public float TimerFor_10Seconds = 0;

    public delegate void OnTenth_Heartbeat();
    public List<OnTenth_Heartbeat> onTenth_Heartbeat_delegates = new List<OnTenth_Heartbeat>();

    public delegate void OnQuarter_Heartbeat();
    public List<OnQuarter_Heartbeat> onQuarter_Heartbeat_delegates = new List<OnQuarter_Heartbeat>();

    public delegate void OnHalf_Heartbeat();
    public List<OnHalf_Heartbeat> onHalf_Heartbeat_delegates = new List<OnHalf_Heartbeat>();

    public delegate void On5s_Heartbeat();
    public List<On5s_Heartbeat> on5s_Heartbeat_delegates = new List<On5s_Heartbeat>();

    public delegate void On10s_Heartbeat();
    public List<On10s_Heartbeat> on10s_Heartbeat_delegates = new List<On10s_Heartbeat>();

    public delegate void On1s_Heartbeat();
    public List<On1s_Heartbeat> on1s_Heartbeat_delegates = new List<On1s_Heartbeat>();

    private void Start()
    {
        Paused = StartPaused;
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Paused) return;
        TimerFor_TenthSecond += Time.unscaledDeltaTime;
        TimerFor_QuarterSecond += Time.unscaledDeltaTime;
        TimerFor_HalfSecond += Time.unscaledDeltaTime;
        TimerFor_5Seconds += Time.unscaledDeltaTime;
        TimerFor_10Seconds += Time.unscaledDeltaTime;
        TimeThisFrame = Time.unscaledDeltaTime * 1000.0f;
       
        CheckTimers();
    }


    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0.0f;
    }

    public void Unpause()
    {
        Paused = false;
        Time.timeScale = 1.0f;
    }



    private bool OneSecondSwitch = false;
    private bool TwoSecondSwitch = false;

    private void CheckTimers()
    {
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_TenthSecond, TimerFor_TenthSecond, AdroitProfiler_Service.MaxTimeForTimer_TenthSecond))
        {
            onTenth_Heartbeat_delegates.ForEach(x => x());
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_QuarterSecond, TimerFor_QuarterSecond, AdroitProfiler_Service.MaxTimeForTimer_QuarterSecond))
        {
            onQuarter_Heartbeat_delegates.ForEach(x => x());
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_HalfSecond, TimerFor_HalfSecond, AdroitProfiler_Service.MaxTimeForTimer_HalfSecond))
        {
            if (OneSecondSwitch)
            {
                OneSecondSwitch = false;
                on1s_Heartbeat_delegates.ForEach(x => x());
            }
            else
            {
                OneSecondSwitch = true;
            }
            onHalf_Heartbeat_delegates.ForEach(x => x());
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_5Seconds, TimerFor_5Seconds, AdroitProfiler_Service.MaxTimeForTimer_5Seconds))
        {
            on5s_Heartbeat_delegates.ForEach(x => x());
        }
        if (AdroitProfiler_Service.CheckTimer(out TimerFor_10Seconds, TimerFor_10Seconds, AdroitProfiler_Service.MaxTimeForTimer_10Seconds))
        {
            on10s_Heartbeat_delegates.ForEach(x => x());
        }
    }
}




