using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {    
    public static System.Action OnChangeScene = delegate { };
    public static System.Action<bool, string> OnActiveDrag = delegate { };
    public static System.Action<bool, string> OnDrag = delegate { };
    public static System.Action<ItemsListDestroyer> ItemsListDestroyerDone = delegate { };
    public static System.Action<GesturesManager.types, bool> OnGestureActive = delegate { };
    public static System.Action<GesturesManager.verticalTypes> OnGestureVertical = delegate { };
    public static System.Action<GesturesManager.horizontalTypes> OnGestureHorizontal = delegate { };
    public static System.Action<Cutscene.types, Cutscene.parts, System.Action> OnCutscene = delegate { };
    public static System.Action<float> OnTimeInit = delegate { };
    public static System.Action OnGameDone = delegate { };
    public static System.Action OnTimeout = delegate { };
    public static System.Action SliderScore = delegate { };
    
}
