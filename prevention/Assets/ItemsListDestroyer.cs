using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsListDestroyer : MonoBehaviour
{
    public List<GameObject> all;

    void Start()
    {
        int id = 0;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if(id>0)
                all.Add(t.gameObject);
            id++;
        }
           
    }
    public void DestroyPart(Vector3 pos)
    {
        if (all.Count <= 0)
            return;

        GameObject go = all[0];
        Destroy(go);
        all.RemoveAt(0);

        if (all.Count <= 0)
            Events.ItemsListDestroyerDone(this);

    }
}
