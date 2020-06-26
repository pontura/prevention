using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggerManager : MonoBehaviour
{
    public bool dragging;
    public Vector2 pos;
    void Start()
    {
        Events.OnDrag += OnDrag;
    }
    void OnDestroy()
    {
        Events.OnDrag -= OnDrag;
    }
    void OnDrag(bool isOn, string itemName)
    {
        if(isOn)
            Events.OnStartTimer();

        SetPos();
        dragging = isOn;

    }
    private void Update()
    {
        if (dragging)
            SetPos();
    }
    void SetPos()
    {
        var screenPoint = (Input.mousePosition);
        pos = Camera.main.ScreenToWorldPoint(screenPoint);
    }
}
