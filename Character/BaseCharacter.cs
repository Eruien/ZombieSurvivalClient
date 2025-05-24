using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

// �����̴� ĳ���͵��� ��� ���� Ŭ����
public class BaseCharacter : BaseObject
{
    [SerializeField]
    protected GameObject weaponSocket;

    // FSM���� ���� ����
    private State _CurrentState = State.Move;
    public State CurrentState { get { return _CurrentState; } }

    // ���� ���¿� Ʈ���Ÿ� ������� ���� ���¸� ������
    public void SetCurrentState(Trigger trigger)
    {
        _CurrentState = stateObserver.Invoke(_CurrentState, trigger);
        CurrentAction = stateActionDict[_CurrentState];
    }

    // ���⼭ �ʱ�ȭ
    // ���¿� ���� �ൿ ��ųʸ�
    private Dictionary<State, Action> stateActionDict = new Dictionary<State, Action>();
    private Vector3 moveDirection = Vector3.zero;
    // y�� ȸ�� ���� �̻��� ȸ���� �ɸ��� �ʰ� ����
    private Vector3 fixRotation = Vector3.zero;

    // �Ʒ����� �ʱ�ȭ
    protected Rigidbody objectRigidBody;
    protected Animator objectAnimation;
    
    // ���� ���¿� Ʈ���Ÿ� ������� ���� ���¸� ������
    private Func<State, Trigger, State> stateObserver;
    // ���� ����ǰ� �ִ� �׼�
    private Action CurrentAction;
   
    private bool IsHit = false;
    private bool attackAnimationSpan = false;
   
    // ����Ƽ �Լ�
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
        // ���� ��ȭ �Լ� ���
        stateObserver = Managers.FSM.Transition;
        // �⺻ ���¸� Move�� ���
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

    // FSM State �Լ�
    public void Idle()
    {

    }

    // ���� ���¿��� ������ �����ϸ� �ٸ� ���·� ��ȯ
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

    // ���� ���¿��� ������ �����ϸ� �ٸ� ���·� ��ȯ
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

    // �Ϲ� �Լ�

    public void SetAttackSpeed(float attackSpeed)
    {
        objectAnimation.SetFloat("AttackSpeed", attackSpeed);
    }
    
    // FSM���� �� ���µ� ���
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

    // ȸ���� y�ุ �ǰ� y��ġ ����
    private void FixTransform()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    // ���� Ÿ���� �ٰŸ����� ���Ÿ����� Ȯ��
    private bool ConfirmAttackType()
    {
        if (objectStat.attackType == AttackType.Range)
        {
            return true;
        }

        return attackAnimationSpan;
    }

    // ����Ƽ �̺�Ʈ ����
    // CollisionCheck�� �̺�Ʈ ��
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

    // Ÿ���� �� ���� ������ �ϱ� ����
    // AttackAnimationCheck�� �̺�Ʈ ��
    public void OnAttackAnimationStart()
    {
        IsHit = false;
        attackAnimationSpan = true;
        weaponSocket.SetActive(true);
    }

    // �ִϸ��̼� ������ Ÿ�̹� üũ
    // AttackAnimationCheck�� �̺�Ʈ ��
    public void OnAttackAnimationEnd()
    {
        attackAnimationSpan = false;
        weaponSocket.SetActive(false);
    }

    // �ڽĿ��Լ� �߰� �ൿ�� ���� ���
    protected virtual void OnChildHitEvent() { }
}
