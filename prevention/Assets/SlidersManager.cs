using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlidersManager : MonoBehaviour
{
    public bool isActive;
    public bool isPlaying;
    public float value;
    public int loops;
    public List<SlideData> all;
    SlideData actual;

    [Serializable]
    public class SlideData
    {
        public GesturesManager.types gestureType;
        public SliderManager sliderManager;
        public float duration;
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
    void OnGestureActive(GesturesManager.types gestureType, bool isOn)
    {
        print("OnGestureActive" + gestureType + " " + isOn);
        Reset();
        if (!isOn)
        {
            isActive = false;
            return;
        }
        loops = 0;
        
        Reset();
        actual = GetData(gestureType);
        if (actual != null)
            actual.sliderManager.Init(this);

        if (
            gestureType == GesturesManager.types.DRAG 
            || gestureType == GesturesManager.types.SLIDE_VERTICAL_LEFT
            || gestureType == GesturesManager.types.SLIDE_VERTICAL_RIGHT
            )
            return;

        isActive = true;
    }
    public void SetPlaying(bool isPlaying)
    {
        this.isPlaying = isPlaying;
    }
    public void SetAnimationProgress(float progress)
    {
        float total = actual.duration * (loops + 1);
        if (progress > total)
        {
            Events.OnStep();
            loops++;
        }
        value = progress / actual.duration;
    }
    public void AddScore()
    {
        Events.OnStep();
        loops++;
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
        isPlaying = false;
        foreach (SlideData data in all)
            data.sliderManager.gameObject.SetActive(false);
    }
}
