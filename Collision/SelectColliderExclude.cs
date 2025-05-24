using UnityEngine;

// 프로젝트 타일용 제외되는 레이어를 외부에서 선택할 수 있게
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
