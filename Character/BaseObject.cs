using System;
using UnityEngine;

// ���� ��Ģ
// ����ȭ �ʵ�, ������Ƽ, ����Ƽ ����, �Ϲ� ����
// public, protected, private ����
// �Ϲ� Ÿ���� �뷮 �������

// �ֻ��� �θ� �⺻ ��� �߰�(Ÿ��, hp, ui Ȱ��ȭ ����, ĳ���Ͱ� �׾��� �� �ൿ �⺻ ����)
public class BaseObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _Target;

    // ��밡 �ڽ��� Ÿ������ ����� �� Ÿ������ ���� ����� �̺�Ʈ ���
    // ���� ����� ü���� 0�� �Ǹ� Death���� �̺�Ʈ �ߵ� �� Ÿ�ٸ��� �ٸ� Ÿ������ �ٲٰ�
    public GameObject Target
    {
        get { return _Target; }
        set
        { 
            _Target = value;
            _Target.GetComponent<BaseObject>().enemyObserver += ChangeTarget;
        }
    }

    [SerializeField]
    private float _hp = 0.0f;

    // hp�� 0�� �Ǿ��� �� Death �Լ� �۵�
    // IsViewHP�� ���� hp�� 100%�� ��� ui�� ������ �ʰ�
    // hp�� ���� �� ��� ui�� ����
    public float hp
    {
        get { return _hp; }
        set
        {
            float prevHP = _hp;
            _hp = value;

            if (_hp - prevHP < 0)
            {
                if (hpDecreaseObserver != null)
                {
                    hpDecreaseObserver.Invoke();
                }
            }
           
            if (_hp <= float.Epsilon)
            {
                Death();
            }

            if (IsViewHP)
            {
                if (MathF.Abs(objectStat.maxhp - _hp) <= float.Epsilon)
                {
                    hpBarUI.SetActive(false);
                }
                else
                {
                    hpBarUI.SetActive(true);
                }
            }
        } 
    }

    // �Ʒ����� �ʱ�ȭ
    // ���� Ÿ������ ���� ���� ���
    public Action enemyObserver;
    // ������ �׾��� �� �̺�Ʈ ���
    public Action deathObserver;
    // hp�� �پ����� �� �̺�Ʈ ���
    public Action hpDecreaseObserver;
    public GameObject hpBarUI;

    // �Ϲ� ���� �ʱ�ȭ
    // ���� ĳ������ ����
    public ObjectStat objectStat = new ObjectStat();
    public bool IsDeath = false;
    public bool IsViewHP = true;

    // ����Ƽ �Լ�
    protected virtual void Awake()
    {
        hpBarUI = Managers.Resource.Instantiate("HPBar_UI", transform.position);
        hpBarUI.transform.SetParent(transform, false);
        SetObjectStat();
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void FixedUpdate()
    {
        if (IsDeath) return;
    }

    protected virtual void Update()
    {
        if (IsDeath) return;
    }

    protected virtual void OnDisable()
    {
        if (_Target != null)
        {
            _Target.GetComponent<BaseObject>().enemyObserver -= ChangeTarget;
        }
    }

    // �Ϲ� �Լ�
    public virtual void ChangeTarget()
    {
     
    }

    // ui�� �������� ����
    public void ViewHPBarUI(bool view)
    {
        IsViewHP = view;
        hpBarUI.SetActive(view);
    }

    // json ���Ͽ��� �Ľ��� �����͸� ���ȿ� �־���
    protected virtual void SetObjectStat()
    {
        objectStat.maxhp = Managers.Data.objectDict[this.GetType().Name].maxhp;
        hp = objectStat.maxhp;
        objectStat.defense = Managers.Data.objectDict[this.GetType().Name].defense;
        objectStat.attackSpeed = Managers.Data.objectDict[this.GetType().Name].attackSpeed;
        objectStat.attackType = Managers.Data.objectDict[this.GetType().Name].attackType;
        objectStat.attackRange = Managers.Data.objectDict[this.GetType().Name].attackRange;
        objectStat.attackRangeCorrectionValue = Managers.Data.objectDict[this.GetType().Name].attackRangeCorrectionValue;
        objectStat.defaultAttackDamage = Managers.Data.objectDict[this.GetType().Name].defaultAttackDamage;
        objectStat.moveSpeed = Managers.Data.objectDict[this.GetType().Name].moveSpeed;
        objectStat.projectTileSpeed = Managers.Data.objectDict[this.GetType().Name].projectTileSpeed;
    }

    // ���� ��Ÿ� ���
    protected bool ComputeAttackDistance()
    {
        Vector3 vec = Target.transform.position - transform.position;
        float dis = Mathf.Pow(vec.x * vec.x + vec.z * vec.z, 0.5f);
        
        if (dis <= objectStat.attackRange) return true;

        return false;
    }

    // ĳ���Ͱ� ������ �� �ൿ
    protected virtual void Death()
    {
        IsDeath = true;

        if (_Target != null)
        {
            _Target.GetComponent<BaseObject>().enemyObserver -= ChangeTarget;
        }

        if (enemyObserver != null)
        {
            enemyObserver.Invoke();
            enemyObserver = null;
        }
       
        if (deathObserver != null)
        {
            deathObserver.Invoke();
            deathObserver = null;
        }
       
        gameObject.SetActive(false);
    }
}
