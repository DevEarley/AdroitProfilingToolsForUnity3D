using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(AdroitProfiler_GameObjectController))]
public class AdroitProfiler_GameObjectController_Buttons : Editor
{

    override public void OnInspectorGUI()
    {
        AdroitProfiler_GameObjectController GOController = (AdroitProfiler_GameObjectController)target;
        if (EditorApplication.isPlaying == true)
        {
            if (GUILayout.Button("Duplicate Slot #1"))
            {
                GOController.DuplicateSlot(0);
            }
            if (GUILayout.Button("Duplicate Slot #2"))
            {
                GOController.DuplicateSlot(1);
            }
            if (GUILayout.Button("Duplicate Slot #3"))
            {
                GOController.DuplicateSlot(2);
            }
            if (GUILayout.Button("Duplicate Slot #4"))
            {
                GOController.DuplicateSlot(3);
            }
            if (GUILayout.Button("Duplicate Slot #5"))
            {
                GOController.DuplicateSlot(4);
            }
            if (GUILayout.Button("Duplicate Slot #6"))
            {
                GOController.DuplicateSlot(5);
            }
            if (GUILayout.Button("Duplicate Slot #7"))
            {
                GOController.DuplicateSlot(6);
            }
            if (GUILayout.Button("Duplicate Slot #8"))
            {
                GOController.DuplicateSlot(7);
            }
            if (GUILayout.Button("Clear Duplicates"))
            {
                GOController.ClearDuplicates();
            }
            if (GUILayout.Button("Turn On All Slots"))
            {
                GOController.TurnOnAllSlots();
            }
            if (GUILayout.Button("Turn Off All Slots"))
            {
                GOController.TurnOffAllSlots();
            }
            if (GUILayout.Button("Set Slot 1"))
            {
                GOController.SetSlot(0);
            }
            if (GUILayout.Button("Set Slot 2"))
            {
                GOController.SetSlot(1);
            }
            if (GUILayout.Button("Set Slot 3"))
            {
                GOController.SetSlot(2);
            }
            if (GUILayout.Button("Set Slot 4"))
            {
                GOController.SetSlot(3);
            }
            if (GUILayout.Button("Set Slot 5"))
            {
                GOController.SetSlot(4);
            }
            if (GUILayout.Button("Set Slot 6"))
            {
                GOController.SetSlot(5);
            }
            if (GUILayout.Button("Set Slot 7"))
            {
                GOController.SetSlot(6);
            }
            if (GUILayout.Button("Set Slot 8"))
            {
                GOController.SetSlot(7);
            }
            if (GUILayout.Button("Toggle Slot 1"))
            {
                GOController.ToggleSlot(0);
            }
            if (GUILayout.Button("Toggle Slot 2"))
            {
                GOController.ToggleSlot(1);
            }
            if (GUILayout.Button("Toggle Slot 3"))
            {
                GOController.ToggleSlot(2);
            }
            if (GUILayout.Button("Toggle Slot 4"))
            {
                GOController.ToggleSlot(3);
            }
            if (GUILayout.Button("Toggle Slot 5"))
            {
                GOController.ToggleSlot(4);
            }
            if (GUILayout.Button("Toggle Slot 6"))
            {
                GOController.ToggleSlot(5);
            }
            if (GUILayout.Button("Toggle Slot 7"))
            {
                GOController.ToggleSlot(6);
            }
            if (GUILayout.Button("Toggle Slot 8"))
            {
                GOController.ToggleSlot(7);
            }
        }
        DrawDefaultInspector();
    }
}
#endif

