using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button StartButton;
    Button ExitButton;

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        StartButton = transform.Find("StartButton").GetComponent<Button>();
        ExitButton = transform.Find("ExitButton").GetComponent<Button>();

        StartButton.onClick.AddListener(OnClickStartButton); //
        ExitButton.onClick.AddListener(OnClickExitButton);
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
