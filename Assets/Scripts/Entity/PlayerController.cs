using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction() //�÷��̾��� �Է�(Ű���� & ���콺)�� �޾Ƽ� �̵� ����(movementDirection)�� �ٶ󺸴� ����(lookDirection)�� �����ϴ� ������ �Ѵ�.
    {
        // Ű���� �Է��� �޾� ����(horizontal) �� ����(vertical) ���� ���� ������
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(horizontal, vertical).normalized; // �̵� ������ 2D ���ͷ� �����ϰ� ����ȭ�Ͽ� ������ �ӵ��� �̵��ϵ��� ��

        Vector2 mousePosition = Input.mousePosition; // ���콺 Ŀ���� ���� ȭ�� ��ǥ�� ������

        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition); // ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ

        lookDirection = (worldPos - (Vector2)transform.position); // ��ü�� ��ġ���� ���콺 ��ǥ������ ���� ���͸� ���

        // �Ÿ��� 0.9 �̸��̸� ���� ���͸� (0,0)���� �����Ͽ� �ʹ� ����� �� ȸ������ �ʵ��� ��
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            // ���� ���͸� ����ȭ�Ͽ� ������ �������� ȸ���ϵ��� ����
            lookDirection = lookDirection.normalized;
        }
        isAttacking = Input.GetMouseButton(0);
    }

}

