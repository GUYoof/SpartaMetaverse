using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    // 캔버스 스케일러를 연결할 변수 (Inspector에서 할당하거나 자동으로 찾아도 됨)
    private CanvasScaler canvasScaler;

    public Button startButton;  // 버튼을 연결할 변수

    void Start()
    {
        // 화면 해상도를 세로형(9:16)으로 설정 (예: 1080x1920)
        Screen.SetResolution(1080, 1920, true);

        // 현재 씬에 있는 CanvasScaler 컴포넌트를 가져옴
        canvasScaler = FindObjectOfType<CanvasScaler>();

        if (canvasScaler != null)
        {
            // CanvasScaler의 매칭 방식을 세로 기준으로 설정
            canvasScaler.matchWidthOrHeight = 1f;
        }

        // 버튼 클릭 시 씬 전환이 되도록 설정
        if (startButton != null)
        {
            startButton.onClick.AddListener(ChangeScene);
        }
    }

    // 버튼 클릭 시 씬 전환
    public void ChangeScene()
    {
        // TheStack 씬으로 이동
        SceneManager.LoadScene("TheStack");
    }
}
