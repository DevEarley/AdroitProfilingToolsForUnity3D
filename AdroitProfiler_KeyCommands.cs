using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_UIBehaviour))]
[RequireComponent(typeof(AdroitProfiler_State))]
[RequireComponent(typeof(AdroitProfiler_Logger))]
[RequireComponent(typeof(AdroitProfiler_GameObjectController))]
//[RequireComponent(typeof(AdroitProfiler_AutoClicker_Heartbeat))]
public class AdroitProfiler_KeyCommands : MonoBehaviour
{
    public TextMeshProUGUI Instructions;
    public GameObject Instructions_Short;
    private AdroitProfiler_UIBehaviour AdroitProfiler_UIBehaviour;
    private AdroitProfiler_Logger AdroitProfiler_Logger;
    private AdroitProfiler_Heartbeat AdroitProfiler_Heartbeat;
  //  private AdroitProfiler_AutoClicker_Heartbeat AdroitProfiler_AutoClicker_Heartbeat;
    private AdroitProfiler_GameObjectController AdroitProfiler_GameObjectController;
    
    private void Start()
    {
     //   AdroitProfiler_AutoClicker_Heartbeat = gameObject.GetComponent<AdroitProfiler_AutoClicker_Heartbeat>();
        AdroitProfiler_Heartbeat = gameObject.GetComponent<AdroitProfiler_Heartbeat>();
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        AdroitProfiler_UIBehaviour = gameObject.GetComponent<AdroitProfiler_UIBehaviour>();
        AdroitProfiler_GameObjectController = gameObject.GetComponent<AdroitProfiler_GameObjectController>();
        Instructions.text = "Show Key Commands: K\r\n" + "" +
            "Set slots: [ + 1,2,3 ... 8\r\n" +
            "Toggle slots: ] + 1,2,3 ... 8\r\n" + "" +
            "Duplicate: [ + ] + 1,2,3 ... 8\r\n" +
            "Turn on all slots: [ + 9\r\n" +
            "Turn off all slots: [ + 0\r\n" +
            "Confirm game object: Enter\r\n" +
            "Capture Metric: [ + C\r\n" +
            "Clear Duplicates: [ + X\r\n" +
            "Save Logs: [ + L\r\n" +
            "Show Time Per Frame Metrics: [ + F\r\n" +
            // "Show Sys Memory Metrics: [ + M\r\n" +
            "Show Draw Metrics: [ + R\r\n" +
            "Show Poly Count Metrics: [ + Y\r\n" +
            "Toggle UI: [ + U\r\n" +
           // "Set Auto Clickers: \\ + 1,2,3,4\r\n" +
            //"Set Auto Mover: \\ + W,A,S,D\r\n" +
            "Toggle Profiler: [ + Q";
    }

    void Update()
    {
     
        var pressingLeftBracket = Input.GetKey(KeyCode.LeftBracket);
        var pressingBackslash = Input.GetKey(KeyCode.Backslash);
        var pressingRightBracket = Input.GetKey(KeyCode.RightBracket);
        if (pressingLeftBracket  && Input.GetKeyDown(KeyCode.Q))
        {
            AdroitProfiler_Heartbeat.Paused = !AdroitProfiler_Heartbeat.Paused;
            return;
        }
        if (AdroitProfiler_Heartbeat.Paused) return;
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            AdroitProfiler_GameObjectController.ConfirmSlot();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            var isActive = Instructions.gameObject.activeSelf;
            Instructions.gameObject.SetActive(isActive == false);
            Instructions_Short.SetActive(isActive == true);
        }
        //shift and alt
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
        //shift
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
    
        //else if (pressingShift && Input.GetKeyDown(KeyCode.M))
        //{
        //    AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.SystemMemory);
        //}
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
        //alt

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

        //pressingBackslash - Auto Clicker

        //else if (pressingBackslash && Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    AdroitProfiler_AutoClicker_Heartbeat.SetSlot(0);
        //}
        //else if (pressingBackslash && Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    AdroitProfiler_AutoClicker_Heartbeat.SetSlot(1);

        //}
        //else if (pressingBackslash && Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    AdroitProfiler_AutoClicker_Heartbeat.SetSlot(2);

        //}
        //else if (pressingBackslash && Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    AdroitProfiler_AutoClicker_Heartbeat.SetSlot(3);

        //}



    }
}
