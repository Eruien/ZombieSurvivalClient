using System;

// �ִϸ��̼ǰ� ���õ� ������ ��Ʈ��
public class AnimationManager
{
    // ������ ���� ���ǵ� ����
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

    // ������ ���� ���ǵ尡 �ܺο��� ���� �� �� �˱� ���ؼ�
    public Action<float> observerArmyAttackSpeed;

    // ó�� �ʱ�ȭ
    public void Init()
    {
        _ArmyAttackSpeed = Managers.Data.objectDict["Army"].attackSpeed;
    }

    // ���� �ٽ� �ε� �� �� �ʱ�ȭ
    public void Clear()
    {
        _ArmyAttackSpeed = Managers.Data.objectDict["Army"].attackSpeed;
        observerArmyAttackSpeed = null;
    }
}
