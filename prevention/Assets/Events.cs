using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {    
    public static System.Action OnChangeScene = delegate { };
    public static System.Action<bool, string> OnActiveDrag = delegate { };
    public static System.Action<bool, string> OnDrag = delegate { };
    //public static System.Action<ItemsListDestroyer> ItemsListDestroyerDone = delegate { };
    public static System.Action<GesturesManager.types, bool> OnGestureActive = delegate { };
    public static System.Action<Cutscene.types, Cutscene.parts, System.Action> OnCutscene = delegate { };
    public static System.Action<float> OnTimeInit = delegate { };
    public static System.Action OnGameDone = delegate { };
    public static System.Action OnTimeout = delegate { };
    public static System.Action OnStep = delegate { };
    public static System.Action<int> OnGamePartAnim = delegate { };
    public static System.Action<float> OnGameProgress = delegate { };

    public static System.Action<string> PlayMusic = delegate { };
    public static System.Action<string> PlaySfx = delegate { };
    public static System.Action<string> PlaySfxRandom = delegate { };
    public static System.Action<string> PlayUISfx = delegate { };
}
