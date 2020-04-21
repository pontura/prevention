using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSoap : MonoBehaviour
{
    public GameWashing gameWashing;

    public GameObject hand1;
    public GameObject hand2;

    public GameObject hand1b;
    public GameObject hand2b;

    public float distance_to_hand;
    public ItemsListDestroyer items;

    public Vector2 lastPos;
    float timer;
    public float timerCheck = 0.05f;
    public float distanceToCheck = 0.1f;

    public void Init()
    {

    }
    public void Over(GameObject go)
    {
        Events.PlaySfxRandom("soap");
        OverHand(go);
        if (items != null) {
            items.DestroyPart(go);
            Events.PlaySfxRandom("bubbles");
        }
    }
    private void OverHand(GameObject go)
    {
        items = go.GetComponentInParent<ItemsListDestroyer>();
    }

}
