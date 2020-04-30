using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemsListDestroyer : MonoBehaviour
{
    public List<GameObject> all;
    Vector2 limitsX = new Vector2(-1.72f, 2.18f);
    Vector2 limitsY = new Vector2(3.12f, -1.8f);
    float _z;
    public void Start()
    {
        GameWashing gs = GetComponentInParent<GameWashing>();
        Events.OnGameProgress += OnGameProgress;
        if (all.Count > 0)
            return;
        int id = 0;
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (id > 0)
            {
                Dirt dirt = Instantiate(gs.dirt);
                all.Add(dirt.gameObject);
                dirt.transform.SetParent(transform);
                dirt.transform.localPosition = t.localPosition;
                dirt.transform.localScale = t.localScale;
                dirt.transform.localEulerAngles = t.localEulerAngles;
                _z = dirt.transform.localPosition.z;
                t.gameObject.SetActive(false);
            }
            id++;
        }
        Game.Instance.AddTotalScore(all.Count);
    }
    private void OnDestroy()
    {
        Events.OnGameProgress -= OnGameProgress;
    }
    void OnGameProgress(float v)
    {
        if (Game.Instance.draggerManager.dragging)
            return;

        float value = Mathf.Lerp(0, all.Count, 1-v);
        int id = 0;
        foreach(GameObject go in all)
        {
            if (id > value)
                go.SetActive(false);
            else
                go.SetActive(true);
            id++;
        }

    }
    public void DestroyPart(GameObject go)
    {

        int prob = 100;
        switch(Data.Instance.userData.levelID)
        {
            case 1:
                prob = 15; break;
            case 2:
                prob = 40; break;
            case 3:
                prob = 60; break;

        }
        if (Random.Range(0, 100) < prob)
        {
            if(Data.Instance.userData.levelID > 2)
                MovePart(go);
            else
                go.transform.localPosition = GetRandomPos();
        }
        else
        {
            all.Remove(go);
            go.SetActive(false);
            Events.OnStep();
        }
    }
    Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(limitsX.x, limitsX.y), Random.Range(limitsY.x, limitsY.y),_z);
    }
    void MovePart(GameObject go)
    {
        go.transform.DOLocalMove(GetRandomPos(), 1);
    }
}
