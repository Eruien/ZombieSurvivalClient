using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

// 움직이는 캐릭터들이 상속 받을 클래스
public class BaseCharacter : BaseObject
{
    [SerializeField]
    protected GameObject weaponSocket;

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
    private Vector3 moveDirection = Vector3.zero;
    // y축 회전 말고 이상한 회전이 걸리지 않게 고정
    private Vector3 fixRotation = Vector3.zero;

    // 아래에서 초기화
    protected Rigidbody objectRigidBody;
    protected Animator objectAnimation;
    
    // 현재 상태와 트리거를 기반으로 다음 상태를 가져옴
    private Func<State, Trigger, State> stateObserver;
    // 현재 실행되고 있는 액션
    private Action CurrentAction;
   
    private bool IsHit = false;
    private bool attackAnimationSpan = false;
   
    // 유니티 함수
    protected override void Awake()
    {
        base.Awake();
        objectRigidBody = GetComponent<Rigidbody>();
        objectAnimation = GetComponentInChildren<Animator>();
        SetStateAction();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
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
        
        FixTransform();
    }

    protected override void Update()
    {
        base.Update();
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

    public void SetAttackSpeed(float attackSpeed)
    {
        objectAnimation.SetFloat("AttackSpeed", attackSpeed);
    }
    
    // FSM에서 쓸 상태들 등록
    protected void SetStateAction()
    {
        stateActionDict.Add(State.Idle, Idle);
        stateActionDict.Add(State.Move, Move);
        stateActionDict.Add(State.Attack, Attack);
    }

    protected override void Death()
    {
        base.Death();
        objectAnimation.SetBool("IsDeath", true);
    }

    // 회전은 y축만 되게 y위치 고정
    private void FixTransform()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    // 어택 타입이 근거리인지 원거리인지 확인
    private bool ConfirmAttackType()
    {
        if (objectStat.attackType == AttackType.Range)
        {
            return true;
        }

        return attackAnimationSpan;
    }

    // 유니티 이벤트 모음
    // CollisionCheck의 이벤트 용
    public void OnHitEvent(Collider other)
    {
        if (!IsHit && attackAnimationSpan && ConfirmAttackType())
        {
            BaseObject obj = other.gameObject.GetComponent<BaseObject>();
            OnChildHitEvent();
            IsHit = true;

            if (obj == null) return;

            obj.hp -= Managers.Data.objectDict[this.GetType().Name].defaultAttackDamage - obj.objectStat.defense;
        }
    }

    // 타격을 한 번만 입히게 하기 위해
    // AttackAnimationCheck의 이벤트 용
    public void OnAttackAnimationStart()
    {
        IsHit = false;
        attackAnimationSpan = true;
        weaponSocket.SetActive(true);
    }

    // 애니메이션 끝나는 타이밍 체크
    // AttackAnimationCheck의 이벤트 용
    public void OnAttackAnimationEnd()
    {
        attackAnimationSpan = false;
        weaponSocket.SetActive(false);
    }

    // 자식에게서 추가 행동이 있을 경우
    protected virtual void OnChildHitEvent() { }
}
