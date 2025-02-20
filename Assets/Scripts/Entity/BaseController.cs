using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } } // 이동하는 방향 바라보는 방향 지정

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f; //넉백에 대한 방향

    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;


    [SerializeField] public WeaponHandler WeaponPrefab;// 인스펙터에서 설정할 수 있는 무기 프리팹 (WeaponHandler 타입)

    protected WeaponHandler weaponHandler;// 현재 장착된 무기를 참조하는 변수
    protected bool isAttacking;// 공격 중인지 여부를 나타내는 변수 (true면 공격)
    private float timeSinceLastAttack = float.MaxValue;// 마지막 공격 이후 경과한 시간 (공격 딜레이를 조절하는 데 사용)

    protected virtual void Awake()// 객체가 생성될 때 호출되는 초기화 함수
    {
        _rigidbody = GetComponent<Rigidbody2D>();// Rigidbody2D 컴포넌트를 가져와서 물리 처리를 담당
        animationHandler = GetComponent<AnimationHandler>(); // 애니메이션을 관리하는 AnimationHandler 가져옴
        statHandler = GetComponent<StatHandler>();// 캐릭터의 능력치(체력, 공격력 등)를 관리하는 StatHandler 가져옴
    
        if (WeaponPrefab != null)// 무기 프리팹이 설정되어 있으면 인스턴스 생성 후 weaponPivot에 장착
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            // 무기 프리팹이 없으면 기존 자식 객체 중 WeaponHandler를 찾아 사용
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {

    }

    // 매 프레임마다 호출되는 함수 (게임 로직 업데이트)
    protected virtual void Update()
    {
        HandleAction(); // 캐릭터의 행동(이동, 공격 등) 처리
        Rotate(lookDirection); // 캐릭터의 방향을 마우스/조작 방향에 맞게 회전
        HandleAttackDelay(); // 공격 딜레이 관리
    }

    protected virtual void FixedUpdate() //고정된 시간 간격마다 호출, 물리 연산(이동, 힘, 충돌 등)
    {
        Movment(movementDirection); //knockbackDuration은 넉백(캐릭터가 밀려나는 효과) 지속 시간을 의미하는 변수
        if (knockbackDuration > 0.0f) //시간이 줄어들다가 0이 되면 넉백 효과가 종료된다
        {
            knockbackDuration -= Time.fixedDeltaTime;  //Time.fixedDeltaTime: FixedUpdate()가 호출될 때마다 일정한 시간 값을 빼준다.
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movment(Vector2 direction) //움직임 방향
    {
        direction = direction * statHandler.Speed; //넉백으로 인 한 이동 // 스탯관리 컴퍼넌트 
        if (knockbackDuration > 0.0f) //넉백
        {
            direction *= 0.2f; //넉백 적용을 해야 한다면 줄여준다.
            direction += knockback; //넉백의 힘 만큼
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction) // 회전
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Mathf.Atan2(y, x): 벡터의 x, y 값을 이용해 라디안(rad) 단위의 회전 각도를 구한다. //rotz = 방향회전 각도
        bool isLeft = Mathf.Abs(rotZ) > 90f; // rotz가 90도 이상이면 왼쪽을 본다는 의미

        characterRenderer.flipX = isLeft; // flipX = true이면 이미지를 좌우 반전하여 캐릭터가 왼쪽을 보게 만든다, flipX = false면 기본 상태(오른쪽)로 유지

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); //마우스나 방향키를 움직일 때 무기가 해당 방향을 바라보도록 회전
        }
        weaponHandler?.Rotate(isLeft);
    }

    public void ApplyKnockback(Transform other, float power, float duration) //넉백을 얼마나 얼마만큼 적용할건지
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power; // 백터의 길이를 1값으로 만들어 준다.
    }

    private void HandleAttackDelay() //딜레이
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




