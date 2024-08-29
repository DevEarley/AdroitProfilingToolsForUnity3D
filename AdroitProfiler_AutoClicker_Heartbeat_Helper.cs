
//#if UNITY_EDITOR
using System.Linq;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(AdroitProfiler_AutomatedTester))]
[RequireComponent(typeof(AdroitProfiler_AutomatedTester_AutoClicker))]

[ExecuteInEditMode]
public class AdroitProfiler_AutoClicker_Heartbeat_Helper : MonoBehaviour
{
    //TODO make this a list!

    public TextMeshProUGUI TMProGUI_Helper_1;
    public TextMeshProUGUI TMProGUI_Helper_2;
    public TextMeshProUGUI TMProGUI_Helper_3;
    public TextMeshProUGUI TMProGUI_Helper_4;
    private AdroitProfiler_AutomatedTester AdroitProfiler_AutomatedTester;
    private AdroitProfiler_AutomatedTester_AutoClicker AdroitProfiler_AutomatedTester_AutoClicker;
    private void Start()
    {
        AdroitProfiler_AutomatedTester = gameObject.GetComponent<AdroitProfiler_AutomatedTester>();
        AdroitProfiler_AutomatedTester_AutoClicker = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoClicker>();
    }
    private void Update()
    {
        var configs = AdroitProfiler_AutomatedTester.TestCase.Configs.Where(x => x.Enabled && x.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoClicker).ToList();
        if (TMProGUI_Helper_1 != null
            && configs.Count > 0)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(configs[0]);
            TMProGUI_Helper_1.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if(TMProGUI_Helper_1 != null)
        {
            TMProGUI_Helper_1.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_2 != null
            && configs.Count > 1)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(configs[1]);
            TMProGUI_Helper_2.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if (TMProGUI_Helper_2 != null)
        {
            TMProGUI_Helper_2.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_3 != null
            && configs.Count > 2)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(configs[2]);
            TMProGUI_Helper_3.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if (TMProGUI_Helper_3 != null)
        {
            TMProGUI_Helper_3.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_4 != null
            && configs.Count > 3)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(configs[3]);
            TMProGUI_Helper_4.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if (TMProGUI_Helper_4 != null)
        {
            TMProGUI_Helper_4.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
//#endif

