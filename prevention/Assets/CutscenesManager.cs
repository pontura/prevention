using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenesManager : MonoBehaviour
{
    public List<Cutscene> all;
    public List<Cutscene> level_2;
    public Transform container;

    void Awake()
    {
        Events.OnCutscene += OnCutscene;
    }
    
    void OnDestroy()
    {
        Events.OnCutscene -= OnCutscene;
    }

    void OnCutscene(Cutscene.types type, Cutscene.parts part, System.Action action)
    {
        Debug.Log("cutscene type : " + type + " part: " + part + " " + action);

        Cutscene cutscene_to_add = null;
      
        foreach (Cutscene c in all)
        {
            if (c.type == type && c.part == part)
                cutscene_to_add = c;
        }
        if (Data.Instance.userData.levelID == 2)
        {
            foreach (Cutscene c in level_2)
            {
                if (c.type == type && c.part == part)
                    cutscene_to_add = c;
            }
        }

        if (cutscene_to_add == null)
        {
            action();
            Debug.LogError("No hay cutscene de : " + type + " " + part + " " + action);
            return;
        }
        Cutscene cutscene = Instantiate(cutscene_to_add);
        cutscene.transform.SetParent(container);
        cutscene.transform.localPosition = Vector3.zero;
        cutscene.transform.localScale = cutscene_to_add.transform.localScale;
        cutscene.Init(action);
    }
}
