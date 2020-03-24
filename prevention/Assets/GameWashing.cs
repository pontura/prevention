using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWashing : MonoBehaviour
{
    Animator anim;

    public int id;
    public states state;
    public enum states
    {
        INTRO,
        GAME1,
        GAME2,
        OUTRO
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("ChangeState", 2);
        Events.ItemsListDestroyerDone += ItemsListDestroyerDone;
    }
    void ChangeState()
    {
        id++;
        state = (states)id;
        switch (id)
        {
            case 1:
                anim.Play("handwash1_idle"); break;
            case 2:
                anim.Play("handwash1_rotation"); break;
        }
    }
    int itemsDone = 0;
    void ItemsListDestroyerDone(ItemsListDestroyer i)
    {
        itemsDone++;
        if (itemsDone >= 2)
        {
            itemsDone = 0;
            ChangeState();
        }
           
    }
}
