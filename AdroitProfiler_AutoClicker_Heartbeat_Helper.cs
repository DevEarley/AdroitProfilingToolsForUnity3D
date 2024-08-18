using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//#if UNITY_EDITOR
[ExecuteInEditMode]
public class AdroitProfiler_AutoClicker_Heartbeat_Helper : MonoBehaviour
{
    //TODO make this a list!

    public TextMeshProUGUI TMProGUI_Helper_1;
    public TextMeshProUGUI TMProGUI_Helper_2;
    public TextMeshProUGUI TMProGUI_Helper_3;
    public TextMeshProUGUI TMProGUI_Helper_4;
    private AdroitProfiler_AutoClicker_Heartbeat AdroitProfiler_AutoClicker_Heartbeat;
    private void Start()
    {
        AdroitProfiler_AutoClicker_Heartbeat = gameObject.GetComponent<AdroitProfiler_AutoClicker_Heartbeat>();
    }
    private void Update()
    {
        if (TMProGUI_Helper_1 != null 
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations.Count> 0
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[0].Enabled
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[0].StartInScene == AdroitProfiler_AutoClicker_Heartbeat.CurrentSceneName)
        {
            var Vector = AdroitProfiler_AutoClicker_Heartbeat.GetPoint(AdroitProfiler_AutoClicker_Heartbeat.Configurations[0]);
            TMProGUI_Helper_1.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else
        {
            TMProGUI_Helper_1.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_2 != null 
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations.Count > 1 
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[1].Enabled
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[1].StartInScene == AdroitProfiler_AutoClicker_Heartbeat.CurrentSceneName)
        {
            var Vector = AdroitProfiler_AutoClicker_Heartbeat.GetPoint(AdroitProfiler_AutoClicker_Heartbeat.Configurations[1]);
            TMProGUI_Helper_2.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else
        {
            TMProGUI_Helper_2.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_3 != null 
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations.Count > 2 
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[2].Enabled
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[2].StartInScene == AdroitProfiler_AutoClicker_Heartbeat.CurrentSceneName)
        {
            var Vector = AdroitProfiler_AutoClicker_Heartbeat.GetPoint(AdroitProfiler_AutoClicker_Heartbeat.Configurations[2]);
            TMProGUI_Helper_3.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else
        {
            TMProGUI_Helper_3.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_4 != null 
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations.Count > 3 
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[3].Enabled
            && AdroitProfiler_AutoClicker_Heartbeat.Configurations[3].StartInScene == AdroitProfiler_AutoClicker_Heartbeat.CurrentSceneName)
        {
            var Vector = AdroitProfiler_AutoClicker_Heartbeat.GetPoint(AdroitProfiler_AutoClicker_Heartbeat.Configurations[3]);
            TMProGUI_Helper_4.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else
        {
            TMProGUI_Helper_4.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
//#endif

