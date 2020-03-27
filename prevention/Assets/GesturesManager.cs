using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GesturesManager : MonoBehaviour
{
    public types type;
    public enum types
    {
        NONE,
        SLIDE_VERTICAL,
        SLIDE_HORIZONTAL
    }

    public verticalTypes verticalType;
    public enum verticalTypes
    {
        NONE,
        UP,
        DOWN
    }

    public horizontalTypes horizontalType;
    public enum horizontalTypes
    {
        NONE,
        RIGHT,
        LEFT
    }
    public Vector2 origin;
    public Vector2 pos;
    public float maxSlideVertical = 10;

    void Start()
    {
        Events.OnGestureActive += OnGestureActive;       
    }
    void OnDestroy()
    {
        Events.OnGestureActive -= OnGestureActive;
    }
    void OnGestureActive(types _type, bool isOn)
    {
        type = _type;
    }
    void Update()
    {
        if (type == types.NONE)
            return;

        if(Input.GetMouseButtonDown(0))
            origin = Input.mousePosition;
        else if (Input.GetMouseButtonUp(0))
            origin = Vector2.zero;

        if (origin == Vector2.zero)
            return;

        if (type == types.SLIDE_VERTICAL)
        {
            pos = Input.mousePosition;
            if (pos.y > origin.y + maxSlideVertical && verticalType != verticalTypes.UP)
            {
                verticalType = verticalTypes.UP;
                origin = pos;
                Events.OnGestureVertical(verticalType);
            }
            else if (pos.y < origin.y - maxSlideVertical && verticalType != verticalTypes.DOWN)
            {
                verticalType = verticalTypes.DOWN;
                origin = pos;
                Events.OnGestureVertical(verticalType);
            }
        }

        else if (type == types.SLIDE_HORIZONTAL)
        {
            pos = Input.mousePosition;
            if (pos.x > origin.x + maxSlideVertical && horizontalType != horizontalTypes.RIGHT)
            {
                horizontalType = horizontalTypes.RIGHT;
                origin = pos;
                Events.OnGestureHorizontal(horizontalType);
            }
            else if (pos.x < origin.x - maxSlideVertical && horizontalType != horizontalTypes.LEFT)
            {
                horizontalType = horizontalTypes.LEFT;
                origin = pos;
                Events.OnGestureHorizontal(horizontalType);
            }
        }
    }
}
