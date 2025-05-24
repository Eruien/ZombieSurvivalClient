using System;

// 애니메이션과 관련된 변수를 컨트롤
public class AnimationManager
{
    // 군인의 공격 스피드 조절
    private float _ArmyAttackSpeed = 0.0f;
    public float ArmyAttackSpeed
    {
        get { return _ArmyAttackSpeed; }
        set
        {
            _ArmyAttackSpeed = value;
            observerArmyAttackSpeed.Invoke(_ArmyAttackSpeed);
        }
    }

    // 군인의 공격 스피드가 외부에서 조절 될 때 알기 위해서
    public Action<float> observerArmyAttackSpeed;

    // 처음 초기화
    public void Init()
    {
        _ArmyAttackSpeed = Managers.Data.objectDict["Army"].attackSpeed;
    }

    // 씬이 다시 로드 될 때 초기화
    public void Clear()
    {
        _ArmyAttackSpeed = Managers.Data.objectDict["Army"].attackSpeed;
        observerArmyAttackSpeed = null;
    }
}
