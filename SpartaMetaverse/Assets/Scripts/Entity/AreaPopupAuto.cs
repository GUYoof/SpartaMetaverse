using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPopupAuto : MonoBehaviour
{
    private GameObject popupUI; // 팝업 UI 오브젝트를 저장할 변수
    private Transform playerTransform; // 현재 트리거 안에 있는 플레이어 Transform
    private bool isPlayerInArea = false; // 플레이어가 트리거 안에 있는지 여부

    // 팝업 UI의 위치를 자식 오브젝트에서 관리하도록 변경
    private PopupPositionManager popupPositionManager;

    void Start()
    {
        // 게임 오브젝트의 이름을 바탕으로 팝업 이름을 결정
        string popupName = gameObject.name + "_Popup";
        popupUI = GameObject.Find(popupName); // 해당 이름을 가진 팝업을 검색

        // 팝업이 존재하면 처음에는 비활성화 상태로 설정
        if (popupUI != null)
        {
            popupUI.SetActive(false);
            popupPositionManager = popupUI.GetComponentInChildren<PopupPositionManager>(); // 자식 오브젝트에서 위치 관리 스크립트 가져오기
        }

        // 플레이어 찾기 (Tag 기반)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    void Update()
    {
        // 플레이어가 트리거 안에 있고 팝업도 존재할 때
        if (isPlayerInArea && popupPositionManager != null && playerTransform != null)
        {
            // 위치 업데이트
            popupPositionManager.UpdatePopupPosition(playerTransform.position);
        }
    }

    // 플레이어가 트리거 영역에 들어왔을 때 호출
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && popupUI != null) // 플레이어가 영역에 들어왔을 때만 팝업을 활성화
        {
            popupUI.SetActive(true);
            playerTransform = other.transform; // 플레이어 Transform 저장
            isPlayerInArea = true; // 트리거 안에 있다는 상태로 변경
        }
    }

    // 플레이어가 트리거 영역에서 나갔을 때 호출
    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 영역을 벗어나면 팝업 제거
        if (other.CompareTag("Player") && popupUI != null)
        {
            popupUI.SetActive(false);
            // 상태 초기화
            isPlayerInArea = false;
            playerTransform = null;
        }
    }
}
