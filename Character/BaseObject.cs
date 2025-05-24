using System;
using UnityEngine;

// 순서 규칙
// 직렬화 필드, 프로퍼티, 유니티 변수, 일반 변수
// public, protected, private 순서
// 일반 타입은 용량 순서대로

// 최상위 부모 기본 요소 추가(타겟, hp, ui 활성화 여부, 캐릭터가 죽었을 때 행동 기본 정의)
public class BaseObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _Target;

    // 상대가 자신을 타겟으로 삼았을 때 타겟으로 삼은 대상을 이벤트 등록
    // 현재 대상이 체력이 0이 되면 Death에서 이벤트 발동 이 타겟말고 다른 타겟으로 바꾸게
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

    // hp가 0이 되었을 때 Death 함수 작동
    // IsViewHP를 통해 hp가 100%인 경우 ui가 보이지 않게
    // hp가 변경 될 경우 ui를 갱신
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

    // 아래에서 초기화
    // 나를 타겟으로 삼은 적을 등록
    public Action enemyObserver;
    // 본인이 죽었을 때 이벤트 등록
    public Action deathObserver;
    // hp가 줄어들었을 때 이벤트 등록
    public Action hpDecreaseObserver;
    public GameObject hpBarUI;

    // 일반 변수 초기화
    // 현재 캐릭터의 스탯
    public ObjectStat objectStat = new ObjectStat();
    public bool IsDeath = false;
    public bool IsViewHP = true;

    // 유니티 함수
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

    // 일반 함수
    public virtual void ChangeTarget()
    {
     
    }

    // ui를 보여줄지 여부
    public void ViewHPBarUI(bool view)
    {
        IsViewHP = view;
        hpBarUI.SetActive(view);
    }

    // json 파일에서 파싱한 데이터를 스탯에 넣어줌
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

    // 공격 사거리 계산
    protected bool ComputeAttackDistance()
    {
        Vector3 vec = Target.transform.position - transform.position;
        float dis = Mathf.Pow(vec.x * vec.x + vec.z * vec.z, 0.5f);
        
        if (dis <= objectStat.attackRange) return true;

        return false;
    }

    // 캐릭터가 죽으면 할 행동
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
