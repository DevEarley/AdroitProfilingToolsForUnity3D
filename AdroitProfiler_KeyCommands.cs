using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AdroitProfiler_UIBehaviour))]
[RequireComponent(typeof(AdroitProfiler_Logger))]
[RequireComponent(typeof(AdroitProfiler_GameObjectController))]
public class AdroitProfiler_KeyCommands : MonoBehaviour
{
    public TextMeshProUGUI Instructions;
    public GameObject Instructions_Short;
    private AdroitProfiler_UIBehaviour AdroitProfiler_UIBehaviour;
    private AdroitProfiler_Logger AdroitProfiler_Logger;
    private AdroitProfiler_GameObjectController AdroitProfiler_GameObjectController;

    private void Start()
    {
        AdroitProfiler_Logger = gameObject.GetComponent<AdroitProfiler_Logger>();
        AdroitProfiler_UIBehaviour = gameObject.GetComponent<AdroitProfiler_UIBehaviour>();
        AdroitProfiler_GameObjectController = gameObject.GetComponent<AdroitProfiler_GameObjectController>();
        Instructions.text = "Show Key Commands: Shift+K\r\n" + "" +
            "Toggle slots: Shift + 1,2,3 ... 8\r\n" + "" +
            "Set slots: Alt + 1,2,3 ... 8\r\n" +
            "Duplicate: Shift + Alt + 1,2,3 ... 8\r\n" +
            "Turn on all slots: Shift + 9\r\n" +
            "Turn off all slots: Shift + 0\r\n" +
            "Confirm game object: Enter\r\n" +
            "Capture Metric: Shift + C\r\n" +
            "Clear Duplicates: Shift + X\r\n" +
            "Save Logs: Shift + L\r\n";
    }

    void Update()
    {

        var pressingShift = Input.GetKey(KeyCode.LeftShift);
        var pressingAlt = Input.GetKey(KeyCode.LeftAlt);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AdroitProfiler_GameObjectController.ConfirmSlot();
        }
        //shift and alt
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha1))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(0);
        }
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(1);
        }
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha3))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(2);
        }
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha4))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(3);
        }
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha5))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(4);
        }
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha6))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(5);
        }
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha7))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(6);
        }
        else if (pressingAlt && pressingShift && Input.GetKeyDown(KeyCode.Alpha8))
        {
            AdroitProfiler_GameObjectController.DuplicateSlot(7);
        }
        //shift
        else if (pressingShift && Input.GetKeyDown(KeyCode.L))
        {
            AdroitProfiler_Logger.SaveLogs();
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.X))
        {
            AdroitProfiler_GameObjectController.ClearDuplicates();
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.C))
        {
            AdroitProfiler_Logger.CapturePerformanceForEvent("Manual Capture");
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.T))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.TimePerFrame);
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.G))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.GC);
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.M))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.SystemMemory);
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.P))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.PolyCount);
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.V))
        {
            AdroitProfiler_UIBehaviour.UpdateState(AdroitProfiler_UIBehaviour_State.OverDraw);
        }
        else if (pressingShift && Input.GetKeyDown(KeyCode.K))
        {
            var isActive = Instructions.gameObject.activeSelf;
            Instructions.gameObject.SetActive(isActive == false);
            Instructions_Short.SetActive(isActive == true);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha1))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(0);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(1);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha3))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(2);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha4))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(3);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha5))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(4);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha6))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(5);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha7))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(6);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha8))
        {
            AdroitProfiler_GameObjectController.ToggleSlot(7);
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha9))
        {
            AdroitProfiler_GameObjectController.TurnOnAllSlots();
        }
        else if (pressingShift&& Input.GetKeyDown(KeyCode.Alpha0))
        {
            AdroitProfiler_GameObjectController.TurnOffAllSlots();
        }
        //alt
       
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha1))
        {
            AdroitProfiler_GameObjectController.SetSlot(0);
        }
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha2))
        {
            AdroitProfiler_GameObjectController.SetSlot(1);
        }
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha3))
        {
            AdroitProfiler_GameObjectController.SetSlot(2);
        }
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha4))
        {
            AdroitProfiler_GameObjectController.SetSlot(3);
        }
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha5))
        {
            AdroitProfiler_GameObjectController.SetSlot(4);
        }
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha6))
        {
            AdroitProfiler_GameObjectController.SetSlot(5);
        }
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha7))
        {
            AdroitProfiler_GameObjectController.SetSlot(6);
        }
        else if (pressingAlt&& Input.GetKeyDown(KeyCode.Alpha8))
        {
            AdroitProfiler_GameObjectController.SetSlot(7);
        }
       
     
     
    }
}