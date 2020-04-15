using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlidersManager : MonoBehaviour
{
    public bool isActive;
    public float value;
    public List<SlideData> all;
    SlideData actual;

    [Serializable]
    public class SlideData
    {
        public GesturesManager.types gestureType;
        public SliderManager sliderManager;
    }

    void Start()
    {
        Reset();
        Events.OnGestureActive += OnGestureActive;
    }
    void OnDestroy()
    {
        Events.OnGestureActive -= OnGestureActive;
    }
    void OnGestureActive(GesturesManager.types gestureType, bool _isActive)
    {
        print("OnGestureActive" + gestureType + " " + _isActive);
        Reset();
        this.isActive = _isActive;
        if (isActive)
        {
            actual = GetData(gestureType);
            if (actual != null)
                actual.sliderManager.Init(this);
        }
    }
    SlideData GetData(GesturesManager.types gestureType)
    {
        foreach (SlideData data in all)
            if (data.gestureType == gestureType)
                return data;
        Debug.Log("No hay gesto :" + gestureType);
        return null;
    }
    private void Reset()
    {
        value = 0;
        foreach (SlideData data in all)
            data.sliderManager.gameObject.SetActive(false);
    }
}
