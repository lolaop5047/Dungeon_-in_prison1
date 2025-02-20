using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler //원거리 무기 WeaponHandler 상속
{

    [Header("Ranged Attack Data")] // 인스펙터에서 구분하기 위한 헤더

    [SerializeField] private Transform projectileSpawnPosition;// 투사체(총알 등)가 생성될 위치 (Transform)


    [SerializeField] private int bulletIndex; // 사용할 투사체의 종류를 결정하는 인덱스 (예: 여러 종류의 총알 중 어떤 것을 사용할지)

    public int BulletIndex { get { return bulletIndex; } } // 읽기 전용 프로퍼티

    [SerializeField] private float bulletSize = 1; // 투사체 크기 (기본값 1)
    public float BulletSize { get { return bulletSize; } } // 읽기 전용 프로퍼티

    [SerializeField] private float duration; // 투사체 존재하는 시간 (일정 시간 후 사라지는 타이머)
    public float Duration { get { return duration; } } // 읽기 전용 프로퍼티

    [SerializeField] private float spread; // 투사체가 퍼지는 정도 (샷건처럼 발사체가 퍼지는 효과)
    public float Spread { get { return spread; } } // 읽기 전용 프로퍼티

    [SerializeField] private int numberofProjectilesPerShot; // 한 번 발사할 때 나가는 투사체의 개수
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } } // 읽기 전용 프로퍼티

    [SerializeField] private float multipleProjectilesAngel; // 다중 투사체가 퍼지는 각도 (각 투사체 간 간격)
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } } // 읽기 전용 프로퍼티

    [SerializeField] private Color projectileColor; // 투사체(총알)의 색상
    public Color ProjectileColor { get { return projectileColor; } } // 읽기 전용 프로퍼티

    public override void Attack() // 공격 메서드 (부모 클래스의 Attack을 오버라이드)
    {
        base.Attack(); // 부모 클래스의 Attack() 실행 (기본 공격 기능 유지)

        float projectilesAngleSpace = multipleProjectilesAngel; // 투사체(발사체) 간의 각도 간격 설정
        int numberOfProjectilesPerShot = numberofProjectilesPerShot; // 한 번에 발사할 투사체 개수

        // 투사체들이 퍼지는 각도의 최소 시작값을 계산
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;

        // 투사체 개수만큼 반복하여 생성
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i; // 현재 투사체의 각도 계산
            float randomSpread = Random.Range(-spread, spread); // 랜덤한 퍼짐 효과 추가 (발사체가 약간씩 퍼지도록)
            angle += randomSpread; // 최종적으로 랜덤 효과 적용된 각도

            // 투사체 생성 메서드 호출 (현재 방향과 계산된 각도를 이용하여 투사체 발사)
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle) // 투사체를 생성하는 메서드
    {
        // lookDirection 방향으로 angle만큼 회전시켜 투사체를 발사하는 로직
    }

    // 벡터를 특정 각도로 회전시키는 함수
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v; // Quaternion을 이용하여 2D 벡터 회전, (v * Quaternion) 성립불가
    }

}

