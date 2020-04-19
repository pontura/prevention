using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public Star[] stars;

    public void Init(int starsQty)
    {
        int id = 0;
        foreach(Star star in stars)
        {
            id++;
            star.Init(id <= starsQty);            
        }
            
    }
}
