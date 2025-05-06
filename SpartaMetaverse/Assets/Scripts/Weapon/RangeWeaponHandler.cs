using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    // ���Ÿ� ���� ���� ������
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;  // �߻�ü�� ������ ��ġ
    [field: SerializeField] public int BulletIndex { get; private set; }  // �Ѿ� �ε���
    [field: SerializeField] public float BulletSize { get; private set; } = 1f;  // �Ѿ� ũ��
    [field: SerializeField] public float Duration { get; private set; }  // �߻�ü ���� �ð�
    [field: SerializeField] public float Spread { get; private set; }  // �Ѿ� ���� ����
    [field: SerializeField] public int NumberOfProjectilesPerShot { get; private set; }  // �� ���� ���ݿ� �߻�Ǵ� �Ѿ� ��
    [field: SerializeField] public float MultipleProjectileAngle { get; private set; }  // �ټ��� �Ѿ� �߻� �� ���� ����
    [field: SerializeField] public Color ProjectileColor { get; private set; }  // �߻�ü�� ����
    [field: SerializeField] public float BulletSpeed { get; private set; } = 10f;  // �Ѿ� �ӵ�

    private ProjectileManager projectileManager;  // �߻�ü ���� �Ŵ��� (�̱��� �ν��Ͻ�)

    // Ŭ���� �ʱ�ȭ �޼��� (Start)
    protected override void Start()
    {
        base.Start();  // �θ� Ŭ������ Start() ȣ��
        projectileManager = ProjectileManager.Instance;  // �߻�ü ���� �Ŵ��� �ν��Ͻ� ��������
    }

    // ���� �޼��� (���Ÿ� ���� ó��)
    public override void Attack()
    {
        base.Attack();  // �θ��� Attack() ȣ��

        // �߻�� �Ѿ˵��� �ּ� ���� ���� (�Ѿ� ���� ���� ���� ���� ���)
        float minAngle = -(NumberOfProjectilesPerShot / 2f) * MultipleProjectileAngle;

        // �Ѿ� ����ŭ �ݺ��Ͽ� �߻�ü�� ����
        for (int i = 0; i < NumberOfProjectilesPerShot; i++)
        {
            // ���� ���
            float angle = minAngle + MultipleProjectileAngle * i;
            angle += Random.Range(-Spread, Spread);  // ���� ������ �������� ����

            // ������ �����Ͽ� �߻�ü�� ����
            projectileManager.ShootBullet(this, projectileSpawnPosition.position, Controller.LookDirection, angle);  // angle�� �߰�
        }
    }


    // �߻�ü�� �����ϴ� �޼���
    private void CreateProjectile(Vector2 lookDirection, float angle)
    {
        // �־��� ������ ������ ȸ����Ŵ
        Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * lookDirection;

        // �Ѿ� �߻� 
        projectileManager.ShootBullet(this, projectileSpawnPosition.position, rotatedDirection, angle);  // angle�� �߰��� ����
    }

}
