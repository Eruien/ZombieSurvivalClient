# ZombieSurvival
* 플레이 영상 : <https://youtu.be/WEAWCcp3GR4>
* 다운로드 : <https://naver.me/GJ5cFw67>
  
<img src="Image/ZombieSurvival.png" width="600" height="350"/>

***

* 작업 기간 : 2025. 05. 01 ~ 2025. 05. 24 (1달)
* 인력 구성 : 1명
* 사용언어 및 개발환경 : C#, Unity
  
# FSM
* 복잡한 상태 전환을 명확히 관리하기 위하여 FSM 사용
***
  <table>
  <tr>
    <th>Idle</th>
    <th>Walk</th>
    <th>Fire</th>
    <th>Death</th>
 </tr>
  <tr>
    <td><img src="Image/Army_Idle.png" width="150" height="150"/></td>
    <td><img src="Image/Army_Walk.png" width="150" height="150"/></td>
    <td><img src="Image/Army_Fire.png" width="150" height="150"/></td>
    <td><img src="Image/Army_Death.png" width="150" height="150"/></td>
</tr>
</table>

<details>
<summary>FSM 코드</summary>
	
```cs
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


// 움직이는 캐릭터들이 상속 받을 클래스
public class BaseCharacter : BaseObject
{
    // FSM에서 현재 상태
    private State _CurrentState = State.Move;
    public State CurrentState { get { return _CurrentState; } }

    // 현재 상태와 트리거를 기반으로 다음 상태를 가져옴
    public void SetCurrentState(Trigger trigger)
    {
        _CurrentState = stateObserver.Invoke(_CurrentState, trigger);
        CurrentAction = stateActionDict[_CurrentState];
    }

    // 여기서 초기화
    // 상태에 따른 행동 딕셔너리
    private Dictionary<State, Action> stateActionDict = new Dictionary<State, Action>();
  
    // 현재 상태와 트리거를 기반으로 다음 상태를 가져옴
    private Func<State, Trigger, State> stateObserver;
    // 현재 실행되고 있는 액션
    private Action CurrentAction;
   
    // 유니티 함수
    protected override void Awake()
    {
        SetStateAction();
    }

    protected override void OnEnable()
    {
        // 상태 변화 함수 등록
        stateObserver = Managers.FSM.Transition;
        // 기본 상태를 Move로 등록
        CurrentAction = Move;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Target != null)
        {
            CurrentAction.Invoke();
        }
    }

    // FSM State 함수
    public void Idle()
    {

    }

    // 현재 상태에서 조건을 만족하면 다른 상태로 전환
    public void Move()
    {
        objectAnimation.SetBool("IsMove", true);

        transform.LookAt(Target.transform);
        moveDirection = Target.transform.position - transform.position;
        moveDirection.y = 0;
        objectRigidBody.MovePosition(transform.position + moveDirection.normalized * objectStat.moveSpeed * Time.fixedDeltaTime);

        if (ComputeAttackDistance())
        {
            SetCurrentState(Trigger.InAttackDistance);
        }
    }

    // 현재 상태에서 조건을 만족하면 다른 상태로 전환
    private void Attack()
    {
        if (!ComputeAttackDistance())
        {
            SetCurrentState(Trigger.OutAttackDistance);
        }

        objectAnimation.SetBool("IsMove", false);
        objectAnimation.SetTrigger("IsAttack");
        transform.LookAt(Target.transform);
    }

    // 일반 함수

    // FSM에서 쓸 상태들 등록
    protected void SetStateAction()
    {
        stateActionDict.Add(State.Idle, Idle);
        stateActionDict.Add(State.Move, Move);
        stateActionDict.Add(State.Attack, Attack);
    }
}

```

</details>

# Observer Pattern
* 반복문에서 비효율적인 검사를 피하고자 사용
* HP갱신, 시민 수가 0 이 되면 게임 오버 판정, 몬스터가 0 이 되면 다음 스테이지
  
<table>
  <tr>
    <th>HP 갱신</th>
    <th>Next Stage</th>
 </tr>
  <tr>
    <td><img src="Image/HPUI.png" width="150" height="150"/></td>
    <td><img src="Image/NextStage.png" width="150" height="150"/></td>
</tr>
</table>

<table>
  <tr>
    <th>Citizen Zero</th>
    <th>Game Over</th>
 </tr>
  <tr>
    <td><img src="Image/CitizenZero.png" width="150" height="150"/></td>
    <td><img src="Image/GameOver.png" width="150" height="150"/></td>
</tr>
</table>


