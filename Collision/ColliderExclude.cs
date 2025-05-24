using UnityEngine;

// 자신의 팀을 공격 충돌 레이어에서 제외
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
