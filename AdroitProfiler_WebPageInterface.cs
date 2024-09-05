#if UNITY_WEBGL
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public  class AdroitProfiler_WebPageInterface :MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void logtopage(string[] testCases);

    public  void UpdateWebPage( string[] runs)
    {
        logtopage(runs);
    }
}
#endif