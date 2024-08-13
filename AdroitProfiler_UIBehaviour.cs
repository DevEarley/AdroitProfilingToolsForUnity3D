using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_UIBehaviour : MonoBehaviour
{
    public TextMeshProUGUI TMProGUI_FPS;
    public TextMeshProUGUI TMProGUI_TenthSecMax;
    public TextMeshProUGUI TMProGUI_QuarterSecMax;
    public TextMeshProUGUI TMProGUI_HalfSecMax;
    public TextMeshProUGUI TMProGUI_5SecMax;
    public TextMeshProUGUI TMProGUI_10SecMax;

    private AdroitProfiler_State AdroitProfiler_State;

    private void Start()
    {
        AdroitProfiler_State = this.gameObject.GetComponent<AdroitProfiler_State>();
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //RESET GO LIST
    }
    void LateUpdate()
    {
         UpdateTimeAndCurrentFrame();
         UpdateUIWithTime("Tenth Second", AdroitProfiler_State.MaxInThis_TenthSecond_TimePerFrame, AdroitProfiler_State.AverageFPSFor_TenthSecond, TMProGUI_TenthSecMax);
         UpdateUIWithTime("Quarter Second", AdroitProfiler_State.MaxInThis_QuarterSecond_TimePerFrame, AdroitProfiler_State.AverageFPSFor_QuarterSecond, TMProGUI_QuarterSecMax);
         UpdateUIWithTime("Half Second", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, AdroitProfiler_State.AverageFPSFor_HalfSecond, TMProGUI_HalfSecMax);
         UpdateUIWithTime("5 Seconds", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, AdroitProfiler_State.AverageFPSFor_5Seconds, TMProGUI_5SecMax);
         UpdateUIWithTime("10 Seconds", AdroitProfiler_State.MaxInThis_HalfSecond_TimePerFrame, AdroitProfiler_State.AverageFPSFor_10Seconds, TMProGUI_10SecMax);
    }
    
   

    private void UpdateTimeAndCurrentFrame()
    {
        var text = "";
        text += "This Frame: " + AdroitProfiler_State.TimeThisFrame.ToString("000") + " ms \n\r";
        text += "Current Time: " + AdroitProfiler_Service.FormatTime(Time.time) + "\n\r";
        TMProGUI_FPS.text = text;
    }

    private void UpdateUIWithTime(string name, float time, int FPS, TextMeshProUGUI TMProGUI_label)
    {
        var text = "";
        text += name + ": " + time.ToString("000") + " ms | "+ FPS.ToString("000") + "FPS";
        TMProGUI_label.text = text; 
    }
}
