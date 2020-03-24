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

    void Update()
    {
        if (gameWashing.state == GameWashing.states.GAME1)
        {
            if (Vector2.Distance(transform.position, hand1.transform.position) < distance_to_hand)
                Over(hand1);
            else if (Vector2.Distance(transform.position, hand2.transform.position) < distance_to_hand)
                Over(hand2);
            else
                items = null;
        } else if (gameWashing.state == GameWashing.states.GAME2)
        {
            if (Vector2.Distance(transform.position, hand1b.transform.position) < distance_to_hand)
                Over(hand1b);
            else if (Vector2.Distance(transform.position, hand2b.transform.position) < distance_to_hand)
                Over(hand2b);
            else
                items = null;
        }
        else
        {
            return;
        }


        timer += Time.deltaTime;
        if(timer> timerCheck)
        {
            timer = 0;
            if (Vector2.Distance(transform.position, lastPos) > distanceToCheck)
                Check();
            lastPos = transform.position;
        }
    }
    void Check()
    {
        if (items != null)
        {
            items.DestroyPart(transform.position);
        }
    }
    private void Over(GameObject go)
    {
        items = go.GetComponentInChildren<ItemsListDestroyer>();       
    }
}
