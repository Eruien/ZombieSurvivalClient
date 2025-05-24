using Assets.Scripts;
using UnityEngine;

// ������ ����ϴ� �⺻ ���� Ŭ����
public class Army : Citizen
{
    // ������Ʈ Ÿ���� ���� ���ӿ�����Ʈ��
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
        // ���� �� �� Ÿ���� ����
        ChangeTarget();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // ������ ���������� ���۵ȴ����� ��ȯ�Ǳ� ������ ���������� ���۵� �� Ÿ�ٺ���
        InGameSceneManager.observerStageStart += ChangeTarget;
        // ���� �ӵ��� ���Ҷ� �ִϸ��̼��� ���� �ӵ��� ���ϰ� �̺�Ʈ ���
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

    // �Ϲ� �Լ�
    
    // Ÿ���� �¾������ ������Ʈ Ÿ�� ����
    protected override void OnChildHitEvent()
    {
        if (projectile == null) return;
        projectile.GetComponent<Projectile>().IsUse = false;
    }

    // AttackAnimationCheck���� OnAttackProjectTile �̺�Ʈ ��
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
