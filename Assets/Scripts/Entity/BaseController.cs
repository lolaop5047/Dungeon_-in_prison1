using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } } // �̵��ϴ� ���� �ٶ󺸴� ���� ����

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f; //�˹鿡 ���� ����

    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;


    [SerializeField] public WeaponHandler WeaponPrefab;// �ν����Ϳ��� ������ �� �ִ� ���� ������ (WeaponHandler Ÿ��)

    protected WeaponHandler weaponHandler;// ���� ������ ���⸦ �����ϴ� ����
    protected bool isAttacking;// ���� ������ ���θ� ��Ÿ���� ���� (true�� ����)
    private float timeSinceLastAttack = float.MaxValue;// ������ ���� ���� ����� �ð� (���� �����̸� �����ϴ� �� ���)

    protected virtual void Awake()// ��ü�� ������ �� ȣ��Ǵ� �ʱ�ȭ �Լ�
    {
        _rigidbody = GetComponent<Rigidbody2D>();// Rigidbody2D ������Ʈ�� �����ͼ� ���� ó���� ���
        animationHandler = GetComponent<AnimationHandler>(); // �ִϸ��̼��� �����ϴ� AnimationHandler ������
        statHandler = GetComponent<StatHandler>();// ĳ������ �ɷ�ġ(ü��, ���ݷ� ��)�� �����ϴ� StatHandler ������
    
        if (WeaponPrefab != null)// ���� �������� �����Ǿ� ������ �ν��Ͻ� ���� �� weaponPivot�� ����
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            // ���� �������� ������ ���� �ڽ� ��ü �� WeaponHandler�� ã�� ���
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {

    }

    // �� �����Ӹ��� ȣ��Ǵ� �Լ� (���� ���� ������Ʈ)
    protected virtual void Update()
    {
        HandleAction(); // ĳ������ �ൿ(�̵�, ���� ��) ó��
        Rotate(lookDirection); // ĳ������ ������ ���콺/���� ���⿡ �°� ȸ��
        HandleAttackDelay(); // ���� ������ ����
    }

    protected virtual void FixedUpdate() //������ �ð� ���ݸ��� ȣ��, ���� ����(�̵�, ��, �浹 ��)
    {
        Movment(movementDirection); //knockbackDuration�� �˹�(ĳ���Ͱ� �з����� ȿ��) ���� �ð��� �ǹ��ϴ� ����
        if (knockbackDuration > 0.0f) //�ð��� �پ��ٰ� 0�� �Ǹ� �˹� ȿ���� ����ȴ�
        {
            knockbackDuration -= Time.fixedDeltaTime;  //Time.fixedDeltaTime: FixedUpdate()�� ȣ��� ������ ������ �ð� ���� ���ش�.
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction) //������ ����
    {
        direction = direction * statHandler.Speed; //�˹����� �� �� �̵� // ���Ȱ��� ���۳�Ʈ 
        if (knockbackDuration > 0.0f) //�˹�
        {
            direction *= 0.2f; //�˹� ������ �ؾ� �Ѵٸ� �ٿ��ش�.
            direction += knockback; //�˹��� �� ��ŭ
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction) // ȸ��
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Mathf.Atan2(y, x): ������ x, y ���� �̿��� ����(rad) ������ ȸ�� ������ ���Ѵ�. //rotz = ����ȸ�� ����
        bool isLeft = Mathf.Abs(rotZ) > 90f; // rotz�� 90�� �̻��̸� ������ ���ٴ� �ǹ�

        characterRenderer.flipX = isLeft; // flipX = true�̸� �̹����� �¿� �����Ͽ� ĳ���Ͱ� ������ ���� �����, flipX = false�� �⺻ ����(������)�� ����

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); //���콺�� ����Ű�� ������ �� ���Ⱑ �ش� ������ �ٶ󺸵��� ȸ��
        }
        weaponHandler?.Rotate(isLeft);
    }

    public void ApplyKnockback(Transform other, float power, float duration) //�˹��� �󸶳� �󸶸�ŭ �����Ұ���
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power; // ������ ���̸� 1������ ����� �ش�.
    }

    private void HandleAttackDelay() //������
    {
        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }

}




