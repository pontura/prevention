using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenesManager : MonoBehaviour
{
    public List<Cutscene> all;
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
        Cutscene cutscene_to_add = null;
        foreach (Cutscene c in all)
        {
            if (c.type == type && c.part == part)
                cutscene_to_add = c;
        }
        Cutscene cutscene = Instantiate(cutscene_to_add);
        cutscene.transform.SetParent(container);
        cutscene.transform.localPosition = Vector3.zero;
        cutscene.transform.localScale = Vector3.one;
        cutscene.Init(action);
    }
}
