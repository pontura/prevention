using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameWashing : MonoBehaviour
{
    public states forceState;
    public List<GameSettings> gameSettings;
    SlidersManager sliderManager;
    [Serializable]
    public class GameSettings
    {
        public states state;
        public float duration;
        public GesturesManager.types gestureType;
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
        VERTICAL2_INTRO,
        VERTICAL2,
        CIRCULOS_INTRO,
        CIRCULOS,
        PULGAR1_INTRO,
        PULGAR1,
        PULGAR2_INTRO,
        PULGAR2,
        PUNIO1_INTRO,
        PUNIO1,
        PUNIO2_INTRO,
        PUNIO2,
        OUTRO,
        EXIT
    }
    void Start()
    {
        sliderManager = Game.Instance.slidersManager;
        anim = GetComponent<Animator>();        
        Events.ItemsListDestroyerDone += ItemsListDestroyerDone;
        Events.OnGestureVertical += OnGestureVertical;
        Events.OnGestureHorizontal += OnGestureHorizontal;
        Events.OnTimeout += OnTimeout;
        Events.SliderScore += SliderScore;
        if (forceState != states.INTRO)
        {
            state = forceState;
            Done();
        } else
        Events.OnCutscene(Cutscene.types.SOAP, Cutscene.parts.INTRO, OnCutsceneDone);
    }
    void OnDestroy()
    {
        Events.ItemsListDestroyerDone -= ItemsListDestroyerDone;
        Events.OnGestureVertical -= OnGestureVertical;
        Events.OnGestureHorizontal -= OnGestureHorizontal;
        Events.OnTimeout -= OnTimeout;
        Events.SliderScore -= SliderScore;
    }
    void OnTimeout()
    {
        GameSettings gameSettings = GetSettings(state);
        Cutscene.types cutsceneType = Cutscene.types.SOAP;
        switch (state)
        {
            case states.GAME1:
                cutsceneType = Cutscene.types.SOAP;
                break;
            case states.GAME2:
                cutsceneType = Cutscene.types.SOAP2;
                break;
            case states.VERTICAL1:
                cutsceneType = Cutscene.types.THUMBS1;
                break;
            case states.VERTICAL2:
                cutsceneType = Cutscene.types.THUMBS1;
                break;
        }
        Events.OnCutscene(cutsceneType, Cutscene.parts.OUTRO_BAD, Game.Instance.Replay);
        Destroy(gameObject);
    }
    GameSettings GetSettings(states state)
    {
        foreach (GameSettings gs in gameSettings)
        {
            if (gs.state == state)
                return gs;
        }
        Debug.LogError("No hay un settings para " + state);
        return null;
    }
    void Done()
    {
        Events.OnActiveDrag(false, "soap");
        Events.OnGestureActive(GesturesManager.types.NONE, false);
        Events.OnDrag(false, "soap");
        print("Game ready state:  " + state);
        switch (state)
        {
            case states.GAME1:
                Events.OnCutscene(Cutscene.types.SOAP, Cutscene.parts.OUTRO_GOOD, OnCutsceneDone);
                break;
            case states.GAME2:
                Events.OnCutscene(Cutscene.types.SOAP2, Cutscene.parts.OUTRO_GOOD, NextCutscene);
                state = states.VERTICAL1_INTRO;
                break;
            case states.VERTICAL1:
                Events.OnCutscene(Cutscene.types.THUMBS1, Cutscene.parts.OUTRO_GOOD, NextCutscene);
                state = states.VERTICAL2_INTRO;
                break;
            case states.VERTICAL2:
                Events.OnCutscene(Cutscene.types.THUMBS2, Cutscene.parts.OUTRO_GOOD, NextCutscene);
                state = states.CIRCULOS_INTRO;
                break;
            case states.CIRCULOS:
                Events.OnCutscene(Cutscene.types.CIRCLES, Cutscene.parts.OUTRO_GOOD, NextCutscene);
                state = states.PULGAR1_INTRO;
                break;
            case states.PULGAR1_INTRO:
                Game.Instance.Replay();
                break;
        }
        Events.OnGameDone();
    }
    void NextCutscene()
    {
        print("NextCutscene:  " + state);
        switch (state)
        {
            case states.VERTICAL1_INTRO:
                Events.OnCutscene(Cutscene.types.THUMBS1, Cutscene.parts.INTRO, OnCutsceneDone);
                break;
            case states.VERTICAL2_INTRO:
                Events.OnCutscene(Cutscene.types.THUMBS2, Cutscene.parts.INTRO, OnCutsceneDone);
                break;
            case states.CIRCULOS_INTRO:
                Events.OnCutscene(Cutscene.types.CIRCLES, Cutscene.parts.INTRO, OnCutsceneDone);
                break;
        }
    }
    void OnCutsceneDone()
    {
        print(" OnCutsceneDone ___________ for state:  " + state);       
        switch (state)
        {
            case states.INTRO:
                anim.Play("intro");
                StartCoroutine(ChangeState(states.GAME1, 4));
                break;
            case states.GAME1:
            //    OnCutsceneDone(); //Events.OnCutscene(Cutscene.types.SOAP2, Cutscene.parts.INTRO, OnCutsceneDone);
            //    state = states.GAME2;
            //    break;
            //case states.GAME2:
                StartCoroutine(ChangeState(states.GAME2, 0.5f));
                anim.Play("handwash1_rotation");
                break;
            case states.VERTICAL1_INTRO:
                anim.Play("handwash2_intro");
                StartCoroutine(ChangeState(states.VERTICAL1, 1));
                break;
            case states.VERTICAL2_INTRO:
                anim.Play("handwash2_transition1");
                StartCoroutine(ChangeState(states.VERTICAL2, 0.7f));
                break;
            case states.CIRCULOS_INTRO:
                anim.Play("handwash3_intro");
                StartCoroutine(ChangeState(states.CIRCULOS, 0.7f));
                break;

        }        
    }

    IEnumerator ChangeState(states _state, float delay)
    {
        yield return new WaitForSeconds(delay);
        state = _state;
        print("ChangeState state " +   state );
        switch (state)
        {
           
            case states.GAME1:
                SetTotalValues(12);
                Events.OnActiveDrag(true, "soap");
                Events.OnTimeInit(GetSettings(state).duration);
                anim.Play("handwash1_idle");
                break;
            case states.GAME2:
                Events.OnActiveDrag(true, "soap");
                SetTotalValues(12);
                Events.OnTimeInit(GetSettings(state).duration);
                break;
            case states.VERTICAL1:
                SetTotalValues(4);
                Events.OnTimeInit(GetSettings(state).duration);
                Events.OnGestureActive(GesturesManager.types.SLIDE_CURVE_LEFT, true);
                break;
            case states.VERTICAL2:
                SetTotalValues(4);
                Events.OnTimeInit(GetSettings(state).duration);
                Events.OnGestureActive(GesturesManager.types.SLIDE_CURVE_RIGHT, true);
                break;
            case states.CIRCULOS:
                SetTotalValues(12);
                Events.OnTimeInit(GetSettings(state).duration);
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
                break;
            //case states.PULGAR1_INTRO:
            //    anim.Play("handwash4_intro");
            //    Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, false);
            //    break;
            //case states.PULGAR1:
            //    SetTotalValues(12);
            //    Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
            //    break;
            //case states.PULGAR2_INTRO:
            //    SetTotalValues(12);
            //    Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, false);
            //    anim.Play("handwash4_transition1");
               // Invoke("ChangeState", 1.6f);
           //     break;
            case states.PULGAR2:
                Events.OnGestureActive(GesturesManager.types.SLIDE_VERTICAL, true);
                break;
            case states.PUNIO1_INTRO:
                SetTotalValues(12);
                anim.Play("handwash5_intro");
              //  Invoke("ChangeState", 1.6f);
                break;
            case states.PUNIO1:
                Events.OnGestureActive(GesturesManager.types.SLIDE_HORIZONTAL, true);
                break;
            case states.PUNIO2_INTRO:
                SetTotalValues(12);
                anim.Play("handwash5_transition1");
               // Invoke("ChangeState", 1);
                break;
            case states.PUNIO2:
                Events.OnGestureActive(GesturesManager.types.SLIDE_HORIZONTAL, true);
                break;
            case states.OUTRO:
                SetTotalValues(1000);
                anim.Play("outro");
             //   Invoke("ChangeState", 9);
                break;
            case states.EXIT:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Done");
                break;
        }
        
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
    void OnGestureVertical(GesturesManager.verticalTypes type)
    {
        step++;
        //if (state == states.VERTICAL1)
        //{
            
        //    if (type == GesturesManager.verticalTypes.UP)
        //        anim.Play("handwash2_step1_a");
        //    else if (type == GesturesManager.verticalTypes.DOWN)
        //        anim.Play("handwash2_step1_b");
        //}
        //else if (state == states.VERTICAL2)
        //{
        //    if (type == GesturesManager.verticalTypes.UP)
        //        anim.Play("handwash2_step2_a");
        //    else if (type == GesturesManager.verticalTypes.DOWN)
        //        anim.Play("handwash2_step2_b");
        //}
        //else 
        if (state == states.CIRCULOS)
        {
            if (type == GesturesManager.verticalTypes.UP)
                anim.Play("handwash3_a");
            else if (type == GesturesManager.verticalTypes.DOWN)
                anim.Play("handwash3_b");
        }
        else if (state == states.PULGAR1)
        {
            if (type == GesturesManager.verticalTypes.UP)
                anim.CrossFade("handwash4_step1", 0.2f, 0, 0);
        }
        else if (state == states.PULGAR2)
        {
            if (type == GesturesManager.verticalTypes.UP)
                anim.CrossFade("handwash4_step2", 0.2f, 0,  0);
        }
        if (step > totalSteps)
            Done();
    }
    void OnGestureHorizontal(GesturesManager.horizontalTypes type)
    {
        step++;
        if (state == states.PUNIO1)
        {
            if (type == GesturesManager.horizontalTypes.RIGHT)
                anim.CrossFade("handwash5_step1", 0.2f, -1, 0);
        }
        else if (state == states.PUNIO2)
        {
            if (type == GesturesManager.horizontalTypes.RIGHT)
                anim.CrossFade("handwash5_step2", 0.2f, -1, 0);
        }
        if (step > totalSteps)
            Done();
    }
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
        print("SliderScore step: " + step);
        if (state == states.VERTICAL1 || state == states.VERTICAL2)
        {
            if (step >= totalSteps)
                Done();
        }
    }
}
