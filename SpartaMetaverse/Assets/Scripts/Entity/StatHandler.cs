using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // 체력을 1에서 100 사이로 설정할 수 있는 변수
    [Range(1, 100)][SerializeField] private int health = 10; // Unity의 인스펙터에서 값을 슬라이더로 조정

    // Health 프로퍼티는 health 변수에 대한 접근자
    public int Health
    { 
        get => health; // health 값을 반환
        set => health = Mathf.Clamp(value, 0, 100); // value를 0과 100 사이로 제한하여 설정
    }

    // 속도를 1f에서 20f 사이로 설정할 수 있는 변수
    [Range(1f, 20f)][SerializeField] private float speed = 3; 

    // Speed 프로퍼티는 speed 변수에 대한 접근자
    public float Speed
    {
        get => speed; // speed 값을 반환
        set => speed = Mathf.Clamp(value, 0, 20); // value를 0과 20 사이로 제한하여 설정
    }
}   

