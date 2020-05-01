using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameWashing : MonoBehaviour
{
    int id = 0;
    public Dirt dirt;
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
        
        Events.OnTimeout += OnTimeout;
        Events.OnGamePartAnim += OnGamePartAnim;
        Events.OnGameDone += OnGameDone;
        Events.OnSliderPointByPointProgression += OnSliderPointByPointProgression;

        if (forceState != states.INTRO)
        {
            id = (int)forceState;
            actualGameSettings = GetSettings();
        }
        IntoCutscene();
    }
    void OnDestroy()
    {
        Events.OnGameDone -= OnGameDone;
        Events.OnTimeout -= OnTimeout;
        Events.OnGamePartAnim -= OnGamePartAnim;
        Events.OnSliderPointByPointProgression -= OnSliderPointByPointProgression;
    }
    void IntoCutscene()
    {
        actualGameSettings = GetSettings();
        state = actualGameSettings.state;

        if (actualGameSettings.cutscene == Cutscene.types.NAILS2)
        {
            //end game!
        }
        else if (actualGameSettings.gestureType == GesturesManager.types.NONE)
            Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.INTRO, NextState);
    }
    void NextState()
    {
        anim.speed = 1;
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

        Game.Instance.AddTotalScore(actualGameSettings.score);
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
        Events.StopMusic();
        Events.PlayMusicOnce("lose");
        Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.OUTRO_BAD, Game.Instance.GameOver);
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
    void OnGameDone()
    {

        sliderManager.isActive = false;
        anim.speed = 1;
        Events.PlayUISfx("stepWin");
     //   print("Game ready state:  " + state + " actualGameSettings.cutscene " + actualGameSettings.cutscene);
        if (actualGameSettings.cutscene == Cutscene.types.NAILS2)
        {
            Data.Instance.userData.AllLevelComplete();
            StartCoroutine(Outro());
           
        }            
        else {
            gameObject.SetActive(false);
            Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.OUTRO_GOOD, NextState);
        }
    }
    IEnumerator Outro()
    {
        anim.Play("outro");
        yield return new WaitForEndOfFrame();
        anim.speed = 1;       
        yield return new WaitForSeconds(5);
        
        Events.StopMusic();
        Events.PlayMusicOnce("win");
        Events.OnCutscene(actualGameSettings.cutscene, Cutscene.parts.OUTRO_GOOD, NextState);
        yield return new WaitForSeconds(4);
        Game.Instance.LevelComplete();
        Game.Instance.Reset();
        // gameObject.SetActive(false);        
    }
   
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
            Events.OnGameDone();
        if (!sliderManager.isActive)
            return;

        if (
           actualGameSettings.gestureType != GesturesManager.types.DRAG
           && actualGameSettings.gestureType != GesturesManager.types.NONE
            && actualGameSettings.gestureType != GesturesManager.types.NONE_OUTRO
           )
        {

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < animValue)
            {
                anim.speed = 1;
            }

            else
                anim.speed = 0;
        }

       
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
    public float animValue;
    void OnSliderPointByPointProgression(bool back, float _animvalue, bool pingpong)
    {

        if (!sliderManager.isActive)
            return;
        AnimationClip clip;
        if (back && pingpong)
        {
            clip = actualGameSettings.clips[1];
            animValue = (1 - _animvalue);// * clip.length;
        }
        else
        {
            clip = actualGameSettings.clips[0];
            if (!pingpong)
            {
                if (_animvalue < animValue)
                {
                    anim.Rebind();
                    animValue = 0;
                } else
                    animValue = _animvalue;
            }
            else
            {
                animValue = _animvalue;
            }
           

        }
        anim.Play(clip.name);
        anim.speed = 0;
       // print(_animvalue + "  animValue " + animValue +  " clip.length: " + clip.length + "  pingpong " + pingpong);
    }
}
