using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// UI 상태 정의 (홈, 게임, 점수화면)
public enum UIState
{
    Home,
    Game,
    Score,
}

public class UIManager : MonoBehaviour
{
    public int BestScore { get; private set; }
    public int BestCombo { get; private set; }

    static UIManager instance;
    public static UIManager Instance => instance;

    UIState currentState = UIState.Home;

    HomeUI homeUI = null;
    GameUI gameUI = null;
    ScoreUI scoreUI = null;

    TheStack theStack = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지되도록 설정

            // 씬 로드시 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;

            // 저장된 베스트 점수 불러오기
            LoadBestData();

            // 게임 로직 및 UI 요소 초기화
            theStack = FindObjectOfType<TheStack>();

            homeUI = GetComponentInChildren<HomeUI>(true);
            homeUI?.Init(this);

            gameUI = GetComponentInChildren<GameUI>(true);
            gameUI?.Init(this);

            scoreUI = GetComponentInChildren<ScoreUI>(true);
            scoreUI?.Init(this);

            ChangeState(UIState.Home);
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    private void OnDestroy()
    {
        // 이벤트 해제 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때 호출되는 이벤트 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 메인 씬일 경우 ScoreUI는 비활성화
        if (scene.name == "MainScene")
        {
            if (scoreUI != null)
                scoreUI.gameObject.SetActive(false);
        }
        else
        {
            // 그 외 씬에서는 ScoreUI 다시 활성화 가능 (필요 시)
            if (scoreUI != null)
                scoreUI.gameObject.SetActive(true);
        }
    }

    // UI 상태 전환
    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    // 게임 시작 버튼 클릭
    public void OnClickStart()
    {
        theStack.ReStart();
        ChangeState(UIState.Game);
    }

    // 종료 버튼 클릭
    public void OnClickExit()
    {
        // 해상도 및 Canvas 비율 설정 (16:9 비율)
        Screen.SetResolution(1920, 1080, true);

        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.matchWidthOrHeight = 0f; // 가로 기준
        }

        // 메인 씬으로 이동
        SceneManager.LoadScene("MainScene");
    }

    // 게임 중 점수 갱신
    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }

    // 점수 UI 설정 (게임 끝나고 보여줄 베스트 기록)
    public void SetScoreUI()
    {
        // 최고 점수/콤보 업데이트 로직
        if (theStack.Score > BestScore)
            BestScore = theStack.Score;

        if (theStack.MaxCombo > BestCombo)
            BestCombo = theStack.MaxCombo;

        SaveBestData(); // 저장

        scoreUI.SetUI(theStack.Score, theStack.MaxCombo, BestScore, BestCombo);
        ChangeState(UIState.Score);
    }

    private void SaveBestData()
    {
        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("BestCombo", BestCombo);
        PlayerPrefs.Save();
        Debug.Log("BestScore saved: " + BestScore); // 디버깅 로그
    }

    private void LoadBestData()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        BestCombo = PlayerPrefs.GetInt("BestCombo", 0);
        Debug.Log("BestScore loaded: " + BestScore); // 디버깅 로그
    }
}
