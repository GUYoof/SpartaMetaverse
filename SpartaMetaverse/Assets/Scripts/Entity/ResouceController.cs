using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResouceController : MonoBehaviour
{
    // 체력 변경 쿨타임 
    [SerializeField] private float healthChangeDelay = 0.5f;

    // 다른 컴포넌트 참조용
    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    // 마지막 체력 변경 후 지난 시간 추적
    private float timeSinceLastChange = float.MaxValue;

    // 현재 체력 
    public float CurrentHealth { get; private set; }

    // 최대 체력
    public float Maxhealth => statHandler.Health;

    private void Awake()
    {
        // 같은 오브젝트에 붙은 컴포넌트들을 가져옴
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        // 게임 시작 시 현재 체력을 최대 체력으로 초기화
        CurrentHealth = statHandler.Health;
    }

    private void Update()
    {
        // 체력 변경 쿨타임이 끝날 때까지 시간 증가
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;

            // 쿨타임이 끝났으면 애니메이션에 무적 상태 해제 호출
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InincibilityEnd();
            }
        }
    }

    // 체력을 변경하는 메서드 (음수면 데미지, 양수면 회복)
    public bool changeHealth(float change)
    {
        // 변경이 0이거나 쿨타임 중이면 무시
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        // 체력 변경을 했으니 타이머 초기화
        timeSinceLastChange = 0f;

        // 체력 변경 적용
        CurrentHealth += change;

        // 최대 체력을 넘지 않게, 최소 0으로 제한
        CurrentHealth = CurrentHealth > Maxhealth ? Maxhealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        // 데미지를 받았으면 데미지 애니메이션 실행
        if (change < 0)
        {
            animationHandler.Damage();
        }

        // 체력이 0 이하라면 죽음 처리 호출
        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    // 캐릭터가 죽었을 때 호출 
    private void Death()
    {

    }
}
