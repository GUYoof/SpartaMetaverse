using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // ���� ��� (���� �÷��̾��� Transform)
    public Transform target;

    // �÷��̾�� ī�޶��� �ʱ� x��, y�� �Ÿ� ���� (offset)
    private float offsetX;
    private float offsetY;

    // ī�޶� �̵��� �� �ִ� ���� �ּ� ��ǥ(���� �Ʒ�)�� �ִ� ��ǥ(������ ��)
    public Vector2 minBounds;
    public Vector2 maxBounds;

    // ī�޶� ������Ʈ ĳ��
    private Camera cam;

    // ī�޶��� ���� ���̿� ���� �ʺ� (Clamp ��꿡 �ʿ�)
    private float halfHeight;
    private float halfWidth;

    // Start�� ������ ���۵� �� �� ���� ȣ��
    void Start()
    {
        // target�� �������� �ʾ��� �� ��� ����ϰ� ��ȯ
        if (target == null)
        {
            return;
        }

        // �÷��̾�� ī�޶��� �ʱ� �Ÿ�(offset)�� ���� (���� ���� ��ġ ����)
        offsetX = transform.position.x - target.position.x;
        offsetY = transform.position.y - target.position.y;

        // ī�޶� ������Ʈ ��������
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            return;
        }

        // orthographicSize�� ī�޶� ������ ���� 
        halfHeight = cam.orthographicSize;

        // aspect(����)�� ���ؼ� ���� �ʺ� ����
        halfWidth = halfHeight * cam.aspect;
    }

    // LateUpdate�� Update ���Ŀ� ȣ�� (ī�޶� �̵��� LateUpdate�� �� ����)
    void LateUpdate()
    {
        // target�� ������ �ƹ��͵� ���� ����
        if (target == null)
            return;

        // ��ǥ ��ġ: �÷��̾� ��ġ + ó�� ������ offset��ŭ ������ ��
        float desiredX = target.position.x + offsetX;
        float desiredY = target.position.y + offsetY;

        // Clamp�� ���� ��� �ȿ����� ��ġ�ϵ��� ����
        // (ī�޶� ũ�⸦ ����ؼ� ��� ������ �� ������ ��)
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

        // ���� ��ġ ���� (z���� ���� �״�� ����)
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // ����׿�: �� �信�� Clamp ��踦 �� �� �ֵ��� �׷���
    void OnDrawGizmos()
    {
        // ��� �ڽ��� �ʷϻ� ������ ǥ��
        Gizmos.color = Color.green;

        // �߽� ��ǥ ���ϱ�
        Vector3 center = new Vector3(
            (minBounds.x + maxBounds.x) / 2,
            (minBounds.y + maxBounds.y) / 2,
            0
        );

        // ������ ���ϱ�
        Vector3 size = new Vector3(
            (maxBounds.x - minBounds.x),
            (maxBounds.y - minBounds.y),
            0
        );

        // �ڽ� �׸���
        Gizmos.DrawWireCube(center, size);
    }
}