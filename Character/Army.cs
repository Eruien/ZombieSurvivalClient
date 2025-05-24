using Assets.Scripts;
using UnityEngine;

// 공격을 담당하는 기본 군인 클래스
public class Army : Citizen
{
    // 프로젝트 타일을 위한 게임오브젝트들
    private GameObject projectilePrefab;
    private GameObject projectile;
   
    protected override void Awake()
    {
        base.Awake();
        SetAttackSpeed(Managers.Animation.ArmyAttackSpeed);
        projectilePrefab = Managers.Resource.Load<GameObject>("Prefabs/Rocket_Projectile");
    }

    protected override void Start()
    {
        base.Start();
        // 시작 될 때 타겟을 선정
        ChangeTarget();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // 적들은 스테이지가 시작된다음에 소환되기 때문에 스테이지가 시작될 때 타겟변경
        InGameSceneManager.observerStageStart += ChangeTarget;
        // 공격 속도가 변할때 애니메이션의 공격 속도가 변하게 이벤트 등록
        Managers.Animation.observerArmyAttackSpeed += SetAttackSpeed;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
    }

    // 일반 함수
    
    // 타겟이 맞았을경우 프로젝트 타일 삭제
    protected override void OnChildHitEvent()
    {
        if (projectile == null) return;
        projectile.GetComponent<Projectile>().IsUse = false;
    }

    // AttackAnimationCheck에서 OnAttackProjectTile 이벤트 용
    public void OnAttackArmyProjectTile()
    {
        projectile = Instantiate(projectilePrefab, weaponSocket.transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.AttackRange = objectStat.attackRange;
        projectileScript.AttackRangeCorrectionValue = objectStat.attackRangeCorrectionValue;
        projectileScript.ProjectTileSpeed = objectStat.projectTileSpeed;
        projectileScript.parentForward = transform.forward;
        projectile.GetComponent<CollisionCheck>().CollisionAddListener(OnHitEvent);
        projectile.GetComponent<SelectColliderExclude>().SelectExcludeLayer(gameObject.layer);
        Managers.Sound.PlaySound(gameObject, "ArmyFire");
    }
}
