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
        Reset();
        if (!isOn)
            return;
        loops = 0;
        isActive = true;
        Reset();
        actual = GetData(gestureType);
        actual.sliderManager.Init(this);
    }
    public void SetPlaying(bool isPlaying)
    {
        this.isPlaying = isPlaying;
    }
    public void SetAnimationProgress(float progress)
    {
        float total = actual.duration * (loops + 1);
        if (progress> total)
        {
            Events.SliderScore();
            loops++;
        }
        value = progress / actual.duration;
    }
    SlideData GetData(GesturesManager.types gestureType)
    {
        foreach (SlideData data in all)
            if (data.gestureType == gestureType)
                return data;
        return null;
    }
    private void Reset()
    {
        foreach (SlideData data in all)
            data.sliderManager.gameObject.SetActive(false);
    }
}
