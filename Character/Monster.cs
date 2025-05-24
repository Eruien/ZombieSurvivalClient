
// 플레이어의 적과 관련된 클래스
using static UnityEngine.GraphicsBuffer;

public class Monster : BaseCharacter
{
    protected override void Awake()
    {
        base.Awake();
        // 시민과 몬스터로 구분해서 메모리에 추가
        Managers.Memory.memoryList[nameof(Monster)].Add(this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // 죽었을 때 돈 드랍
        deathObserver += DeathDropMoney;
    }

    public override void ChangeTarget()
    {
        BaseObject obj = Managers.Memory.GetNearEnemy("Citizen", this);

        if (obj == null) return;

        Target = obj.gameObject;
    }

    // 죽었을 때 돈 드랍 함수
    public virtual void DeathDropMoney()
    {
        
    }
}
