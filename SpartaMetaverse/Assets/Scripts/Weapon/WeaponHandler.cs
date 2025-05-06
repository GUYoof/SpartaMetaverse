using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    // 공격 관련 변수들
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f; // 공격 딜레이 (초 단위)
    [SerializeField] private float weaponSize = 1f; // 무기 크기
    [SerializeField] private float power = 1f; // 공격력
    [SerializeField] private float speed = 1f; // 공격 속도
    [SerializeField] private float attackRange = 1f; // 공격 범위
    [SerializeField] public LayerMask target; // 공격 가능한 대상의 레이어

    // 넉백 관련 변수들
    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false; // 넉백 여부
    [SerializeField] private float knockbackPower = 1f; // 넉백의 힘
    [SerializeField] private float knockbackTime = 1f; // 넉백 지속 시간

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




    public BaseController Controller { get; private set; }  // 부모 객체의 BaseController 참조

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");  // 애니메이션 파라미터 해시 (공격 여부)
    private Animator animator;  // 무기의 애니메이터 컴포넌트
    private SpriteRenderer weaponRenderer;  // 무기의 스프라이트 렌더러

    // 초기화 메서드 (Awake)
    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();  // 부모 객체의 BaseController 컴포넌트 가져오기
        animator = GetComponentInChildren<Animator>();  // 자식 객체의 Animator 컴포넌트 가져오기
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();  // 자식 객체의 SpriteRenderer 컴포넌트 가져오기

        SetAnimatorSettings();  // 애니메이터 설정
    }

    protected virtual void Start()
    {

    }

    // 애니메이터 설정 메서드
    private void SetAnimatorSettings()
    {
        animator.speed = 1.0f / Delay;  // 공격 딜레이에 따라 애니메이션 속도 조정
        transform.localScale = Vector3.one * WeaponSize;  // 무기의 크기를 설정
    }

    // 공격 메서드 (애니메이터 트리거로 공격 시작)
    public virtual void Attack()
    {
        Debug.Log("Attack triggered!");
        animator.SetTrigger(IsAttack);
    }
    // 무기 회전 메서드 (왼쪽/오른쪽으로 회전)
    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}   
