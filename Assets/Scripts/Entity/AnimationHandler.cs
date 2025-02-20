using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour //MonoBehaviour를 상속받았으므로 Unity의 게임 오브젝트에 붙일 수 있는 스크립트
{                                             //이 코드는 애니메이션을 제어하는 AnimationHandler 클래스
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator animator;

    protected virtual void Awake() //Awake()는 Unity에서 객체가 생성될 때 자동으로 실행되는 함수
    {
        animator = GetComponentInChildren<Animator>(); //이 클래스는 애니메이션을 제어할 Animator를 초기화하는 역할을 함
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f); //이동 속도가 0.5보다 크면 IsMove를 true로 설정 → 애니메이션 실행
    }                                                    //이동 속도가 0.5 이하이면 false → 정지 애니메이션

    public void Damage()
    {
        animator.SetBool(IsDamage, true); //sDamage를 true로 설정 → 데미지 입는 애니메이션 실행
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false); //IsDamage를 false로 설정 → 데미지 상태 종료 애니메이션 실행
    }
}


