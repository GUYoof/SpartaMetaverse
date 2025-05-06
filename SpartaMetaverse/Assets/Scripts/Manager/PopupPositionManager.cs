using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPositionManager : MonoBehaviour
{
    private Camera mainCamera; // 메인 카메라
    private Vector2 screenOffsetLeaderboard = new Vector2(-2500f, 100f); // LeaderboardArea_Popup 오프셋
    private Vector2 screenOffsetGame = new Vector2(0f, 200f); // GameArea_Popup 오프셋
    private Vector2 screenOffsetNPC01 = new Vector2(-500f, 150f); // NPC_01Area_Popup 오프셋
    private Vector2 screenOffsetNPC02 = new Vector2(500f, 150f);  // NPC_02Area_Popup 오프셋

    // 자식 오브젝트들의 RectTransform을 참조할 변수들
    [SerializeField] private RectTransform leaderboardPopupRectTransform;
    [SerializeField] private RectTransform gameAreaPopupRectTransform;
    [SerializeField] private RectTransform npc01AreaPopupRectTransform;
    [SerializeField] private RectTransform npc02AreaPopupRectTransform;

    private RectTransform canvasRectTransform;

    void Start()
    {
        // 메인 카메라 설정
        mainCamera = Camera.main;

        // 캔버스의 RectTransform 가져오기
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
    }

    // 자식 오브젝트들의 위치를 갱신하는 함수
    public void UpdatePopupPosition(Vector3 playerPosition)
    {
        UpdateChildPosition(leaderboardPopupRectTransform, screenOffsetLeaderboard, playerPosition);
        UpdateChildPosition(gameAreaPopupRectTransform, screenOffsetGame, playerPosition);
    }

    // 각 자식 오브젝트의 위치를 업데이트하는 함수
    private void UpdateChildPosition(RectTransform childRectTransform, Vector2 screenOffset, Vector3 playerPosition)
    {
        if (childRectTransform != null)
        {
            // 월드 좌표를 화면 좌표로 변환
            Vector3 screenPos = mainCamera.WorldToScreenPoint(playerPosition);

            // 화면 좌표에 오프셋 추가
            Vector2 screenPos2D = new Vector2(screenPos.x, screenPos.y) + screenOffset;

            // 화면 좌표 → 캔버스 로컬 좌표로 변환
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,  // 캔버스의 RectTransform
                screenPos2D,  // 화면 좌표에 오프셋을 추가한 좌표
                mainCamera,  // 메인 카메라
                out Vector2 localPos // 캔버스 내 로컬 좌표
            );

            // 자식 오브젝트의 RectTransform에 새로운 로컬 좌표를 적용
            childRectTransform.anchoredPosition = localPos;
        }
    }
}
