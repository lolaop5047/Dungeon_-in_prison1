using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler //���Ÿ� ���� WeaponHandler ���
{

    [Header("Ranged Attack Data")] // �ν����Ϳ��� �����ϱ� ���� ���

    [SerializeField] private Transform projectileSpawnPosition;// ����ü(�Ѿ� ��)�� ������ ��ġ (Transform)


    [SerializeField] private int bulletIndex; // ����� ����ü�� ������ �����ϴ� �ε��� (��: ���� ������ �Ѿ� �� � ���� �������)

    public int BulletIndex { get { return bulletIndex; } } // �б� ���� ������Ƽ

    [SerializeField] private float bulletSize = 1; // ����ü ũ�� (�⺻�� 1)
    public float BulletSize { get { return bulletSize; } } // �б� ���� ������Ƽ

    [SerializeField] private float duration; // ����ü �����ϴ� �ð� (���� �ð� �� ������� Ÿ�̸�)
    public float Duration { get { return duration; } } // �б� ���� ������Ƽ

    [SerializeField] private float spread; // ����ü�� ������ ���� (����ó�� �߻�ü�� ������ ȿ��)
    public float Spread { get { return spread; } } // �б� ���� ������Ƽ

    [SerializeField] private int numberofProjectilesPerShot; // �� �� �߻��� �� ������ ����ü�� ����
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } } // �б� ���� ������Ƽ

    [SerializeField] private float multipleProjectilesAngel; // ���� ����ü�� ������ ���� (�� ����ü �� ����)
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } } // �б� ���� ������Ƽ

    [SerializeField] private Color projectileColor; // ����ü(�Ѿ�)�� ����
    public Color ProjectileColor { get { return projectileColor; } } // �б� ���� ������Ƽ

    public override void Attack() // ���� �޼��� (�θ� Ŭ������ Attack�� �������̵�)
    {
        base.Attack(); // �θ� Ŭ������ Attack() ���� (�⺻ ���� ��� ����)

        float projectilesAngleSpace = multipleProjectilesAngel; // ����ü(�߻�ü) ���� ���� ���� ����
        int numberOfProjectilesPerShot = numberofProjectilesPerShot; // �� ���� �߻��� ����ü ����

        // ����ü���� ������ ������ �ּ� ���۰��� ���
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;

        // ����ü ������ŭ �ݺ��Ͽ� ����
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i; // ���� ����ü�� ���� ���
            float randomSpread = Random.Range(-spread, spread); // ������ ���� ȿ�� �߰� (�߻�ü�� �ణ�� ��������)
            angle += randomSpread; // ���������� ���� ȿ�� ����� ����

            // ����ü ���� �޼��� ȣ�� (���� ����� ���� ������ �̿��Ͽ� ����ü �߻�)
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle) // ����ü�� �����ϴ� �޼���
    {
        // lookDirection �������� angle��ŭ ȸ������ ����ü�� �߻��ϴ� ����
    }

    // ���͸� Ư�� ������ ȸ����Ű�� �Լ�
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v; // Quaternion�� �̿��Ͽ� 2D ���� ȸ��, (v * Quaternion) �����Ұ�
    }

}

