
// �÷��̾��� ���� ���õ� Ŭ����
using static UnityEngine.GraphicsBuffer;

public class Monster : BaseCharacter
{
    protected override void Awake()
    {
        base.Awake();
        // �ùΰ� ���ͷ� �����ؼ� �޸𸮿� �߰�
        Managers.Memory.memoryList[nameof(Monster)].Add(this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // �׾��� �� �� ���
        deathObserver += DeathDropMoney;
    }

    public override void ChangeTarget()
    {
        BaseObject obj = Managers.Memory.GetNearEnemy("Citizen", this);

        if (obj == null) return;

        Target = obj.gameObject;
    }

    // �׾��� �� �� ��� �Լ�
    public virtual void DeathDropMoney()
    {
        
    }
}
