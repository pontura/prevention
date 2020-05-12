using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemsListDestroyer : MonoBehaviour
{
    public List<GameObject> all;
    public Vector2 limitsX = new Vector2(-1.72f, 2.18f);
    public Vector2 limitsY = new Vector2(3.12f, -1.8f);
    float _z;

    public types type;

    public enum types
    {
        NORMAL,
        SCALE
    }


    public void Start()
    {
        GameWashing gs = GetComponentInParent<GameWashing>();
        Events.OnGameProgress += OnGameProgress;
        if (all.Count > 0)
            return;
        int id = 0;

        if (type == types.SCALE)
            LoopScaler();

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (id > 0)
            {
                Dirt dirt = Instantiate(gs.dirt);
                all.Add(dirt.gameObject);
                dirt.transform.SetParent(transform);
                dirt.transform.localPosition = t.localPosition;
                if (type == types.SCALE)
                {
                    float scaleX = 0.7f;
                    dirt.transform.localScale = new Vector3(scaleX, scaleX, scaleX);
                }
                else
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

        float value = Mathf.Lerp(0, all.Count, 1 - v);
        int id = 0;
        foreach (GameObject go in all)
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
        if (type == types.SCALE)
        {
            float scaleX = go.transform.localScale.x;
            scaleX -= 0.05f;
            go.transform.localScale = new Vector3(scaleX, scaleX, scaleX);
            if (scaleX <= 0.2f)
                Die(go);
            else
                go.GetComponent<Animation>().Play();
        }
        else
        {
            int prob = 100;
            switch (Data.Instance.userData.levelID)
            {
                case 1:
                    prob = 15; break;
                case 2:
                    prob = 60; break;
                case 3:
                    prob = 70; break;

            }
            if (Random.Range(0, 100) < prob)
            {
                Events.PlayUISfx("respawn");
                if (Data.Instance.userData.levelID > 2)
                    MovePart(go);
                else
                    go.transform.localPosition = GetRandomPos();
            }
            else
            {
                Events.PlayUISfx("die");
                Die(go);
            }
        }
    }
    void Die(GameObject go)
    {
        all.Remove(go);
        go.SetActive(false);
        Events.OnStep();
    }
    Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(limitsX.x, limitsX.y), Random.Range(limitsY.x, limitsY.y), _z);
    }
    void MovePart(GameObject go)
    {
        go.transform.DOLocalMove(GetRandomPos(), 1);
    }




    void LoopScaler()
    {
        Invoke("LoopScaler", 0.75f);
        foreach (GameObject go in all)
        {
            float scaleX = go.transform.localScale.x;
            if (go.transform.localScale.x < 0.3f && Random.Range(0,100)<40)
            {
                scaleX += 0.1f;
                go.transform.localScale = new Vector3(scaleX, scaleX, scaleX);
                go.GetComponent<Animation>().Play("scalerRevive");
            }
        }
    }
}
