using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(Grid))]
public class AdroitProfiler_GameObjectController_Buttons : Editor
{

    override public void OnInspectorGUI()
    {
        if (EditorApplication.isPlaying == false) return;
            AdroitProfiler_GameObjectController GOController = (AdroitProfiler_GameObjectController)target;
        if (GUILayout.Button("Duplicate Slot #1"))
        {
            GOController.DuplicateSlot(1);
        }
        if (GUILayout.Button("Duplicate Slot #2"))
        {
            GOController.DuplicateSlot(2);
        }
        if (GUILayout.Button("Duplicate Slot #3"))
        {
            GOController.DuplicateSlot(3);
        }
        if (GUILayout.Button("Duplicate Slot #4"))
        {
            GOController.DuplicateSlot(4);
        }
        if (GUILayout.Button("Duplicate Slot #5"))
        {
            GOController.DuplicateSlot(5);
        }
        if (GUILayout.Button("Duplicate Slot #6"))
        {
            GOController.DuplicateSlot(6);
        }
        if (GUILayout.Button("Duplicate Slot #7"))
        {
            GOController.DuplicateSlot(7);
        }
        if (GUILayout.Button("Duplicate Slot #8"))
        {
            GOController.DuplicateSlot(8);
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

        DrawDefaultInspector();
    }
}
#endif

[RequireComponent(typeof(AdroitProfiler_State))]
public class AdroitProfiler_GameObjectController : MonoBehaviour
{
    public TextMeshProUGUI TMProGUI_GameObjectList;
    public List<GameObject> Slots = new List<GameObject>(8);
    public List<GameObject> Duplicates;
    public GameObject SelectedGameObject;
    public List<GameObject> GameObjectList = new List<GameObject>(15);
    private int GameObjectListOffset = 0;
    private bool SettingSlot = false;
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
                ShowListOfGameObjects();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                GameObjectListOffset++;
                if (GameObjectListOffset > GameObjectList.Count - 1) GameObjectListOffset = GameObjectList.Count - 1;
                ShowListOfGameObjects();
            }
            SelectedGameObject = GameObjectList.FirstOrDefault();
        }
    }
    public void DuplicateSlot(int number)
    {
        if (Slots[number])
        {
            var duplicate = GameObject.Instantiate(Slots[number]);
            var mesh = duplicate.GetComponent<MeshRenderer>();
            var skinnedmesh = duplicate.GetComponent<SkinnedMeshRenderer>();
            if(skinnedmesh != null)
            {
                duplicate.transform.position += new Vector3(skinnedmesh.bounds.size.x,0, skinnedmesh.bounds.size.z);

            }
            else if(mesh != null)
            {
                duplicate.transform.position += new Vector3(mesh.bounds.size.x, 0, mesh.bounds.size.z);
            }
            else
            {
             duplicate.transform.position += Vector3.one;
            }
            Duplicates.Add(duplicate);
        }
    }

    public void SetSlot(int number)
    {
        if(SettingSlot == true && Slots[number])
        {
            Slots[number] = SelectedGameObject;
        }
        if(SettingSlot == false)
        {
            ShowListOfGameObjects();
        }
    }

    public void ToggleSlot(int number)
    {
        if (Slots[number])
        {
            Slots[number].SetActive(!Slots[number].activeInHierarchy);
        }
    }
    public void TurnOffSlot(int number)
    {
        if (Slots[number])
        {
            Slots[number].SetActive(false);
        }
    }

    public void TurnOnSlot(int number)
    {
        if (Slots[number])
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
        Duplicates.ForEach(duplicate => {
            GameObject.Destroy(duplicate);
        });
    }

    private void ShowListOfGameObjects()
    {
        UpdateListOfGameObjects();
        TMProGUI_GameObjectList.text = "";
        GameObjectList.ForEach(go =>
        {
            if (go == null) return;
            if(go.transform.parent != null)
            { 
                TMProGUI_GameObjectList.text += go.transform.parent.gameObject.name + " / " + go.name;
            }
            else
            {
                TMProGUI_GameObjectList.text += "scene_root / " + go.name;
            }
        });
    }

    private void UpdateListOfGameObjects()
    {
        var scene = SceneManager.GetActiveScene();
        GameObjectList = scene.GetRootGameObjects().Where(x => x.activeInHierarchy).Skip(GameObjectListOffset).Take(15).ToList();
    }
}
