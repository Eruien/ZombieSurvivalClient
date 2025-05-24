
// 장벽, 군인등 플레이어 편 클래스
public class Citizen : BaseCharacter
{
    protected override void Awake()
    {
        base.Awake();
        // 시민과 몬스터로 구분해서 메모리에 추가
        Managers.Memory.memoryList[nameof(Citizen)].Add(this);
    }

    public override void ChangeTarget()
    {
        BaseObject obj = Managers.Memory.GetNearEnemy("Monster", this);

        if (obj == null) return;

        Target = obj.gameObject;
    }
}
