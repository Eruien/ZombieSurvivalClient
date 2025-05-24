
// 적 보스 몬스터 클래스
public class DemonCreature : Monster
{
    // 유니티 함수
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

    // 일반 함수

    // 죽었을 때 돈 드랍 함수
    public override void DeathDropMoney()
    {
        Managers.Item.CurrentMoney += UnityEngine.Random.Range(300, 500);
    }

    protected override void OnChildHitEvent() { }
}
