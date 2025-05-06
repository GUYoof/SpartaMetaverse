using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : BaseUI
{
    TextMeshProUGUI scoreTest;
    TextMeshProUGUI comboTest;
    TextMeshProUGUI bestScoreTest;
    TextMeshProUGUI bestComboTest;

    Button startButton;
    Button exitButton;


    protected override UIState GetUIState()
    {
        return UIState.Score;
    }

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        scoreTest = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        comboTest = transform.Find("ComboText").GetComponent<TextMeshProUGUI>();
        bestScoreTest = transform.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        bestComboTest = transform.Find("BestComboText").GetComponent<TextMeshProUGUI>();

        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void SetUI(int score, int combo, int bestScore, int bestCombo)
    {
        scoreTest.text = score.ToString();
        comboTest.text = combo.ToString();
        bestScoreTest.text = bestScore.ToString();
        bestComboTest.text = bestCombo.ToString();
    }

    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }

    void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }
}
