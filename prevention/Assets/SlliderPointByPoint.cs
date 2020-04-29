using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlliderPointByPoint : SliderManager
{
    public GameObject endPointsContainer;
    public List<EndPoint> all;
    public int id;
    public int nextID;
    public bool pingpong;
    public bool back;
    
    Vector2 pos;
    EndPoint activePoint;
   
    public override void Start()
    {
        Invoke("Init", 0.1f);
    }
    void Init()
    {
        int id = 0;
        foreach (EndPoint ep in endPointsContainer.GetComponentsInChildren<EndPoint>())
        {
            ep.Init(this, id);
            all.Add(ep);
            id++;
        }
        id = 0;
        nextID = id + 1;        
        sliderButton.transform.position = all[0].transform.position;
        SetActive();
        SetHelper(true);
    }
    void Update()
    {
        if (activePoint == null)
            return;
        pos = activePoint.transform.position;
        sliderButton.transform.position = Vector2.Lerp(sliderButton.transform.position, pos, 0.3f);
    }
    void Next()
    {
        if (pingpong)
        {
            if (back)
            {
                id--;
                if (id <= 0)
                {
                    id = 0;
                    back = false;
                    Events.OnSliderChangeDirection(true);
                }
            }
            else
            {           
                id++;
                if (id >= all.Count - 1)
                {
                    id = all.Count - 1;
                    back = true;
                    Events.OnSliderChangeDirection(false);
                }                
            }           
        }
        else
        {
            id++;
            if (id >= all.Count)
                id = 0;
        }
        if (back)
            nextID = id - 1;
        else
        {
            nextID = id + 1;
            if (nextID >= all.Count)
                nextID = 0;
        }

        Events.OnSliderPointByPointProgression(back, (float)id/(all.Count-1), pingpong);

        SetActive();
    }
    void SetActive()
    {
        activePoint = all[id];
        activePoint.SetState(false);
        all[nextID].SetState(true);
        
    }
    public void SetOver(EndPoint ep)
    {
        if(ep.id == nextID)
        {
            sliderButton.SetOn(true);
            Events.PlayNextSliderSfx();
            Events.OnStep();
            Next();
            SetHelper(false);
        }
    }
}
