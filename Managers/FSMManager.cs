using Assets.Scripts;
using System.Collections.Generic;

// FSM 관리 클래스
public class FSMManager
{
    // FSM 다음 행동을 저장한 딕셔너리
    public Dictionary<(State, Trigger), State> stateTransitionDict = new Dictionary<(State, Trigger), State>();

    // FSM 행동을 전부 추가
    public void Init()
    {
        stateTransitionDict.Add((State.Move, Trigger.InAttackDistance), State.Attack);
        stateTransitionDict.Add((State.Attack, Trigger.OutAttackDistance), State.Move);
    }

    // FSM 현재 상태에서 트리거에 따라 다음 행동을 가져옴
    public State Transition(State currentState, Trigger trigger)
    {
        return stateTransitionDict[(currentState, trigger)];
    }
}
