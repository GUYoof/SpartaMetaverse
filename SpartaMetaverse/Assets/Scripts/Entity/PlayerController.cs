using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerController�� BaseController�� ��ӹ޾� �÷��̾��� �Է��� ó���Ѵ�.
// Ű���� �Է��� �� �����Ӹ��� �о� �ǽð����� ������ �����Ѵ�.
public class PlayerController : BaseController
{
    Camera playerCamera;

    protected override void Awake()
    {
        base.Awake();
        playerCamera = Camera.main; // ī�޶� ĳ��
    }

    protected override void HandleAction()
    {
        base.HandleAction(); //�߰�

        // Ű���� �Է����� �̵� ���� ����
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D �Ǵ� ����Ű �¿�
        float vertical = Input.GetAxisRaw("Vertical");     // W/S �Ǵ� ����Ű ����
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // ���콺 ��ġ�� �ü� ���� ����
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = playerCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        // �ʹ� ������ ���� �ʱ�ȭ
        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }

        // ���콺 Ŭ�� �� ���� �÷��� ����
        isAttacking = Input.GetMouseButton(0);
    }
}
