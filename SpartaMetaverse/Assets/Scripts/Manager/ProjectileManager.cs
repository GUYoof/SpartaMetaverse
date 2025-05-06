using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // 싱글톤 인스턴스 (유일한 객체로만 접근 가능)
    public static ProjectileManager Instance { get; private set; }

    // 발사체 프리팹 배열 (다양한 종류의 발사체 프리팹을 관리)
    [SerializeField] private GameObject[] projectilePrefabs;

    // Awake()는 객체 초기화 시 호출되는 메서드
    private void Awake()
    {
        // 이미 다른 인스턴스가 존재하면 현재 객체를 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 싱글톤 인스턴스 설정 (첫 번째 인스턴스만 남음)
        Instance = this;
    }

    // 총알 발사 메서드
    public void ShootBullet(RangeWeaponHandler weapon, Vector2 startPos, Vector2 direction, float angle)
    {
        // 주어진 각도로 방향을 회전시킴
        Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * direction;

        // 무기에 맞는 발사체 선택
        GameObject prefab = projectilePrefabs[weapon.BulletIndex];

        // 발사체 프리팹을 시작 위치에 인스턴스화
        GameObject obj = Instantiate(prefab, startPos, Quaternion.identity);

        // 생성된 발사체에 ProjectileController가 있으면 초기화
        if (obj.TryGetComponent(out ProjectileController controller))
        {
            controller.Init(rotatedDirection, weapon, this);  // 방향, 무기, 매니저를 설정
        }
        else
        {
            Debug.LogError("Projectile prefab missing ProjectileController!");
        }
    }

}
