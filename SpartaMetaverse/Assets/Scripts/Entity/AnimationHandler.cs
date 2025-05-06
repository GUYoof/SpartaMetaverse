using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    // "IsMove"와 "IsDamage" 파라미터의 해시 값을 미리 계산하여 캐싱, 애니메이션 상태를 제어하는 데 사용
    private static readonly int IsMoving = Animator.StringToHash("IsMove"); // 이동 상태를 나타내는 파라미터
    private static readonly int IsDamage = Animator.StringToHash("IsDamage"); // 피해 상태를 나타내는 파라미터

    protected Animator animator; // 애니메이터 컴포넌트를 참조하는 변수

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>(); // 자식 오브젝트에서 애니메이터 검색
    }

    // 이동에 따른 애니메이션 상태를 변경하는 함수
    public void Move(Vector2 obj)
    {
        // 이동이 일정 크기 이상일 때만 "IsMove"를 true로 설정
        animator.SetBool(IsMoving, obj.magnitude > 0.5f); // obj.magnitude는 벡터의 크기 (이동의 크기)
    }

    // 피해를 받았을 때 애니메이션 상태를 변경하는 함수
    public void Damage()
    {
        animator.SetBool(IsDamage, true); // 피해를 받았을 때 "IsDamage" 파라미터를 true로 설정
    }

    // 무적 시간이 끝났을 때 피해 상태를 종료하는 함수
    public void InincibilityEnd()
    {
        animator.SetBool(IsDamage, false); // "IsDamage"를 false로 설정하여 피해 애니메이션을 종료
    }
}
