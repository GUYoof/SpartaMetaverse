using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerController는 BaseController를 상속받아 플레이어의 입력을 처리한다.
// 키보드 입력을 매 프레임마다 읽어 실시간으로 방향을 갱신한다.
public class PlayerController : BaseController
{
    Camera playerCamera;

    protected override void Awake()
    {
        base.Awake();
        playerCamera = Camera.main; // 카메라 캐싱
    }

    protected override void HandleAction()
    {
        base.HandleAction(); //추가

        // 키보드 입력으로 이동 방향 설정
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D 또는 방향키 좌우
        float vertical = Input.GetAxisRaw("Vertical");     // W/S 또는 방향키 상하
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 마우스 위치로 시선 방향 설정
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = playerCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // 너무 가까우면 방향 초기화
        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }

        // 마우스 클릭 시 공격 플래그 설정
        isAttacking = Input.GetMouseButton(0);
    }
}
