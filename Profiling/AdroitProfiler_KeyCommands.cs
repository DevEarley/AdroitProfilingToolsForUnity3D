using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_UIBehaviour))]
[RequireComponent(typeof(AdroitProfiler_State))]
[RequireComponent(typeof(AdroitProfiler_Logger))]
[RequireComponent(typeof(AdroitProfiler_GameObjectController))]

#if UNITY_WEBGL

[RequireComponent(typeof(AdroitProfiler_WebPageInterface))]

#endif

public class AdroitProfiler_KeyCommands : MonoBehaviour
{
    public TextMeshProUGUI Instructions;
    public GameObject Instructions_Short;
    private AdroitProfiler_UIBehaviour AdroitProfiler_UIBehaviour;
    private AdroitProfiler_Logger AdroitProfiler_Logger;
    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;
    private AdroitProfiler_AutomatedTester AdroitProfiler_AutomatedTester;
    private AdroitProfiler_GameObjectController AdroitProfiler_GameObjectController;
    private AdroitProfiler_AutomatedTester_AutoClicker AutoClicker;
    private string LeftBracketInstructions = "Commands: [, ]\r\n" + "" +
            "Set slots: [ + 1,2,3 ... 8\r\n" +
            "Confirm game object: Enter\r\n" +
            "Turn on all slots: [ + 9\r\n" +
            "Turn off all slots: [ + 0\r\n" +
            "Capture Metric: [ + C\r\n" +
            "Clear Duplicates: [ + X\r\n" +
            "Save Logs: [ + L\r\n" +
            "Show Time Per Frame Metrics: [ + F\r\n" +
            "Show Draw Metrics: [ + R\r\n" +
            "Show Poly Count Metrics: [ + Y\r\n" +
            "Toggle UI: [ + U\r\n" +
            "Go to previous test case: [ + P\r\n" +
            "Go to next test case: [ + N\r\n" +
            "Toggle Profiler: [ + Q\r\n";

    private string RightBracketInstructions = "Commands: [, ]\r\n" + "" +
            "Toggle slots: ] + 1,2,3 ... 8\r\n"+
                "Toggle Auto Clickers: ] + C";


    private string BothBracketInstructions = "Commands: [, ]\r\n" + "" +
            "Duplicate GO at Slot: [ + ] + 1,2,3 ... 8\r\n";

    private void Start()
    {
        AdroitProfiler_Heartbeat = gameObject.GetComponent<AdroitProfiler_Heartbeat>();
        AdroitProfiler_AutomatedTester = gameObject.GetComponent<AdroitProfiler_AutomatedTester>();
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        AdroitProfiler_UIBehaviour = gameObject.GetComponent<AdroitProfiler_UIBehaviour>();
        AdroitProfiler_GameObjectController = gameObject.GetComponent<AdroitProfiler_GameObjectController>();
        AutoClicker = gameObject.GetComponent<AdroitProfiler_AutomatedTester_AutoClicker>();
        Instructions.text = LeftBracketInstructions;
    }

    void Update()
    {
        var pressingLeftBracket = Input.GetKey(KeyCode.LeftBracket);
        var pressingBackslash = Input.GetKey(KeyCode.Backslash);
        var pressingRightBracket = Input.GetKey(KeyCode.RightBracket);

        if (pressingLeftBracket && pressingRightBracket == false)
        {
            Instructions.text = LeftBracketInstructions;
            Instructions.gameObject.SetActive(true);
            Instructions_Short.SetActive(false);
        }
        else if (pressingRightBracket && pressingLeftBracket == false)
        {
            Instructions.text = RightBracketInstructions;
            Instructions.gameObject.SetActive(true);
            Instructions_Short.SetActive(false);
        }
        else if (pressingLeftBracket == true && pressingRightBracket == true)
        {
            Instructions.text = BothBracketInstructions;
            Instructions.gameObject.SetActive(true);
            Instructions_Short.SetActive(false);
        }
        else if (pressingLeftBracket == false && pressingRightBracket == false)
        {
            Instructions.gameObject.SetActive(false);
            Instructions_Short.SetActive(true);
        }

        if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Q))
        {
            AdroitProfiler_Heartbeat.Paused = !AdroitProfiler_Heartbeat.Paused;
            return;
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            AdroitProfiler_GameObjectController.ConfirmSlot();
        }
        //Left & Right Bracket
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha1))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(0);
        }
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(1);
        }
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha3))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(2);
        }
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha4))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(3);
        }
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha5))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(4);
        }
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha6))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(5);
        }
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha7))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(6);
        }
        else if (pressingRightBracket && pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha8))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(7);
        }
        //Only Right Bracket
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha1))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(0);
        }
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(1);
        }
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha3))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(2);
        }
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha4))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(3);
        }
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha5))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(4);
        }
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha6))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(5);
        }
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha7))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(6);
        }
        else if (pressingRightBracket && Input.GetKeyDown(KeyCode.Alpha8))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(7);
        }
        //Only Left bracket
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.L))
        {
            AdroitProfiler_Logger.SaveLogs();
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.U))
        {
            AdroitProfiler_UIBehaviour.ToggleUI();
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.X))
        {
            AdroitProfiler_GameObjectController.ClearDuplicates();
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.C))
        {
            AdroitProfiler_Logger.CapturePerformanceForEvent("Manual Capture");
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.F))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.TimePerFrame);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Y))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.PolyCount);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.R))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.DrawCalls);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha1))
        {
            AdroitProfiler_GameObjectController.SetSlot(0);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdroitProfiler_GameObjectController.SetSlot(1);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha3))
        {
            AdroitProfiler_GameObjectController.SetSlot(2);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha4))
        {
            AdroitProfiler_GameObjectController.SetSlot(3);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha5))
        {
            AdroitProfiler_GameObjectController.SetSlot(4);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha6))
        {
            AdroitProfiler_GameObjectController.SetSlot(5);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha7))
        {
            AdroitProfiler_GameObjectController.SetSlot(6);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha8))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(7);
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha9))
        {
            AdroitProfiler_GameObjectController.TurnOnAllSlots();
        }
        else if (pressingLeftBracket && Input.GetKeyDown(KeyCode.Alpha0))
        {
            AdroitProfiler_GameObjectController.TurnOffAllSlots();
        }
        //for automated tester
        else if (AdroitProfiler_AutomatedTester != null && pressingLeftBracket && Input.GetKeyDown(KeyCode.P))
        {
            AdroitProfiler_AutomatedTester.GotoPreviousTestCase();
        }
        else if (AdroitProfiler_AutomatedTester != null && pressingLeftBracket && Input.GetKeyDown(KeyCode.N))
        {
            AdroitProfiler_AutomatedTester.GotoNextTestCase();
        }
        //for automated clicker
        else if (AutoClicker != null && pressingRightBracket && Input.GetKeyDown(KeyCode.C))
        {
            AutoClicker.disableClickers = !AutoClicker.disableClickers;
        }
       
    }
}
