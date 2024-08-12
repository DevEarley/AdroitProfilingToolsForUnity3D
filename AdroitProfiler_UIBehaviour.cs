using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI TMProGUI_FPS;
    public TextMeshProUGUI TMProGUI_TenthSecMax;
    public TextMeshProUGUI TMProGUI_QuarterSecMax;
    public TextMeshProUGUI TMProGUI_HalfSecMax;
    public TextMeshProUGUI TMProGUI_5SecMax;
    public TextMeshProUGUI TMProGUI_10SecMax;

    private AdroitProfiler_State AdroitProfiler_State;

    private void Start()
    {
        AdroitProfiler_State = this.gameObject.GetComponent<AdroitProfiler_State>();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void LateUpdate()
    {
         UpdateFPSText();
         UpdateUIWithTime("Tenth Second", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, TMProGUI_HalfSecMax);
         UpdateUIWithTime("Quarter Second", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, TMProGUI_HalfSecMax);
         UpdateUIWithTime("Half Second", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, TMProGUI_HalfSecMax);
         UpdateUIWithTime("5 Seconds", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, TMProGUI_5SecMax);
         UpdateUIWithTime("10 Seconds", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, TMProGUI_10SecMax);
    }

    private void UpdateFPSText()
    {
        // var text = AdroitProfiler_State.PreviousAverage.ToString("000") + " FPS \n\r";
        var text = "";
        text += "This Frame: " + AdroitProfiler_State.TimeThisFrame.ToString("000") + " ms \n\r";
        text += "Current Time: " + AdroitProfiler_Service.FormatTime(Time.time) + "\n\r";
        TMProGUI_FPS.text = text;
    }

    private void UpdateUIWithTime(string name, float time,  TextMeshProUGUI TMProGUI_label)
    {
        TMProGUI_label.text = name + ": " + time.ToString("000") + " ms ";
    }
}
