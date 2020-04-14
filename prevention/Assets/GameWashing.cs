using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameWashing : MonoBehaviour
{
    int id = 0;
    public states forceState;
    public List<GameSettings> gameSettings;
    SlidersManager sliderManager;
    [Serializable]
    public class GameSettings
    {
        public states state;
        public Cutscene.types cutscene;
        public int gameDuration;
        public int score;
        public GesturesManager.types gestureType;
        public AnimationClip introClip;
        public AnimationClip idleClip;
        public AnimationClip[] clips;
    }
    Animator anim;
    
    public states state;
    public enum states
    {
        INTRO,
        GAME1,
        GAME2,
        VERTICAL1_INTRO,
        VERTICAL1,
        VERTICAL2,
        CIRCULOS_INTRO,
        CIRCULOS,
        PULGAR1_INTRO,
        PULGAR1,
        PULGAR2,
        PUNIO1_INTRO,
        PUNIO1,
        PUNIO2,
        OUTRO,
        EXIT
    }
    public GameSettings actualGameSettings;
    void Start()
    {
        sliderManager = Game.Instance.slidersManager;
        anim = GetComponent<Animator>();        
        Events.ItemsListDestroyerDone += ItemsListDestroyerDone;
        Events.OnTimeout += OnTimeout;
        Events.SliderScore += SliderScore;
        Events.OnGamePartAnim += OnGamePartAnim;

        if (forceState != states.INTRO)
        {
            id = (int)forceState;
            actualGameSettings = GetSettings();
        }

        IntoCutscene();
    }
    void OnDestroy()
    {
        Events.ItemsListDestroyerDone -= ItemsListDestroyerDone;
        Events.OnTimeout -= OnTimeout;
        Events.SliderScore -= SliderScore;
        Events.OnGamePartAnim -= OnGamePartAnim;
    }
    void IntoCutscene()
    {
        actualGameSettings = GetSettings();
        state = actualGameSettings.state;

        if(actualGameSettings.cutscene == Cutscene.types.NAILS2)
            Game.Instance.Replay();
        else  if (actualGameSettings.gestureType == GesturesManager.types.NONE)
            Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.INTRO, NextState);
    }
    void NextState()
    {
        gameObject.SetActive(true);
        id++;
        actualGameSettings = GetSettings();        
        state = actualGameSettings.state;

        if (actualGameSettings.introClip != null)
        {
            anim.Play(actualGameSettings.introClip.name);
            StartCoroutine(StartPlaying(actualGameSettings.introClip.averageDuration));
        }
        else if (actualGameSettings.gestureType == GesturesManager.types.NONE)
            IntoCutscene();
        else
        {
            print(state + " intro clip: " + actualGameSettings.introClip);
            StartPlaying(actualGameSettings.introClip.averageDuration);
        }
    }
    IEnumerator StartPlaying(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartPlaying();
    }
    void StartPlaying()
    {

        print("StartPlaying " + actualGameSettings.state);

        SetTotalValues(actualGameSettings.score);
        Events.OnTimeInit(actualGameSettings.gameDuration);

        if(actualGameSettings.idleClip)
            anim.Play(actualGameSettings.idleClip.name);

        switch (actualGameSettings.gestureType)
        {
            case GesturesManager.types.DRAG:
                Events.OnActiveDrag(true, "soap");
                Events.OnDrag(false, "soap");
                break;
            default:
                Events.OnGestureActive(actualGameSettings.gestureType, true);
                break;
        }

    }
    void OnTimeout()
    {
        Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.OUTRO_BAD, Game.Instance.Replay);
        Destroy(gameObject);
    }
    GameSettings GetSettings()
    {
        foreach (GameSettings gs in gameSettings)
        {
            if ((int)gs.state == id)
                return gs;
        }
        id++;
        return GetSettings();
        Debug.LogError("No hay un settings para " + state);
        return null;
    }
    private void OnGameDone()
    {
        Events.OnActiveDrag(false, "soap");
        Events.OnGestureActive(GesturesManager.types.NONE, false);
        Events.OnDrag(false, "soap");
        Events.OnGameDone();
    }
    void Done()
    {
        OnGameDone();
        print("Game ready state:  " + state);
        if (actualGameSettings.cutscene == Cutscene.types.NAILS2)
            StartCoroutine(Outro());
        else
        {
            Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.OUTRO_GOOD, NextState);
            gameObject.SetActive(false);
        }
    }
    IEnumerator Outro()
    {
        anim.Play("outro");
        yield return new WaitForSeconds(10);
        Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.OUTRO_GOOD, NextState);
        gameObject.SetActive(false);
    }
    void SetTotalValues(int _total)
    {
        totalSteps = _total;
        step = 0;
    }
    int itemsDone = 0;
    void ItemsListDestroyerDone(ItemsListDestroyer i)
    {
        itemsDone++;
        if (itemsDone >= 2)
        {
            itemsDone = 0;
            Done();
        }           
    }   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Done();
        if (!sliderManager.isActive)
            return;
        if (sliderManager.isPlaying)
            anim.speed = 1;
        else
            anim.speed = 0;
    }
    public int totalSteps;
    public int step;
    void SliderScore()
    {
        step++;
        if (step >= totalSteps)
            Done();
    }
    void OnGamePartAnim(int direction)
    {
        string animName;
        if (direction == 1)
        {
            animName = actualGameSettings.clips[1].name;
        }
        else
        {
            animName = actualGameSettings.clips[0].name;
        }
        anim.Play(animName);
    }
}
