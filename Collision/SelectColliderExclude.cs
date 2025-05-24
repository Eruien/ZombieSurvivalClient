using UnityEngine;

// ������Ʈ Ÿ�Ͽ� ���ܵǴ� ���̾ �ܺο��� ������ �� �ְ�
public class SelectColliderExclude : MonoBehaviour
{
    private Collider attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<BoxCollider>();
        if (attackCollider == null)
        {
            attackCollider = GetComponent<SphereCollider>();
        }
    }

    public void SelectExcludeLayer(int layer)
    {
        attackCollider.excludeLayers = (1 << layer) | attackCollider.excludeLayers;
    }
}
