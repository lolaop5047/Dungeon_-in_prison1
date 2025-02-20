using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour //FollowCamera Ŭ������ MonoBehaviour�� ��ӹ��� �� Unity���� ������Ʈ�� ������ũ��
{
    public Transform target;
    float offsetX; //�ʱ� ī�޶� ��ġ�� ���(target) ������ X�� �Ÿ� ���̸� �����ϴ� ����.
    float offsetY;

    void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x; //���� ī�޶��� X ��ġ���� ����� X ��ġ�� �� ���� offsetX�� ����.
        offsetY = transform.position.y - target.position.y;
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        pos.y = target.position.y + offsetY;
        transform.position = pos;
    }
}
