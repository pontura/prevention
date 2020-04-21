using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsListDestroyer : MonoBehaviour
{
    public List<GameObject> all;

    public void Start()
    {
        Events.OnGameProgress += OnGameProgress;
        if (all.Count > 0)
            return;
        int id = 0;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if(id>0)
                all.Add(t.gameObject);
            t.gameObject.SetActive(true);
            id++;
        }
        Game.Instance.AddTotalScore(all.Count);
    }
    private void OnDestroy()
    {
        Events.OnGameProgress -= OnGameProgress;
    }
    void OnGameProgress(float v)
    {
        if (Game.Instance.draggerManager.dragging)
            return;

        float value = Mathf.Lerp(0, all.Count, 1-v);
        int id = 0;
        foreach(GameObject go in all)
        {
            if (id > value)
                go.SetActive(false);
            else
                go.SetActive(true);
            id++;
        }

    }
    public void DestroyPart(GameObject go)
    {
        all.Remove(go);
        go.SetActive(false);
        Events.OnStep();
    }
}
