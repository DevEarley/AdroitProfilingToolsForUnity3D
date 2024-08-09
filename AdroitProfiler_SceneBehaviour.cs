using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdroitProfiler_SceneBehaviour : MonoBehaviour
{

    public List<string> scenes;
    public int currentScene;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

    }

    public void GoToNextSceneInList()
    {
        currentScene++;
        SceneManager.LoadScene(scenes[currentScene]);
    }
}
