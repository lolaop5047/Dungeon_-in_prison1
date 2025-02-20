using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f; //ü�� ��ȭ�� ������ �ð� 0.5 //0.5�� ���� �߰����� ü�� ��ȭ�� ����(����)

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue; //������ ���� ü���� ���� �� �ֵ��� ����

    public float CurrentHealth { get; private set; } //CurrentHealth �� ���� ü�� (private set; �̹Ƿ� �ܺο��� ����ȵ�.
    public float MaxHealth => statHandler.Health; //MaxHealth �� �ִ� ü�� (StatHandler���� ������).

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health; //������ ���۵� �� ü�� �ʱ�ȭ.
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay) //�������� ���� Ÿ�̸�
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay) //imeSinceLastChange�� healthChangeDelay(0.5��)�� ������ ���� ����
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
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth; //ü���� MaxHealth�� ������ MaxHealth�� ���� //�ִ� ü�� �ʰ�����
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change < 0) // ü�� 0���� ���� �� animationHandler.Damage(); ����
        {
            animationHandler.Damage();

        }

        if (CurrentHealth <= 0f) //���üũ
        {
            Death();
        }

        return true;
    }

    private void Death()
    {

    }

}
