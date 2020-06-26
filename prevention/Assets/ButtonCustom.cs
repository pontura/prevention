using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCustom : MonoBehaviour
{
    public string buttonName;

    public void OnClick()
    {
        Events.OnDrag(true, buttonName);
    }
    public void OnRelease()
    {

#if UNITY_WEBGL
        return;
#endif
        Events.OnDrag(false, buttonName);
    }
}
