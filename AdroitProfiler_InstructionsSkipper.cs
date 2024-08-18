using AdroitStudios.General.Game;
using AdroitStudios.ProSocial;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdroitProfiler_InstructionsSkipper : MonoBehaviour
{
    public List<Button> Buttons = null;
    private bool Busy = false;
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += _OnSceneLoaded;
    }
    private void LateUpdate()
    {
            
            FindObjectsOfType<Button>().Where(x => x.gameObject.activeInHierarchy&& x.GetComponentInParent<InstructionalPanelUI>()!=null).ToList().ForEach(button =>
            {
                if (Busy == false)
                {
                    Busy = true;
                    StartCoroutine(PressButton(button));
                
                }
            });
        
    }

    IEnumerator PressButton(Button button)
    {

        yield return new WaitForSecondsRealtime(0.1f);
        Debug.Log("button.Select();");
        var pdata = new PointerEventData(null);
        pdata.button = PointerEventData.InputButton.Left;
        button.OnPointerClick(pdata);
        Busy = false;

    }
    //void OnApplicationPause(bool pauseStatus)
    //{
    //    Debug.Log("OnApplicationPause");
    //    if (Buttons != null)
    //    {
    //        FindObjectsOfType<Button>().Where(x => x.gameObject.activeInHierarchy && x.GetComponentInParent<InstructionalPanelUI>() != null).ToList().ForEach(button =>
    //        {
    //            var pdata = new PointerEventData(null);
    //            pdata.button = PointerEventData.InputButton.Left;
    //            button.OnPointerClick(pdata);
    //            Debug.Log("OnApplicationPause | button.Select();");
    //        });
    //    }
    //}
    private static void _OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var self_AdroitProfiler_InstructionsSkipper = FindObjectOfType<AdroitProfiler_InstructionsSkipper>();
        self_AdroitProfiler_InstructionsSkipper.OnSceneLoaded(scene, mode);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Buttons = FindObjectsOfType<Button>().ToList();

        //instructionPanels.ForEach(instructionPanel => {
        //    instructionPanel.gameObject.SetActive(false);
        //});
    }
}
