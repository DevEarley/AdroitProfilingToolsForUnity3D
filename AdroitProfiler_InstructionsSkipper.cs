using AdroitStudios.ProSocial;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdroitProfiler_InstructionsSkipper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += _OnSceneLoaded;
    }
    

    private static void _OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var self_AdroitProfiler_InstructionsSkipper = FindObjectOfType<AdroitProfiler_InstructionsSkipper>();
        self_AdroitProfiler_InstructionsSkipper.OnSceneLoaded(scene, mode);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var instructionPanels = FindObjectsOfType<InstructionalPanelUI>().ToList();
        instructionPanels.ForEach(instructionPanel => {
           // instructionPanel.gameObject.SetActive(false);
        });
    }
}
