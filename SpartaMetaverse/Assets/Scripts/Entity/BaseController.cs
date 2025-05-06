using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// BaseController는 캐릭터의 기본적인 이동, 회전 및 공격 처리를 담당하는 베이스 클래스
/// 상속해서 각 캐릭터별 커스텀 동작을 구현
public class BaseController : MonoBehaviour
{
    // 컴포넌트 및 핸들러
    protected Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer characterRenderer; // 캐릭터 본체 스프라이트 렌더러
    [SerializeField] private Transform weaponPivot; // 무기 회전 축
    [SerializeField] private RangeWeaponHandler rangeWeaponHandler; // 무기 핸들러
    [SerializeField] public WeaponHandler WeaponPrefab; // 무기 프리팹

    protected AnimationHandler animationHandler; // 이동 애니메이션 처리
    protected StatHandler statHandler; // 스탯(속도 등) 관리
    protected WeaponHandler weaponHandler; // 무기 회전을 위한 프리팹 위치

    // 이동 및 회전 방향
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection => movementDirection; // 외부에서 조회용

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection => lookDirection; // 외부에서 조회용

    // 공격 여부 
    protected bool isAttacking = false; // 실제 무기 핸들러
    public bool IsAttacking => isAttacking; // 외부에서 조회용

    private float timeSinceLastAttack = 0f; // 공격 딜레이 체크용 타이머

    // 컴포넌트 초기화
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        // Debugging: null 체크
        if (_rigidbody == null) Debug.LogError("Rigidbody2D is not assigned!");
        if (animationHandler == null) Debug.LogError("AnimationHandler is not assigned!");
        if (statHandler == null) Debug.LogError("StatHandler is not assigned!");
        if (rangeWeaponHandler == null) Debug.LogWarning("RangeWeaponHandler is not assigned!");

        // 무기 핸들러를 프리팹에서 생성하거나 자식 오브젝트에서 찾음
        if (WeaponPrefab != null)
        {
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot.position, Quaternion.identity);
            weaponHandler.transform.SetParent(weaponPivot);  // 부모 설정
            weaponHandler.gameObject.SetActive(true);  // 게임 오브젝트 활성화
            Debug.Log(" WeaponPrefab instantiated.");
        }
        else
        {
            Debug.LogError("WeaponPrefab is not assigned!");
            weaponHandler = GetComponentInChildren<WeaponHandler>();  // 자식에서 무기 찾기
        }

        Debug.Log($"Awake: Initial timeSinceLastAttack={timeSinceLastAttack}");
    }

    // 기본 Start 메서드. 필요한 경우 상속해서 사용
    protected virtual void Start()
    {
        Debug.Log("BaseController started.");
    }

    // 매 프레임마다 호출: 행동 처리 및 회전 처리
    protected virtual void Update()
    {
        // 마우스 위치를 화면에서 월드 좌표로 변환
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = (mousePos - (Vector2)transform.position).normalized;

        // 마우스 좌클릭 시 공격 시작
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            Debug.Log("Mouse button down - Start attacking");
        }
        // 마우스 버튼 뗄 때 공격 중지
        if (Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            Debug.Log("Mouse button up - Stop attacking");
        }

        HandleAction();
        Rotate(lookDirection);
    }

    // 물리 업데이트: 이동 처리
    protected virtual void FixedUpdate()
    {
        Move(movementDirection);
    }

    // 사용자 입력이나 AI 동작을 구현하는 메서드
    protected virtual void HandleAction()
    {
        if (weaponHandler == null)
        {
            Debug.Log("weaponHandler is NULL in HandleAction");
            return;
        }

        timeSinceLastAttack += Time.deltaTime;
        Debug.Log($"[HandleAction] isAttacking={isAttacking}, timeSinceLastAttack={timeSinceLastAttack}, Delay={weaponHandler.Delay}");

        if (isAttacking && timeSinceLastAttack >= weaponHandler.Delay)
        {
            Debug.Log($"Attack triggered! weaponHandler is {weaponHandler.GetType()}");
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    // 실제 이동 처리 (Shift 키로 속도 2배 처리)
    public void Move(Vector2 direction)
    {
        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f; // Shift 키 확인
        Vector2 finalDirection = direction * statHandler.Speed * speedMultiplier;
        _rigidbody.velocity = finalDirection;
        animationHandler.Move(finalDirection);
    }

    // 캐릭터 및 무기 회전 처리
    public void Rotate(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 방향을 각도로 변환
        bool isLeft = Mathf.Abs(rotZ) > 90f; // 왼쪽을 보는지 판단
        characterRenderer.flipX = isLeft; // 캐릭터 뒤집기

        // 무기 피벗도 회전
        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    // 공격 처리 메서드
    public virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
        {
            Debug.Log("Attack method called");
            weaponHandler?.Attack();
        }
    }
}
