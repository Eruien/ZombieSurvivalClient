
// �庮, ���ε� �÷��̾� �� Ŭ����
public class Citizen : BaseCharacter
{
    protected override void Awake()
    {
        base.Awake();
        // �ùΰ� ���ͷ� �����ؼ� �޸𸮿� �߰�
        Managers.Memory.memoryList[nameof(Citizen)].Add(this);
    }

    public override void ChangeTarget()
    {
        BaseObject obj = Managers.Memory.GetNearEnemy("Monster", this);

        if (obj == null) return;

        Target = obj.gameObject;
    }
}
