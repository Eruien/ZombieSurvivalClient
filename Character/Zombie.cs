
// 기본적인 적 클래스
using Assets.Scripts;

public class Zombie : Monster
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
        Managers.Item.CurrentMoney += UnityEngine.Random.Range(50, 100);
    }

    protected override void OnChildHitEvent()
    {
       
    }
}
