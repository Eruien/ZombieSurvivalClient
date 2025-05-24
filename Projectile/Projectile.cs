using UnityEngine;

// ������Ʈ Ÿ�� Ŭ����
public class Projectile : MonoBehaviour
{
    // �ܺο��� ������� �ʴ´ٰ� �����ϸ� ������Ʈ �ı�
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

    // ���� ��Ÿ�
    public float AttackRange { get; set; }
    // ���� ��Ÿ� ����
    public float AttackRangeCorrectionValue { get; set; }
    // ����ü ���ǵ�
    public float ProjectTileSpeed { get; set; }

    // ó�� ��ġ
    public Vector3 initialPos = Vector3.zero;
    // �θ� ���ϴ� ����
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

    // ��� ���Ϳ� �ڽ� ������ �Ÿ� ��� �Լ�
    private float ComputeDistance()
    {
        Vector3 vec = transform.position - initialPos;
        float dis = Mathf.Pow(vec.x * vec.x + vec.z * vec.z, 0.5f);
        return dis;
    }
}
