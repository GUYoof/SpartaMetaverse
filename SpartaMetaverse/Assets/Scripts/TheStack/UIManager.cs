using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// UI ���� ���� (Ȩ, ����, ����ȭ��)
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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �����ǵ��� ����

            // �� �ε�� �̺�Ʈ ���
            SceneManager.sceneLoaded += OnSceneLoaded;

            // ����� ����Ʈ ���� �ҷ�����
            LoadBestData();

            // ���� ���� �� UI ��� �ʱ�ȭ
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
            Destroy(gameObject); // �ߺ� ���� ����
        }
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� (�޸� ���� ����)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� �� ȣ��Ǵ� �̺�Ʈ �Լ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ���� ��� ScoreUI�� ��Ȱ��ȭ
        if (scene.name == "MainScene")
        {
            if (scoreUI != null)
                scoreUI.gameObject.SetActive(false);
        }
        else
        {
            // �� �� �������� ScoreUI �ٽ� Ȱ��ȭ ���� (�ʿ� ��)
            if (scoreUI != null)
                scoreUI.gameObject.SetActive(true);
        }
    }

    // UI ���� ��ȯ
    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }

    // ���� ���� ��ư Ŭ��
    public void OnClickStart()
    {
        theStack.ReStart();
        ChangeState(UIState.Game);
    }

    // ���� ��ư Ŭ��
    public void OnClickExit()
    {
        // �ػ� �� Canvas ���� ���� (16:9 ����)
        Screen.SetResolution(1920, 1080, true);

        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.matchWidthOrHeight = 0f; // ���� ����
        }

        // ���� ������ �̵�
        SceneManager.LoadScene("MainScene");
    }

    // ���� �� ���� ����
    public void UpdateScore()
    {
        gameUI.SetUI(theStack.Score, theStack.Combo, theStack.MaxCombo);
    }

    // ���� UI ���� (���� ������ ������ ����Ʈ ���)
    public void SetScoreUI()
    {
        // �ְ� ����/�޺� ������Ʈ ����
        if (theStack.Score > BestScore)
            BestScore = theStack.Score;

        if (theStack.MaxCombo > BestCombo)
            BestCombo = theStack.MaxCombo;

        SaveBestData(); // ����

        scoreUI.SetUI(theStack.Score, theStack.MaxCombo, BestScore, BestCombo);
        ChangeState(UIState.Score);
    }

    private void SaveBestData()
    {
        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("BestCombo", BestCombo);
        PlayerPrefs.Save();
        Debug.Log("BestScore saved: " + BestScore); // ����� �α�
    }

    private void LoadBestData()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        BestCombo = PlayerPrefs.GetInt("BestCombo", 0);
        Debug.Log("BestScore loaded: " + BestScore); // ����� �α�
    }
}
