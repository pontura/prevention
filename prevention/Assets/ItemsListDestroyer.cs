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
        Game.Instance.AddTotalScore(all.Count);
    }
    public void DestroyPart(GameObject go)
    {
        all.Remove(go);
        Destroy(go);

        Events.OnStep();
    }
}
