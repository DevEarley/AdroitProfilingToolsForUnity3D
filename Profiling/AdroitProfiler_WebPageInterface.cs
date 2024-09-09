#if UNITY_WEBGL
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
[RequireComponent(typeof(AdroitProfiler_Logger))]

public class AdroitProfiler_WebPageInterface :MonoBehaviour
{
    private AdroitProfiler_Logger Logger;
    private void Start()
    {
        Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
    }

    [DllImport("__Internal")]
    private static extern void logtopage(string testCases);

    public  void UpdateWebPage()
    {
        string jsonLogData = "";
        foreach (var run in Logger.Runs)
        {
            jsonLogData += AdroitProfiler_Logger.ProfileHeader;
            run.Value.ForEach(performanceEventLog =>
            {
                jsonLogData += performanceEventLog;
            });
        };
        logtopage(jsonLogData);
    }
}
#endif