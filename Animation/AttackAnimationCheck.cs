using UnityEngine;
using UnityEngine.Events;

public class AttackAnimationCheck : MonoBehaviour
{
    // ���� �ִϸ��̼��� �������� üũ
    [SerializeField]
    public UnityEvent attackAnimationStartEvent;

    // ���� �ִϸ��̼��� �������� üũ
    [SerializeField]
    public UnityEvent attackAnimationEndEvent;

    // ������ƮŸ���� �߻� ������ üũ
    [SerializeField]
    public UnityEvent attackProjectTileEvent;

    // �ִϸ��̼ǿ� ��ϵǾ� �ִ� �̺�Ʈ
    private void OnAttackStart()
    {
        attackAnimationStartEvent.Invoke();
    }

    // �ִϸ��̼ǿ� ��ϵǾ� �ִ� �̺�Ʈ
    private void OnAttackEnd()
    {
        attackAnimationEndEvent.Invoke();
    }

    // �ִϸ��̼ǿ� ��ϵǾ� �ִ� �̺�Ʈ
    private void OnAttackProjectTile()
    {
        attackProjectTileEvent.Invoke();
    }
}
