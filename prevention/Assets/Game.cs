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
    public GameWashing[] levels;
    public Timeline timeline;

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
        Reset();
    }
    private void Start()
    {
        levels[Data.Instance.userData.levelID - 1].gameObject.SetActive(true);

        Events.OnGameDone += OnGameDone;
        Events.OnStep += OnStep;
        LoadDataFromSettings();
    }
    private void OnDestroy()
    {
        Events.OnGameDone -= OnGameDone;
        Events.OnStep -= OnStep;
    }
    public void GameOver()
    {
        Data.Instance.LoadScene("LevelSelector");
    }
    public void Replay()
    {
        Data.Instance.LoadScene("Game");
    }

    public void LevelComplete()
    {
        Reset();
        GetComponent<Summary>().Init();      
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
        print(total + " totalSteps: " + totalSteps);
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
    public void Reset()
    {
        foreach (GameWashing go in levels)
        {
#if UNITY_ANDROID 
            go.state = GameWashing.states.INTRO;
#endif
            go.gameObject.SetActive(false);
        }
    }
    void LoadDataFromSettings()
    {
        if (Data.Instance.settings.gameData.circles.Length == 0)
            Invoke("LoadDataFromSettings", 0.1f);
        else
            LoadData();
    }
    void LoadData()
    {
        Settings.GameData gData = Data.Instance.settings.gameData;
        int id = 0;
        foreach (GameWashing gw in levels)
        {
            SetGameSettings(gw, GameWashing.states.GAME1, gData.soap_duration[id], gData.soap_score[id]);
            SetGameSettings(gw, GameWashing.states.GAME2, gData.soap_duration[id], gData.soap_score[id]);

            SetGameSettings(gw, GameWashing.states.VERTICAL1,gData.vertical[id], gData.vertical_score[id]);
            SetGameSettings(gw, GameWashing.states.VERTICAL2, gData.vertical[id], gData.vertical_score[id]);

            SetGameSettings(gw, GameWashing.states.CIRCULOS, gData.circles[id], gData.circles_score[id]);

            SetGameSettings(gw, GameWashing.states.PULGAR1, gData.pulgar[id], gData.pulgar_score[id]);
            SetGameSettings(gw, GameWashing.states.PULGAR2, gData.pulgar[id], gData.pulgar_score[id]);

            SetGameSettings(gw, GameWashing.states.PUNIO1, gData.nails[id], gData.nails_score[id]);
            SetGameSettings(gw, GameWashing.states.PUNIO2, gData.nails[id], gData.nails_score[id]);

            id++;
        }
    }
    void SetGameSettings(GameWashing gameWashing, GameWashing.states state, int duration, int score)
    {
        foreach(GameWashing.GameSettings settings in gameWashing.gameSettings)
        {
            if (settings.state == state)
            {
                settings.gameDuration = duration;
                settings.score = score;
            }
        }
    }
}
