
// �� ���� ���� Ŭ����
public class DemonCreature : Monster
{
    // ����Ƽ �Լ�
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        ChangeTarget();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }

    // �Ϲ� �Լ�

    // �׾��� �� �� ��� �Լ�
    public override void DeathDropMoney()
    {
        Managers.Item.CurrentMoney += UnityEngine.Random.Range(300, 500);
    }

    protected override void OnChildHitEvent() { }
}
