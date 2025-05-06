using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // ü���� 1���� 100 ���̷� ������ �� �ִ� ����
    [Range(1, 100)][SerializeField] private int health = 10; // Unity�� �ν����Ϳ��� ���� �����̴��� ����

    // Health ������Ƽ�� health ������ ���� ������
    public int Health
    { 
        get => health; // health ���� ��ȯ
        set => health = Mathf.Clamp(value, 0, 100); // value�� 0�� 100 ���̷� �����Ͽ� ����
    }

    // �ӵ��� 1f���� 20f ���̷� ������ �� �ִ� ����
    [Range(1f, 20f)][SerializeField] private float speed = 3; 

    // Speed ������Ƽ�� speed ������ ���� ������
    public float Speed
    {
        get => speed; // speed ���� ��ȯ
        set => speed = Mathf.Clamp(value, 0, 20); // value�� 0�� 20 ���̷� �����Ͽ� ����
    }
}   

