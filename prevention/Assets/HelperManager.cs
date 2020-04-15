using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperManager : MonoBehaviour
{
    
    public GameObject helper;
    public Text field;
    public Animation anim;

    void Start()
    {
        helper.SetActive(false);
    }
    public void Init(GameObject target, string text)
    {
        anim.Play("drag_on");
        helper.transform.position = target.transform.position;
        field.text = text;
        helper.SetActive(true);
    }
    public void SetOff()
    {
        helper.SetActive(false);
    }
}
