using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // "IsMove"�� "IsDamage" �Ķ������ �ؽ� ���� �̸� ����Ͽ� ĳ��, �ִϸ��̼� ���¸� �����ϴ� �� ���
    private static readonly int IsMoving = Animator.StringToHash("IsMove"); // �̵� ���¸� ��Ÿ���� �Ķ����
    private static readonly int IsDamage = Animator.StringToHash("IsDamage"); // ���� ���¸� ��Ÿ���� �Ķ����

    protected Animator animator; // �ִϸ����� ������Ʈ�� �����ϴ� ����

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // �ڽ� ������Ʈ���� �ִϸ����� �˻�
    }

    // �̵��� ���� �ִϸ��̼� ���¸� �����ϴ� �Լ�
    public void Move(Vector2 obj)
    {
        // �̵��� ���� ũ�� �̻��� ���� "IsMove"�� true�� ����
        animator.SetBool(IsMoving, obj.magnitude > 0.5f); // obj.magnitude�� ������ ũ�� (�̵��� ũ��)
    }

    // ���ظ� �޾��� �� �ִϸ��̼� ���¸� �����ϴ� �Լ�
    public void Damage()
    {
        animator.SetBool(IsDamage, true); // ���ظ� �޾��� �� "IsDamage" �Ķ���͸� true�� ����
    }

    // ���� �ð��� ������ �� ���� ���¸� �����ϴ� �Լ�
    public void InincibilityEnd()
    {
        animator.SetBool(IsDamage, false); // "IsDamage"�� false�� �����Ͽ� ���� �ִϸ��̼��� ����
    }
}
