using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDraggerManager : MonoBehaviour
{
    public bool canBeDragged;
    public string itemName;
    public GameObject item_idle;
    public GameObject item_drag;
    public bool isOn;
    Vector3 parentPosition;
    public GameObject button;

    void Start()
    {
        Events.OnActiveDrag += OnActiveDrag;
        Events.OnDrag += OnDrag;
    }
    void OnDestroy()
    {
        Events.OnActiveDrag -= OnActiveDrag;
        Events.OnDrag -= OnDrag;
    }
    void OnActiveDrag(bool _canBeDragged, string _itemName)
    {
        if (itemName != _itemName)
            return;

        item_drag.GetComponent<ItemSoap>().Init();
        canBeDragged = _canBeDragged;

        if (!canBeDragged)
        {
            SetState(false);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        button.SetActive(canBeDragged);
    }
    void OnDrag(bool _isOn, string _itemName)
    {
        if (!canBeDragged)
            return;
        if (itemName != _itemName)
            return;
        this.isOn = _isOn;
        if (isOn)
        {
            parentPosition = item_drag.transform.parent.localPosition;
            Game.Instance.helperManager.SetOff();
        } else
        {
            Game.Instance.helperManager.Init(button.gameObject, Data.Instance.locallizationManager.Translate("action").ToUpper());
        }
        SetState(isOn);
    }
    void SetState(bool isOn)
    {
        if (isOn)
        {           
            item_idle.SetActive(false);
            item_drag.SetActive(true);
        }
        else
        {
            Animation anim = item_idle.GetComponent<Animation>();
            if (anim != null)
                anim.Play();
            item_idle.SetActive(true);
            item_drag.SetActive(false);
        }
    }
    void Update()
    {
        if (!isOn)
            return;
        Vector3 pos = new Vector3(Game.Instance.draggerManager.pos.x, Game.Instance.draggerManager.pos.y, item_drag.transform.localPosition.z);
        item_drag.transform.localPosition = pos - parentPosition;
    }
}
