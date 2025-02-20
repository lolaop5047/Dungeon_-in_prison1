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

    protected override void HandleAction() //플레이어의 입력(키보드 & 마우스)을 받아서 이동 방향(movementDirection)과 바라보는 방향(lookDirection)을 설정하는 역할을 한다.
    {
        // 키보드 입력을 받아 수평(horizontal) 및 수직(vertical) 방향 값을 가져옴
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(horizontal, vertical).normalized; // 이동 방향을 2D 벡터로 설정하고 정규화하여 일정한 속도로 이동하도록 함

        Vector2 mousePosition = Input.mousePosition; // 마우스 커서의 현재 화면 좌표를 가져옴

        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition); // 마우스 좌표를 월드 좌표로 변환

        lookDirection = (worldPos - (Vector2)transform.position); // 객체의 위치에서 마우스 좌표까지의 방향 벡터를 계산

        // 거리가 0.9 미만이면 방향 벡터를 (0,0)으로 설정하여 너무 가까울 때 회전하지 않도록 함
        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            // 방향 벡터를 정규화하여 일정한 방향으로 회전하도록 설정
            lookDirection = lookDirection.normalized;
        }
        isAttacking = Input.GetMouseButton(0);
    }

}

