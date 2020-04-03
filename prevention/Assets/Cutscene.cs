using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public types type;
    public parts part;
    public float duration;

    public enum types
    {
        SOAP,
        SOAP2,
        THUMBS1,
        THUMBS2,
        CIRCLES,
        NAILS1,
        NAILS2,
    }
    public enum parts
    {
        INTRO,
        OUTRO_GOOD,
        OUTRO_BAD
    }
    System.Action OnReady;
    public void Init(System.Action OnReady)
    {
        this.OnReady = OnReady;
        Invoke("Done", duration);
    }
    void Done()
    {        
        OnReady();
        OnReady = null;
        Destroy(gameObject);
    }
}
