using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour //FollowCamera 클래스는 MonoBehaviour를 상속받음 → Unity에서 오브젝트에 부착스크립
{
    public Transform target;
    float offsetX; //초기 카메라 위치와 대상(target) 사이의 X축 거리 차이를 저장하는 변수.
    float offsetY;

    void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x; //현재 카메라의 X 위치에서 대상의 X 위치를 뺀 값을 offsetX에 저장.
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
