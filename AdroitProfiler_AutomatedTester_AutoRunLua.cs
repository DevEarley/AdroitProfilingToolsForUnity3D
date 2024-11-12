using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MHS
{
    public class AdroitProfiler_AutomatedTester_AutoRunLua : MonoBehaviour, AdroitProfiler_AutomatedTester_IAutomate
    {
        public void ProcessConfiguration(AdroitProfiler_AutomatedTester_Configuration config)
        {
            Debug.Log("Running Lua script: "+config.LuaScript);
            Lua.Run(config.LuaScript);
        }
    }
}
