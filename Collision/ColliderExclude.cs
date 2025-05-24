using UnityEngine;

// �ڽ��� ���� ���� �浹 ���̾�� ����
public class ColliderExclude : MonoBehaviour
{
    private int teamLayerNumber = 0;
    private Collider attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<BoxCollider>();

        if (attackCollider == null)
        {
            attackCollider = GetComponent<SphereCollider>();
        }

        teamLayerNumber = transform.root.gameObject.layer;
        attackCollider.excludeLayers = (1 << teamLayerNumber) | attackCollider.excludeLayers;
    }
}
