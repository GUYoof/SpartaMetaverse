using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // 따라갈 대상 (보통 플레이어의 Transform)
    public Transform target;

    // 플레이어와 카메라의 초기 x축, y축 거리 차이 (offset)
    private float offsetX;
    private float offsetY;

    // 카메라가 이동할 수 있는 맵의 최소 좌표(왼쪽 아래)와 최대 좌표(오른쪽 위)
    public Vector2 minBounds;
    public Vector2 maxBounds;

    // 카메라 컴포넌트 캐싱
    private Camera cam;

    // 카메라의 절반 높이와 절반 너비 (Clamp 계산에 필요)
    private float halfHeight;
    private float halfWidth;

    // Start는 게임이 시작될 때 한 번만 호출
    void Start()
    {
        // target이 설정되지 않았을 때 경고 출력하고 반환
        if (target == null)
        {
            return;
        }

        // 플레이어와 카메라의 초기 거리(offset)를 구함 (시작 시점 위치 차이)
        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;

        // 카메라 컴포넌트 가져오기
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            return;
        }

        // orthographicSize는 카메라 높이의 절반 
        halfHeight = cam.orthographicSize;

        // aspect(비율)를 곱해서 절반 너비도 구함
        halfWidth = halfHeight * cam.aspect;
    }

    // LateUpdate는 Update 이후에 호출 (카메라 이동은 LateUpdate가 더 안전)
    void LateUpdate()
    {
        // target이 없으면 아무것도 하지 않음
        if (target == null)
            return;

        // 목표 위치: 플레이어 위치 + 처음 저장한 offset만큼 떨어진 곳
        float desiredX = target.position.x + offsetX;
        float desiredY = target.position.y + offsetY;

        // Clamp로 맵의 경계 안에서만 위치하도록 제한
        // (카메라 크기를 고려해서 경계 밖으로 안 나가게 함)
        float clampedX = Mathf.Clamp(
            desiredX,
            minBounds.x + halfWidth,
            maxBounds.x - halfWidth
        );

        float clampedY = Mathf.Clamp(
            desiredY,
            minBounds.y + halfHeight,
            maxBounds.y - halfHeight
        );

        // 최종 위치 적용 (z축은 기존 그대로 유지)
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // 디버그용: 씬 뷰에서 Clamp 경계를 볼 수 있도록 그려줌
    void OnDrawGizmos()
    {
        // 경계 박스를 초록색 선으로 표시
        Gizmos.color = Color.green;

        // 중심 좌표 구하기
        Vector3 center = new Vector3(
            (minBounds.x + maxBounds.x) / 2,
            (minBounds.y + maxBounds.y) / 2,
            0
        );

        // 사이즈 구하기
        Vector3 size = new Vector3(
            (maxBounds.x - minBounds.x),
            (maxBounds.y - minBounds.y),
            0
        );

        // 박스 그리기
        Gizmos.DrawWireCube(center, size);
    }
}