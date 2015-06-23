using UnityEngine;
using System.Collections;

public static class GlobalInput {



    static Vector2 GetTouchCoords()
    {
        var touchCount = Input.touchCount;
        var lastTouch = Input.touches[touchCount - 1];
        var position = lastTouch.position;

        return position;
    }

    static bool IsTouch()
    {
        return Input.touchCount > 0;
    }

    public static bool IsLeft()
    {

#if UNITY_EDITOR
        return Input.GetKey(KeyCode.A);
#elif UNITY_ANDROID
        if (IsTouch() && GetTouchCoords().x < Screen.width / 2)
        {
            return true;
        }
        return false;       
#endif

    }

    public static bool IsRight()
    {
#if UNITY_EDITOR
        return Input.GetKey(KeyCode.D);
#elif UNITY_ANDROID

        if (IsTouch() && GetTouchCoords().x > Screen.width / 2)
        {
            return true;
        }
        return false;
#endif
    }


    
    

}
