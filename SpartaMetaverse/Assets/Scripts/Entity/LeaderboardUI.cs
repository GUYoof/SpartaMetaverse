using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    // BestScorePoint와 BestComboPoint 텍스트 컴포넌트를 에디터에서 할당
    public TextMeshProUGUI BestScorePoint;
    public TextMeshProUGUI BestComboPoint;

    // 코루틴 Start 함수
    IEnumerator Start()
    {
        // UIManager 인스턴스가 생성될 때까지 대기
        yield return new WaitUntil(() => UIManager.Instance != null);

        // BestScore나 BestCombo가 설정될 때까지 대기
        yield return new WaitUntil(() => UIManager.Instance.BestScore >= 0 && UIManager.Instance.BestCombo >= 0);

        // UIManager에서 베스트 점수와 콤보 가져오기
        int bestScore = UIManager.Instance.BestScore;
        int bestCombo = UIManager.Instance.BestCombo;

        // 텍스트 UI에 점수 표시
        BestScorePoint.text = bestScore.ToString();
        BestComboPoint.text = bestCombo.ToString();
    }
}
