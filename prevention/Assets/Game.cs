using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    static Game mInstance = null;
    public DraggerManager draggerManager;
    public SlidersManager slidersManager;
    public HelperManager helperManager;
    public int totalSteps;
    public int steps;

    public static Game Instance
    {
        get
        {
            if (mInstance == null)
            {
                Debug.LogError("Algo llama a Game antes de inicializarse");
            }
            return mInstance;
        }
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
    }
    private void Start()
    {
        Events.OnGameDone += OnGameDone;
        Events.OnStep += OnStep;
    }
    private void OnDestroy()
    {
        Events.OnGameDone -= OnGameDone;
        Events.OnStep -= OnStep;
    }
    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Done");
    }
    
    void ItemsListDestroyerDone(ItemsListDestroyer i)
    {
        steps++;
        if (steps >= 2)
        {
            steps = 0;
            Events.OnGameDone();
        }
    }
    void OnGameDone()
    {
        steps = 0;
        totalSteps = 0;
        Events.OnActiveDrag(false, "soap");
        Events.OnGestureActive(GesturesManager.types.NONE, false);
        Events.OnDrag(false, "soap");
    }
    public void AddTotalScore(int total)
    {
        totalSteps += total;
    }
    void OnStep()
    {
        steps++;
        if (steps >= totalSteps)
            Events.OnGameDone();
        float value;
        if (steps == 0)
            value = totalSteps;
        else
            value = (float)steps / (float)totalSteps;

        Events.OnGameProgress(value);
    }
}
