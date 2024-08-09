using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(AdroitProfiler))]
public class AdroitProfiler_UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI TMProGUI_FPS;
    public TextMeshProUGUI TMProGUI_HalfSecMax;
    public TextMeshProUGUI TMProGUI_5SecMax;
    public TextMeshProUGUI TMProGUI_10SecMax;

    private AdroitProfiler AdroitProfiler;

    private void Start()
    {
        AdroitProfiler = this.gameObject.GetComponent<AdroitProfiler>();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void LateUpdate()
    {
        TMProGUI_FPS.text =  UpdateText();
        TMProGUI_HalfSecMax.text = UpdateHalfSecondText();
        TMProGUI_5SecMax.text = Update5SecondsText();
        TMProGUI_10SecMax.text = Update10SecondsText();
    }

    private string UpdateText()
    {
        var text = AdroitProfiler.PreviousAverage.ToString("000") + " FPS \n\r";
        text += "This Frame: " + AdroitProfiler.TimeThisFrame.ToString("000") + " ms \n\r";
        text += "Current Time: " + AdroitProfiler_Service.FormatTime(Time.time) + "\n\r";
        return text;
    }

    private string UpdateHalfSecondText()
    {
        return "Half Second: " + AdroitProfiler.MaxInThis_HalfSecond_TimePerFrame.ToString("000") + " ms ";
    }

    private string Update5SecondsText()
    {
        return "5 Seconds: " + AdroitProfiler.MaxInThis_5Seconds_TimePerFrame.ToString("000") + " ms";
    }

    private string Update10SecondsText()
    {
        return "10 Seconds: " + AdroitProfiler.MaxInThis_10Seconds_TimePerFrame.ToString("000") + " ms";
    }
}
