using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f; //체력 변화간 딜레이 시간 0.5 //0.5초 동안 추가적인 체력 변화를 방지(무적)

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue; //딜레이 없이 체력이 변할 수 있도록 설정

    public float CurrentHealth { get; private set; } //CurrentHealth → 현재 체력 (private set; 이므로 외부에서 변경안됨.
    public float MaxHealth => statHandler.Health; //MaxHealth → 최대 체력 (StatHandler에서 가져옴).

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health; //게임이 시작될 때 체력 초기화.
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay) //무적상태 해제 타이머
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay) //imeSinceLastChange가 healthChangeDelay(0.5초)를 넘으면 무적 해제
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth; //체력이 MaxHealth를 넘으면 MaxHealth로 설정 //최대 체력 초과방지
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change < 0) // 체력 0보다 작을 시 animationHandler.Damage(); 실행
        {
            animationHandler.Damage();

        }

        if (CurrentHealth <= 0f) //사망체크
        {
            Death();
        }

        return true;
    }

    private void Death()
    {

    }

}
