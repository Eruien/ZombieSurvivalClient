using UnityEngine;

// 프로젝트 타일 클래스
public class Projectile : MonoBehaviour
{
    // 외부에서 사용하지 않는다고 설정하면 오브젝트 파괴
    private bool _IsUse = true;
    public bool IsUse
    {
        get { return _IsUse; }
        set
        {
            _IsUse = value;
            if (!_IsUse)
            {
                Destroy(gameObject);
            }
        }
    }

    // 공격 사거리
    public float AttackRange { get; set; }
    // 공격 사거리 보정
    public float AttackRangeCorrectionValue { get; set; }
    // 투사체 스피드
    public float ProjectTileSpeed { get; set; }

    // 처음 위치
    public Vector3 initialPos = Vector3.zero;
    // 부모가 향하는 방향
    public Vector3 parentForward = Vector3.zero;

    private void Awake()
    {
        initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.LookAt(parentForward);
        transform.position += parentForward * (ProjectTileSpeed * Time.fixedDeltaTime);

        if (ComputeDistance() >= AttackRange + AttackRangeCorrectionValue && IsUse)
        {
            IsUse = false;
        }
    }

    // 상대 벡터와 자신 사이의 거리 계산 함수
    private float ComputeDistance()
    {
        Vector3 vec = transform.position - initialPos;
        float dis = Mathf.Pow(vec.x * vec.x + vec.z * vec.z, 0.5f);
        return dis;
    }
}
