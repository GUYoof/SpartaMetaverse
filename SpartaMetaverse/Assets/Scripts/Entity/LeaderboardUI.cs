using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    // BestScorePoint�� BestComboPoint �ؽ�Ʈ ������Ʈ�� �����Ϳ��� �Ҵ�
    public TextMeshProUGUI BestScorePoint;
    public TextMeshProUGUI BestComboPoint;

    // �ڷ�ƾ Start �Լ�
    IEnumerator Start()
    {
        // UIManager �ν��Ͻ��� ������ ������ ���
        yield return new WaitUntil(() => UIManager.Instance != null);

        // BestScore�� BestCombo�� ������ ������ ���
        yield return new WaitUntil(() => UIManager.Instance.BestScore >= 0 && UIManager.Instance.BestCombo >= 0);

        // UIManager���� ����Ʈ ������ �޺� ��������
        int bestScore = UIManager.Instance.BestScore;
        int bestCombo = UIManager.Instance.BestCombo;

        // �ؽ�Ʈ UI�� ���� ǥ��
        BestScorePoint.text = bestScore.ToString();
        BestComboPoint.text = bestCombo.ToString();
    }
}
