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
        Data.Instance.LoadScene("GameOver");
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
            go.gameObject.SetActive(false);
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
            GameWashing.GameSettings s;
            s = GetGameSettings(gw, GameWashing.states.GAME1);
            s.gameDuration = gData.soap_duration[id];
            s.score = gData.soap_score[id];

            s = GetGameSettings(gw, GameWashing.states.GAME2);
            s.gameDuration = gData.soap_duration[id];
            s.score = gData.soap_score[id];

            s = GetGameSettings(gw, GameWashing.states.VERTICAL1);
            s.gameDuration = gData.vertical[id];
            s.score = gData.vertical_score[id];

            s = GetGameSettings(gw, GameWashing.states.VERTICAL2);
            s.gameDuration = gData.vertical[id];
            s.score = gData.vertical_score[id];

            s = GetGameSettings(gw, GameWashing.states.CIRCULOS);
            s.gameDuration = gData.circles[id];
            s.score = gData.circles_score[id];

            s = GetGameSettings(gw, GameWashing.states.PULGAR1);
            s.gameDuration = gData.pulgar[id];
            s.score = gData.pulgar_score[id];

            s = GetGameSettings(gw, GameWashing.states.PULGAR2);
            s.gameDuration = gData.pulgar[id];
            s.score = gData.pulgar_score[id];

            s = GetGameSettings(gw, GameWashing.states.PUNIO1);
            s.gameDuration = gData.nails[id];
            s.score = gData.nails_score[id];

            s = GetGameSettings(gw, GameWashing.states.PUNIO2);
            s.gameDuration = gData.nails[id];
            s.score = gData.nails_score[id];

            id++;
        }
    }
    GameWashing.GameSettings GetGameSettings(GameWashing gameWashing, GameWashing.states state)
    {
        foreach(GameWashing.GameSettings settings in gameWashing.gameSettings)
        {
            if (settings.state == state)
                return settings;
        }
        return null;
    }
}
