using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {    
    public static System.Action OnChangeScene = delegate { };
    public static System.Action<bool, string> OnDrag = delegate { };
    public static System.Action<ItemsListDestroyer> ItemsListDestroyerDone = delegate { };
    
}
