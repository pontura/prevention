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
       // Events.OnGestureVertical += OnGestureVertical;
      //  Events.OnGestureHorizontal += OnGestureHorizontal;
        Events.OnTimeout += OnTimeout;
        Events.SliderScore += SliderScore;
#if UNITY_EDITOR
        if (forceState != states.INTRO)
            id = (int)forceState;
#endif
        IntoCutscene();
    }
    void OnDestroy()
    {
        Events.ItemsListDestroyerDone -= ItemsListDestroyerDone;
      //  Events.OnGestureVertical -= OnGestureVertical;
      //  Events.OnGestureHorizontal -= OnGestureHorizontal;
        Events.OnTimeout -= OnTimeout;
        Events.SliderScore -= SliderScore;
    }
    void IntoCutscene()
    {
        actualGameSettings = GetSettings();
        state = actualGameSettings.state;
        if (actualGameSettings.gestureType == GesturesManager.types.NONE)
            Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.INTRO, NextState);
    }
    void NextState()
    {       
        id++;
        actualGameSettings = GetSettings();        
        state = actualGameSettings.state;

        print("Next state " + state);
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
        Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.OUTRO_GOOD, NextState);        
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
    public int totalSteps;
    public int step;
    //void OnGestureVertical(GesturesManager.verticalTypes type)
    //{
    //    step++;
    //    //if (state == states.VERTICAL1)
    //    //{
            
    //    //    if (type == GesturesManager.verticalTypes.UP)
    //    //        anim.Play("handwash2_step1_a");
    //    //    else if (type == GesturesManager.verticalTypes.DOWN)
    //    //        anim.Play("handwash2_step1_b");
    //    //}
    //    //else if (state == states.VERTICAL2)
    //    //{
    //    //    if (type == GesturesManager.verticalTypes.UP)
    //    //        anim.Play("handwash2_step2_a");
    //    //    else if (type == GesturesManager.verticalTypes.DOWN)
    //    //        anim.Play("handwash2_step2_b");
    //    //}
    //    //else 
    //    if (state == states.CIRCULOS)
    //    {
    //        if (type == GesturesManager.verticalTypes.UP)
    //            anim.Play("handwash3_a");
    //        else if (type == GesturesManager.verticalTypes.DOWN)
    //            anim.Play("handwash3_b");
    //    }
    //    else if (state == states.PULGAR1)
    //    {
    //        if (type == GesturesManager.verticalTypes.UP)
    //            anim.CrossFade("handwash4_step1", 0.2f, 0, 0);
    //    }
    //    else if (state == states.PULGAR2)
    //    {
    //        if (type == GesturesManager.verticalTypes.UP)
    //            anim.CrossFade("handwash4_step2", 0.2f, 0,  0);
    //    }
    //    if (step > totalSteps)
    //        Done();
    //}
    //void OnGestureHorizontal(GesturesManager.horizontalTypes type)
    //{
    //    step++;
    //    if (state == states.PUNIO1)
    //    {
    //        if (type == GesturesManager.horizontalTypes.RIGHT)
    //            anim.CrossFade("handwash5_step1", 0.2f, -1, 0);
    //    }
    //    else if (state == states.PUNIO2)
    //    {
    //        if (type == GesturesManager.horizontalTypes.RIGHT)
    //            anim.CrossFade("handwash5_step2", 0.2f, -1, 0);
    //    }
    //    if (step > totalSteps)
    //        Done();
    //}
    private void Update()
    {
        if (!sliderManager.isActive)
            return;
        if (sliderManager.isPlaying)
            anim.speed = 1;
        else
            anim.speed = 0;
    }
    void SliderScore()
    {
        step++;
        if (step >= totalSteps)
            Done();
    }
}
