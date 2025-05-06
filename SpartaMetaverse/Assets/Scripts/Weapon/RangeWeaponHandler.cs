using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    // 원거리 공격 관련 데이터
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;  // 발사체가 생성될 위치
    [field: SerializeField] public int BulletIndex { get; private set; }  // 총알 인덱스
    [field: SerializeField] public float BulletSize { get; private set; } = 1f;  // 총알 크기
    [field: SerializeField] public float Duration { get; private set; }  // 발사체 지속 시간
    [field: SerializeField] public float Spread { get; private set; }  // 총알 퍼짐 정도
    [field: SerializeField] public int NumberOfProjectilesPerShot { get; private set; }  // 한 번의 공격에 발사되는 총알 수
    [field: SerializeField] public float MultipleProjectileAngle { get; private set; }  // 다수의 총알 발사 시 각도 간격
    [field: SerializeField] public Color ProjectileColor { get; private set; }  // 발사체의 색상
    [field: SerializeField] public float BulletSpeed { get; private set; } = 10f;  // 총알 속도

    private ProjectileManager projectileManager;  // 발사체 관리 매니저 (싱글톤 인스턴스)

    // 클래스 초기화 메서드 (Start)
    protected override void Start()
    {
        base.Start();  // 부모 클래스의 Start() 호출
        projectileManager = ProjectileManager.Instance;  // 발사체 관리 매니저 인스턴스 가져오기
    }

    // 공격 메서드 (원거리 공격 처리)
    public override void Attack()
    {
        base.Attack();  // 부모의 Attack() 호출

        // 발사될 총알들의 최소 각도 설정 (총알 수에 따라 각도 범위 계산)
        float minAngle = -(NumberOfProjectilesPerShot / 2f) * MultipleProjectileAngle;

        // 총알 수만큼 반복하여 발사체를 생성
        for (int i = 0; i < NumberOfProjectilesPerShot; i++)
        {
            // 각도 계산
            float angle = minAngle + MultipleProjectileAngle * i;
            angle += Random.Range(-Spread, Spread);  // 퍼짐 정도를 랜덤으로 적용

            // 각도를 포함하여 발사체를 생성
            projectileManager.ShootBullet(this, projectileSpawnPosition.position, Controller.LookDirection, angle);  // angle을 추가
        }
    }


    // 발사체를 생성하는 메서드
    private void CreateProjectile(Vector2 lookDirection, float angle)
    {
        // 주어진 각도로 방향을 회전시킴
        Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * lookDirection;

        // 총알 발사 
        projectileManager.ShootBullet(this, projectileSpawnPosition.position, rotatedDirection, angle);  // angle을 추가로 전달
    }

}
