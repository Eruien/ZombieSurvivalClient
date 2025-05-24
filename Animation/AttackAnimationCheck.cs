using UnityEngine;
using UnityEngine.Events;

public class AttackAnimationCheck : MonoBehaviour
{
    // 공격 애니메이션의 시작점을 체크
    [SerializeField]
    public UnityEvent attackAnimationStartEvent;

    // 공격 애니메이션의 끝날때를 체크
    [SerializeField]
    public UnityEvent attackAnimationEndEvent;

    // 프로젝트타일의 발사 시점을 체크
    [SerializeField]
    public UnityEvent attackProjectTileEvent;

    // 애니메이션에 등록되어 있는 이벤트
    private void OnAttackStart()
    {
        attackAnimationStartEvent.Invoke();
    }

    // 애니메이션에 등록되어 있는 이벤트
    private void OnAttackEnd()
    {
        attackAnimationEndEvent.Invoke();
    }

    // 애니메이션에 등록되어 있는 이벤트
    private void OnAttackProjectTile()
    {
        attackProjectTileEvent.Invoke();
    }
}
