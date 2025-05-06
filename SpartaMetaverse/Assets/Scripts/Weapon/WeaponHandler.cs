using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    // ���� ���� ������
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; // ���� ������ (�� ����)
    [SerializeField] private float weaponSize = 1f; // ���� ũ��
    [SerializeField] private float power = 1f; // ���ݷ�
    [SerializeField] private float speed = 1f; // ���� �ӵ�
    [SerializeField] private float attackRange = 1f; // ���� ����
    [SerializeField] public LayerMask target; // ���� ������ ����� ���̾�

    // �˹� ���� ������
    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false; // �˹� ����
    [SerializeField] private float knockbackPower = 1f; // �˹��� ��
    [SerializeField] private float knockbackTime = 1f; // �˹� ���� �ð�

    // Properties
    public float Delay => delay;
    public float WeaponSize => weaponSize;
    public float Power => power;
    public float Speed => speed;
    public float AttackRange => attackRange;
    public LayerMask Target => target;

    public bool IsOnKnockback => isOnKnockback;
    public float KnockbackPower => knockbackPower;
    public float KnockbackTime => knockbackTime;




    public BaseController Controller { get; private set; }  // �θ� ��ü�� BaseController ����

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");  // �ִϸ��̼� �Ķ���� �ؽ� (���� ����)
    private Animator animator;  // ������ �ִϸ����� ������Ʈ
    private SpriteRenderer weaponRenderer;  // ������ ��������Ʈ ������

    // �ʱ�ȭ �޼��� (Awake)
    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();  // �θ� ��ü�� BaseController ������Ʈ ��������
        animator = GetComponentInChildren<Animator>();  // �ڽ� ��ü�� Animator ������Ʈ ��������
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();  // �ڽ� ��ü�� SpriteRenderer ������Ʈ ��������

        SetAnimatorSettings();  // �ִϸ����� ����
    }

    protected virtual void Start()
    {

    }

    // �ִϸ����� ���� �޼���
    private void SetAnimatorSettings()
    {
        animator.speed = 1.0f / Delay;  // ���� �����̿� ���� �ִϸ��̼� �ӵ� ����
        transform.localScale = Vector3.one * WeaponSize;  // ������ ũ�⸦ ����
    }

    // ���� �޼��� (�ִϸ����� Ʈ���ŷ� ���� ����)
    public virtual void Attack()
    {
        Debug.Log("Attack triggered!");
        animator.SetTrigger(IsAttack);
    }
    // ���� ȸ�� �޼��� (����/���������� ȸ��)
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}   
