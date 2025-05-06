using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ� (������ ��ü�θ� ���� ����)
    public static ProjectileManager Instance { get; private set; }

    // �߻�ü ������ �迭 (�پ��� ������ �߻�ü �������� ����)
    [SerializeField] private GameObject[] projectilePrefabs;

    // Awake()�� ��ü �ʱ�ȭ �� ȣ��Ǵ� �޼���
    private void Awake()
    {
        // �̹� �ٸ� �ν��Ͻ��� �����ϸ� ���� ��ü�� �ı�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �̱��� �ν��Ͻ� ���� (ù ��° �ν��Ͻ��� ����)
        Instance = this;
    }

    // �Ѿ� �߻� �޼���
    public void ShootBullet(RangeWeaponHandler weapon, Vector2 startPos, Vector2 direction, float angle)
    {
        // �־��� ������ ������ ȸ����Ŵ
        Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * direction;

        // ���⿡ �´� �߻�ü ����
        GameObject prefab = projectilePrefabs[weapon.BulletIndex];

        // �߻�ü �������� ���� ��ġ�� �ν��Ͻ�ȭ
        GameObject obj = Instantiate(prefab, startPos, Quaternion.identity);

        // ������ �߻�ü�� ProjectileController�� ������ �ʱ�ȭ
        if (obj.TryGetComponent(out ProjectileController controller))
        {
            controller.Init(rotatedDirection, weapon, this);  // ����, ����, �Ŵ����� ����
        }
        else
        {
            Debug.LogError("Projectile prefab missing ProjectileController!");
        }
    }

}
