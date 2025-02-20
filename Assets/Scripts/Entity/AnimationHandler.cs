using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour //MonoBehaviour�� ��ӹ޾����Ƿ� Unity�� ���� ������Ʈ�� ���� �� �ִ� ��ũ��Ʈ
{                                             //�� �ڵ�� �ִϸ��̼��� �����ϴ� AnimationHandler Ŭ����
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");

    protected Animator animator;

    protected virtual void Awake() //Awake()�� Unity���� ��ü�� ������ �� �ڵ����� ����Ǵ� �Լ�
    {
        animator = GetComponentInChildren<Animator>(); //�� Ŭ������ �ִϸ��̼��� ������ Animator�� �ʱ�ȭ�ϴ� ������ ��
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f); //�̵� �ӵ��� 0.5���� ũ�� IsMove�� true�� ���� �� �ִϸ��̼� ����
    }                                                    //�̵� �ӵ��� 0.5 �����̸� false �� ���� �ִϸ��̼�

    public void Damage()
    {
        animator.SetBool(IsDamage, true); //sDamage�� true�� ���� �� ������ �Դ� �ִϸ��̼� ����
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false); //IsDamage�� false�� ���� �� ������ ���� ���� �ִϸ��̼� ����
    }
}


