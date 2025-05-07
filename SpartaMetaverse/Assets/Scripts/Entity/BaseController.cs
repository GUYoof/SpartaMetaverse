using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// BaseController�� ĳ������ �⺻���� �̵�, ȸ�� �� ���� ó���� ����ϴ� ���̽� Ŭ����
/// ����ؼ� �� ĳ���ͺ� Ŀ���� ������ ����
public class BaseController : MonoBehaviour
{
    // ������Ʈ �� �ڵ鷯
    protected Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer characterRenderer; // ĳ���� ��ü ��������Ʈ ������
    [SerializeField] private Transform weaponPivot; // ���� ȸ�� ��
    [SerializeField] private RangeWeaponHandler rangeWeaponHandler; // ���� �ڵ鷯
    [SerializeField] public WeaponHandler WeaponPrefab; // ���� ������

    protected AnimationHandler animationHandler; // �̵� �ִϸ��̼� ó��
    protected StatHandler statHandler; // ����(�ӵ� ��) ����
    protected WeaponHandler weaponHandler; // ���� ȸ���� ���� ������ ��ġ

    // �̵� �� ȸ�� ����
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection => movementDirection; // �ܺο��� ��ȸ��

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection => lookDirection; // �ܺο��� ��ȸ��

    // ���� ���� 
    protected bool isAttacking = false; // ���� ���� �ڵ鷯
    public bool IsAttacking => isAttacking; // �ܺο��� ��ȸ��

    private float timeSinceLastAttack = 0f; // ���� ������ üũ�� Ÿ�̸�

    // ������Ʈ �ʱ�ȭ
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
    }

    // �⺻ Start �޼���. �ʿ��� ��� ����ؼ� ���
    protected virtual void Start()
    {
        Debug.Log("BaseController started.");
    }

    // �� �����Ӹ��� ȣ��: �ൿ ó�� �� ȸ�� ó��
    protected virtual void Update()
    {
        // ���콺 ��ġ�� ȭ�鿡�� ���� ��ǥ�� ��ȯ
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = (mousePos - (Vector2)transform.position).normalized;

        // ���콺 ��Ŭ�� �� ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            Debug.Log("Mouse button down - Start attacking");
        }
        // ���콺 ��ư �� �� ���� ����
        if (Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            Debug.Log("Mouse button up - Stop attacking");
        }

        HandleAction();
        Rotate(lookDirection);
    }

    // ���� ������Ʈ: �̵� ó��
    protected virtual void FixedUpdate()
    {
        Move(movementDirection);
    }

    // ����� �Է��̳� AI ������ �����ϴ� �޼���
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

    // ���� �̵� ó�� (Shift Ű�� �ӵ� 2�� ó��)
    public void Move(Vector2 direction)
    {
        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f; // Shift Ű Ȯ��
        Vector2 finalDirection = direction * statHandler.Speed * speedMultiplier;
        _rigidbody.velocity = finalDirection;
        animationHandler.Move(finalDirection);
    }

    // ĳ���� �� ���� ȸ�� ó��
    public void Rotate(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ������ ������ ��ȯ
        bool isLeft = Mathf.Abs(rotZ) > 90f; // ������ ������ �Ǵ�
        characterRenderer.flipX = isLeft; // ĳ���� ������

        // ���� �ǹ��� ȸ��
        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }

    // ���� ó�� �޼���
    public virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
        {
            Debug.Log("Attack method called");
            weaponHandler?.Attack();
        }
    }

    // Trigger ������ ���� �� ȣ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BattleArea"))
        {
            if (weaponHandler == null)
            {
                if (WeaponPrefab != null)
                {
                    weaponHandler = Instantiate(WeaponPrefab, weaponPivot.position, Quaternion.identity);
                    weaponHandler.transform.SetParent(weaponPivot);  // �θ� ����
                    weaponHandler.gameObject.SetActive(true);        // ���� ������Ʈ Ȱ��ȭ
                }
                else
                {
                    weaponHandler = GetComponentInChildren<WeaponHandler>();
                }
            }
        }
    }
    // Trigger ������ ������ �� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BattleArea"))
        {
            if (weaponHandler != null)
            {
                Destroy(weaponHandler.gameObject);  // ���� ������Ʈ ����
                weaponHandler = null;               // ���۷��� �ʱ�ȭ
            }
        }
    }

}
