using UnityEngine;
using UnityEngine.Events;

// Socket에 붙은 콜라이더가 충돌했는지 체크하는 클래스
public class CollisionCheck : MonoBehaviour
{
    // 콜라이더가 타격했을 때 이벤트
    [SerializeField]
    public UnityEvent<Collider> UnityHitEvent;

    // 프로젝트 타일이 타격했을 때 사용할 함수
    public UnityAction<Collider> hitAction;

    // 콜라이더가 닿이면 타격 판정이 되게 Trigger로 설정
    private void OnTriggerEnter(Collider other)
    {
        UnityHitEvent.Invoke(other);
    }

    private void OnDisable()
    {
        if (hitAction == null) return;
        UnityHitEvent.RemoveListener(hitAction);
    }

    // 프로젝트 타일용 히트됬을 때 함수 직접 등록
    public void CollisionAddListener(UnityAction<Collider> action)
    {
        hitAction += action;
        UnityHitEvent.AddListener(hitAction);
    }
}


