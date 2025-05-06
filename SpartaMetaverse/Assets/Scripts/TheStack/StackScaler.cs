using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackScaler : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);

        // CanvasScaler �ٽ� ã�Ƽ� ����
        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.matchWidthOrHeight = 1f;
        }
    }
}
