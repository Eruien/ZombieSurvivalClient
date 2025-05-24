using UnityEngine;
using UnityEngine.Events;

// Socket�� ���� �ݶ��̴��� �浹�ߴ��� üũ�ϴ� Ŭ����
public class CollisionCheck : MonoBehaviour
{
    // �ݶ��̴��� Ÿ������ �� �̺�Ʈ
    [SerializeField]
    public UnityEvent<Collider> UnityHitEvent;

    // ������Ʈ Ÿ���� Ÿ������ �� ����� �Լ�
    public UnityAction<Collider> hitAction;

    // �ݶ��̴��� ���̸� Ÿ�� ������ �ǰ� Trigger�� ����
    private void OnTriggerEnter(Collider other)
    {
        UnityHitEvent.Invoke(other);
    }

    private void OnDisable()
    {
        if (hitAction == null) return;
        UnityHitEvent.RemoveListener(hitAction);
    }

    // ������Ʈ Ÿ�Ͽ� ��Ʈ���� �� �Լ� ���� ���
    public void CollisionAddListener(UnityAction<Collider> action)
    {
        hitAction += action;
        UnityHitEvent.AddListener(hitAction);
    }
}


