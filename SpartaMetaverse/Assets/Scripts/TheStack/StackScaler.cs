using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackScaler : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);

        // CanvasScaler 다시 찾아서 세팅
        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.matchWidthOrHeight = 1f;
        }
    }
}
