using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;  // 발사체의 무기 핸들러
    private float currentDuration;                  // 발사체가 얼마나 오래 지속됐는지 추적
    private Vector2 direction;                      // 발사체 방향
    private bool isReady;                           // 발사체가 준비되었는지 여부
    private Transform pivot;                        // 발사체 피벗 

    private Rigidbody2D _rigidbody;                 // 발사체의 물리 엔진 구성요소
    private SpriteRenderer spriteRenderer;          // 발사체 시각 구성요소

    public bool fxOnDestory = true;                 // 파괴 시 이펙트를 생성할지 여부

    private ProjectileManager projectileManager;    // 발사체를 관리하는 매니저 

    private void Awake()
    {
        // 컴포넌트 캐싱
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);

        // Collider가 있는지 확인
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("No Collider2D found on projectile!");
        }

        // Rigidbody2D가 있는지 확인
        if (_rigidbody == null)
        {
            Debug.LogError("No Rigidbody2D found on projectile!");
        }
    }

    private void Update()
    {
        if (!isReady) return; // 준비 안 됐으면 스킵

        currentDuration += Time.deltaTime;

        // 지속 시간이 다 되면 파괴
        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // 발사체 속도 체크
        float speed = rangeWeaponHandler.Speed;
        if (speed < 1f)
        {
            Debug.LogWarning("Bullet speed is too low: " + speed);
        }

        // 지정된 방향으로 이동
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 레벨(벽 등)과 충돌했는지 체크
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Collided with level: " + collision.gameObject.name);
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestory);
        }
        // 타겟(적 등)과 충돌했는지 체크
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Collided with target: " + collision.gameObject.name);
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }

    // 발사체 초기화
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        // 변수 초기화
        this.rangeWeaponHandler = weaponHandler;
        this.projectileManager = projectileManager;

        this.direction = direction;
        currentDuration = 0;

        // 크기와 색상 설정
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        // 방향 맞추기
        transform.right = this.direction;

        // 방향에 따라 피벗 회전 조정
        if (direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    // 발사체 파괴 함수
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        // 여기서 나중에 이펙트 같은 걸 추가 가능 
        Destroy(this.gameObject);
    }
}
