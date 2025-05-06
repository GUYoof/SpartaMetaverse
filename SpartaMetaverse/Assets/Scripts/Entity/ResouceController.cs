using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResouceController : MonoBehaviour
{
    // ü�� ���� ��Ÿ�� 
    [SerializeField] private float healthChangeDelay = 0.5f;

    // �ٸ� ������Ʈ ������
    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    // ������ ü�� ���� �� ���� �ð� ����
    private float timeSinceLastChange = float.MaxValue;

    // ���� ü�� 
    public float CurrentHealth { get; private set; }

    // �ִ� ü��
    public float Maxhealth => statHandler.Health;

    private void Awake()
    {
        // ���� ������Ʈ�� ���� ������Ʈ���� ������
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        // ���� ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
        CurrentHealth = statHandler.Health;
    }

    private void Update()
    {
        // ü�� ���� ��Ÿ���� ���� ������ �ð� ����
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;

            // ��Ÿ���� �������� �ִϸ��̼ǿ� ���� ���� ���� ȣ��
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InincibilityEnd();
            }
        }
    }

    // ü���� �����ϴ� �޼��� (������ ������, ����� ȸ��)
    public bool changeHealth(float change)
    {
        // ������ 0�̰ų� ��Ÿ�� ���̸� ����
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        // ü�� ������ ������ Ÿ�̸� �ʱ�ȭ
        timeSinceLastChange = 0f;

        // ü�� ���� ����
        CurrentHealth += change;

        // �ִ� ü���� ���� �ʰ�, �ּ� 0���� ����
        CurrentHealth = CurrentHealth > Maxhealth ? Maxhealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        // �������� �޾����� ������ �ִϸ��̼� ����
        if (change < 0)
        {
            animationHandler.Damage();
        }

        // ü���� 0 ���϶�� ���� ó�� ȣ��
        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    // ĳ���Ͱ� �׾��� �� ȣ�� 
    private void Death()
    {

    }
}
