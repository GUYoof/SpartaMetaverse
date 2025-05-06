using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;  // �߻�ü�� ���� �ڵ鷯
    private float currentDuration;                  // �߻�ü�� �󸶳� ���� ���ӵƴ��� ����
    private Vector2 direction;                      // �߻�ü ����
    private bool isReady;                           // �߻�ü�� �غ�Ǿ����� ����
    private Transform pivot;                        // �߻�ü �ǹ� 

    private Rigidbody2D _rigidbody;                 // �߻�ü�� ���� ���� �������
    private SpriteRenderer spriteRenderer;          // �߻�ü �ð� �������

    public bool fxOnDestory = true;                 // �ı� �� ����Ʈ�� �������� ����

    private ProjectileManager projectileManager;    // �߻�ü�� �����ϴ� �Ŵ��� 

    private void Awake()
    {
        // ������Ʈ ĳ��
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);

        // Collider�� �ִ��� Ȯ��
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("No Collider2D found on projectile!");
        }

        // Rigidbody2D�� �ִ��� Ȯ��
        if (_rigidbody == null)
        {
            Debug.LogError("No Rigidbody2D found on projectile!");
        }
    }

    private void Update()
    {
        if (!isReady) return; // �غ� �� ������ ��ŵ

        currentDuration += Time.deltaTime;

        // ���� �ð��� �� �Ǹ� �ı�
        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        // �߻�ü �ӵ� üũ
        float speed = rangeWeaponHandler.Speed;
        if (speed < 1f)
        {
            Debug.LogWarning("Bullet speed is too low: " + speed);
        }

        // ������ �������� �̵�
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ����(�� ��)�� �浹�ߴ��� üũ
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Collided with level: " + collision.gameObject.name);
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestory);
        }
        // Ÿ��(�� ��)�� �浹�ߴ��� üũ
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Collided with target: " + collision.gameObject.name);
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }

    // �߻�ü �ʱ�ȭ
    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        // ���� �ʱ�ȭ
        this.rangeWeaponHandler = weaponHandler;
        this.projectileManager = projectileManager;

        this.direction = direction;
        currentDuration = 0;

        // ũ��� ���� ����
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        // ���� ���߱�
        transform.right = this.direction;

        // ���⿡ ���� �ǹ� ȸ�� ����
        if (direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    // �߻�ü �ı� �Լ�
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        // ���⼭ ���߿� ����Ʈ ���� �� �߰� ���� 
        Destroy(this.gameObject);
    }
}
