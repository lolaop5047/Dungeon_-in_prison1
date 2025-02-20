using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; } //무기크기

    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; } //공격력

    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; } //공격 속도

    [SerializeField] private float attackRange = 10f; //공격 범위
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target; //공격대상 설정 

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false; //넉백
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f; //넉백 거리
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f; //넉백 시간
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack"); //불러오기

    public BaseController Controller { get; private set; }

    private Animator animator;
    private SpriteRenderer weaponRenderer;

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }


}

