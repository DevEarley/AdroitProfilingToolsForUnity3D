
//#if UNITY_EDITOR
using System.Collections.Generic;
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
    private List<AdroitProfiler_AutomatedTester_Configuration> Configs;
    private void Start()
    {
        AdroitProfiler_AutomatedTester = gameObject.GetComponent<AdroitProfiler_AutomatedTester>();
        AdroitProfiler_AutomatedTester_AutoClicker = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoClicker>();
        if (AdroitProfiler_AutomatedTester.GlobalTestCases == null) return;
        if (AdroitProfiler_AutomatedTester.CurrentTestCase == null) return;
        GetConfigsFromTestCases();

    }

    private void GetConfigsFromTestCases()
    {
        var testCases = new List<AdroitProfiler_AutomatedTester_Configuration_TestCase>();
        testCases.AddRange(AdroitProfiler_AutomatedTester.GlobalTestCases);
        testCases.Add(AdroitProfiler_AutomatedTester.CurrentTestCase);
        var nonNullTestCases = testCases.Where(x => x != null);
        var allConfigs = nonNullTestCases.SelectMany(x => x.ConfigurableActions);
        var allTests = nonNullTestCases.SelectMany(x => x.ConfigurableTests);
        var allConfigsAndTests = allConfigs.Concat(allTests);
        Configs = allConfigsAndTests.Where(x => x != null && x.Enabled && x.ConfigType == AdroitProfiler_AutomatedTester_Configuration_Type.AutoClicker).ToList();
    }

    private void Update()
    {
        if(Configs == null)
        {
            GetConfigsFromTestCases();
        }
        if (TMProGUI_Helper_1 != null
            && Configs.Count > 0)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(Configs[0]);
            TMProGUI_Helper_1.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if(TMProGUI_Helper_1 != null)
        {
            TMProGUI_Helper_1.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_2 != null
            && Configs.Count > 1)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(Configs[1]);
            TMProGUI_Helper_2.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if (TMProGUI_Helper_2 != null)
        {
            TMProGUI_Helper_2.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_3 != null
            && Configs.Count > 2)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(Configs[2]);
            TMProGUI_Helper_3.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if (TMProGUI_Helper_3 != null)
        {
            TMProGUI_Helper_3.rectTransform.anchoredPosition = Vector2.zero;
        }
        if (TMProGUI_Helper_4 != null
            && Configs.Count > 3)
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(Configs[3]);
            TMProGUI_Helper_4.rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
        }
        else if (TMProGUI_Helper_4 != null)
        {
            TMProGUI_Helper_4.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
//#endif

