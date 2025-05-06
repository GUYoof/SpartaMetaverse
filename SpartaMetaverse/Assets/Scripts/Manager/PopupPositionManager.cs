using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPositionManager : MonoBehaviour
{
    private Camera mainCamera; // ���� ī�޶�
    private Vector2 screenOffsetLeaderboard = new Vector2(-2500f, 100f); // LeaderboardArea_Popup ������
    private Vector2 screenOffsetGame = new Vector2(0f, 200f); // GameArea_Popup ������
    private Vector2 screenOffsetNPC01 = new Vector2(-500f, 150f); // NPC_01Area_Popup ������
    private Vector2 screenOffsetNPC02 = new Vector2(500f, 150f);  // NPC_02Area_Popup ������

    // �ڽ� ������Ʈ���� RectTransform�� ������ ������
    [SerializeField] private RectTransform leaderboardPopupRectTransform;
    [SerializeField] private RectTransform gameAreaPopupRectTransform;
    [SerializeField] private RectTransform npc01AreaPopupRectTransform;
    [SerializeField] private RectTransform npc02AreaPopupRectTransform;

    private RectTransform canvasRectTransform;

    void Start()
    {
        // ���� ī�޶� ����
        mainCamera = Camera.main;

        // ĵ������ RectTransform ��������
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
    }

    // �ڽ� ������Ʈ���� ��ġ�� �����ϴ� �Լ�
    public void UpdatePopupPosition(Vector3 playerPosition)
    {
        UpdateChildPosition(leaderboardPopupRectTransform, screenOffsetLeaderboard, playerPosition);
        UpdateChildPosition(gameAreaPopupRectTransform, screenOffsetGame, playerPosition);
    }

    // �� �ڽ� ������Ʈ�� ��ġ�� ������Ʈ�ϴ� �Լ�
    private void UpdateChildPosition(RectTransform childRectTransform, Vector2 screenOffset, Vector3 playerPosition)
    {
        if (childRectTransform != null)
        {
            // ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 screenPos = mainCamera.WorldToScreenPoint(playerPosition);

            // ȭ�� ��ǥ�� ������ �߰�
            Vector2 screenPos2D = new Vector2(screenPos.x, screenPos.y) + screenOffset;

            // ȭ�� ��ǥ �� ĵ���� ���� ��ǥ�� ��ȯ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,  // ĵ������ RectTransform
                screenPos2D,  // ȭ�� ��ǥ�� �������� �߰��� ��ǥ
                mainCamera,  // ���� ī�޶�
                out Vector2 localPos // ĵ���� �� ���� ��ǥ
            );

            // �ڽ� ������Ʈ�� RectTransform�� ���ο� ���� ��ǥ�� ����
            childRectTransform.anchoredPosition = localPos;
        }
    }
}
