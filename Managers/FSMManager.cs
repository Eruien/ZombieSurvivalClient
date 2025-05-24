using Assets.Scripts;
using System.Collections.Generic;

// FSM ���� Ŭ����
public class FSMManager
{
    // FSM ���� �ൿ�� ������ ��ųʸ�
    public Dictionary<(State, Trigger), State> stateTransitionDict = new Dictionary<(State, Trigger), State>();

    // FSM �ൿ�� ���� �߰�
    public void Init()
    {
        stateTransitionDict.Add((State.Move, Trigger.InAttackDistance), State.Attack);
        stateTransitionDict.Add((State.Attack, Trigger.OutAttackDistance), State.Move);
    }

    // FSM ���� ���¿��� Ʈ���ſ� ���� ���� �ൿ�� ������
    public State Transition(State currentState, Trigger trigger)
    {
        return stateTransitionDict[(currentState, trigger)];
    }
}
