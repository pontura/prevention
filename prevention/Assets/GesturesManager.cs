using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GesturesManager : MonoBehaviour
{
    public types type;
    public enum types
    {
        NONE,
        DRAG,
        SLIDE_VERTICAL_LEFT,
        SLIDE_BOTTOM,
        SLIDE_CURVE_LEFT,
        SLIDE_CURVE_RIGHT,
        SLIDE_ROUNDED_CENTER,
        NONE_OUTRO,
        SLIDE_VERTICAL_RIGHT,
        SLIDER_S_LEFT,
        SLIDER_S_RIGHT,
        SLIDE_BOTTOM_2,
        SLIDER_INFINITO_1,
        SLIDER_INFINITO_2   ,
        RULO_1,
        RULO_2
    }
}