[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_GameObjectController : MonoBehaviour
{
    public TextMeshProUGUI TMProGUI_GameObjectList;
    public bool SkipInstructions = true;
    /*
    TODO:

    Add the following to "Instruction Panel UI". Is there a way to do this automatically some how? Autoclicking the screen?

        var AdroitProfiler_GameObjectController = FindObjectOfType<AdroitProfiler_GameObjectController>();
        if(AdroitProfiler_GameObjectController!= null && AdroitProfiler_GameObjectController.SkipInstructions == true)
        {
            HideInstruction();
        }


    */

    [HideInInspector]
    public Dictionary<int,GameObject> Slots = new Dictionary<int,GameObject>();
    private List<GameObject> Duplicates = new List<GameObject>();
    [HideInInspector]
    public GameObject SelectedGameObject;
    [HideInInspector]
    public GameObject RootGameObject;
    private List<GameObject> GameObjectList = new List<GameObject>(15);
    private int GameObjectListOffset = 0;
    public float DuplicationOffset = 1.0f;
    private bool SettingSlot = false;
    private int CurrentSlot = 0;
    void Start()
    {
        TMProGUI_GameObjectList.text = "";

    }
    private void Update()
    {
        if (SettingSlot)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                GameObjectListOffset--;
                if (GameObjectListOffset < 0) GameObjectListOffset = 0;
                ShowListOfGameObjects(RootGameObject);

            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                GameObjectListOffset++;
                if (GameObjectListOffset > GameObjectList.Count - 1) GameObjectListOffset = GameObjectList.Count - 1;
                ShowListOfGameObjects(RootGameObject);

            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                GameObjectListOffset = 0;
                ShowListOfGameObjects(SelectedGameObject);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                GameObjectListOffset = 0;
                SelectParentOrNull();
            }
            SelectedGameObject = GameObjectList.FirstOrDefault();
        }

        void SelectParentOrNull()
        {
            if (RootGameObject==null || RootGameObject.transform.parent == null)
            {
                ShowListOfGameObjects(null);
            }
            else
            {
                ShowListOfGameObjects(RootGameObject.transform.parent.gameObject);
            }
        }
    }   


    public void DuplicateSlot(int number)
    {
        if (Slots.ContainsKey(number))
        {
            var offset = Vector3.zero;
            var prefab = Slots[number];
            var mesh = prefab.GetComponent<MeshRenderer>();
            var skinnedmesh = prefab.GetComponent<SkinnedMeshRenderer>();
            if (skinnedmesh != null)
            {
                offset = new Vector3(skinnedmesh.bounds.size.x, 0, skinnedmesh.bounds.size.z);

            }
            else if (mesh != null)
            {
                offset = new Vector3(mesh.bounds.size.x, 0, mesh.bounds.size.z);
            }
            else
            {
                offset = Vector3.one * DuplicationOffset;
            }
            var duplicate = GameObject.Instantiate(Slots[number], offset * (Duplicates.Count+1) , Quaternion.identity);
            Duplicates.Add(duplicate);
        }
    }

    public void SetSlot(int number)
    {
        if (SettingSlot == true )
        {
            SetSlotForNumber(number);
        }
        else if (SettingSlot == false)
        {
            CurrentSlot = number;
            SettingSlot = true;
            ShowListOfGameObjects(null);
        }
    }

    private void SetSlotForNumber(int number)
    {
        if (Slots.ContainsKey(number))
        {
            Slots[number] = SelectedGameObject;
        }
        else
        {
            Slots.Add(number, SelectedGameObject);

        }
        SettingSlot = false;
        ShowSlots();
    }

    private void ShowSlots()
    {
        TMProGUI_GameObjectList.text = "";
        Slots.ToList().ForEach(x => TMProGUI_GameObjectList.text += x.Value.name + "\n");
    }

    public void ToggleSlot(int number)
    {
        if (number < Slots.Count)
        {
            Slots[number].SetActive(!Slots[number].activeInHierarchy);
        }
    }
    public void TurnOffSlot(int number)
    {
        if (number < Slots.Count)
        {
            Slots[number].SetActive(false);
        }
    }

    public void TurnOnSlot(int number)
    {
        if (number < Slots.Count)
        {
            Slots[number].SetActive(true);
        }
    }

    public void TurnOnAllSlots()
    {
        TurnOnSlot(0);
        TurnOnSlot(1);
        TurnOnSlot(2);
        TurnOnSlot(3);
        TurnOnSlot(4);
        TurnOnSlot(5);
        TurnOnSlot(6);
        TurnOnSlot(7);
    }
    public void ConfirmSlot()
    {
        SetSlotForNumber(CurrentSlot);
    }
    public void TurnOffAllSlots()
    {
        TurnOffSlot(0);
        TurnOffSlot(1);
        TurnOffSlot(2);
        TurnOffSlot(3);
        TurnOffSlot(4);
        TurnOffSlot(5);
        TurnOffSlot(6);
        TurnOffSlot(7);
    }

    public void ClearDuplicates()
    {
        Duplicates.ForEach(duplicate =>
        {
            if(duplicate != null) {
            Destroy(duplicate);
            }
            duplicate = null;
        });
        Duplicates.Clear();
    }

    private void ShowListOfGameObjects(GameObject newRoot)
    {
        
        UpdateListOfGameObjects( newRoot);
        TMProGUI_GameObjectList.text = "> ";

        GameObjectList.ForEach(go =>
        {
            if (go == null) return;
            if (go.transform.parent != null)
            {
                TMProGUI_GameObjectList.text += go.transform.parent.gameObject.name + " / " + go.name + "\n";
            }
            else
            {
                TMProGUI_GameObjectList.text += "scene_root / " + go.name + "\n";
            }
        });
    }

    private void UpdateListOfGameObjects(GameObject newRoot)
    {
        RootGameObject = newRoot;
        var scene = SceneManager.GetActiveScene();
        if(RootGameObject == null)
        {
            GameObjectList = scene.GetRootGameObjects().Where(x => x.activeInHierarchy).Skip(GameObjectListOffset).ToList();
        }
        else
        {
            GameObjectList = RootGameObject.GetComponentsInChildren<Transform>().Select(t => t.gameObject).Where(t => t.transform.parent== RootGameObject.transform).Skip(GameObjectListOffset).ToList();
        }
        
    }
}
