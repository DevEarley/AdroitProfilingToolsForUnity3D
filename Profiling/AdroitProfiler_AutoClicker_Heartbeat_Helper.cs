
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
    public List<TextMeshProUGUI> Helpers = new List<TextMeshProUGUI>();

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
        if (Configs == null)
        {
            GetConfigsFromTestCases();
        }
        var helperIndex = 0;
        foreach (var config in Configs.Take(Helpers.Count))
        {
            var Vector = AdroitProfiler_AutomatedTester_AutoClicker.GetPoint(config);
            Helpers[helperIndex].rectTransform.anchoredPosition = new Vector2(Vector.x, Vector.y);
            helperIndex++;
        }
        var moreHelpersThanConfigs = Helpers.Count - Configs.Count;
        if (moreHelpersThanConfigs > 0)
        {
            foreach (var helper in Helpers.TakeLast(moreHelpersThanConfigs))
            {
                if (helper != null
                && Configs.Count > 0)
                {
                    helper.rectTransform.anchoredPosition = Vector2.zero;
                }
            }
        }
    }
}
//#endif

