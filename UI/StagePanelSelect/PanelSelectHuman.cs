using UnityEngine;

// �гο��� ���� �ڿ��� ������ �� Ŭ����
public class PanelSelectHuman : PanelSelect
{
    protected override void Awake()
    {
        base.Awake();
    }

    // �ڿ� ���� ����
    public override void SetResourceCount()
    {
        resourceCount = Random.Range(1, 3);
        textUI.text = resourceCount.ToString();
    }

    // �ڿ� ��ư�� Ŭ������ ��
    public override void OnSelectButton()
    {
        base.OnSelectButton();
        Managers.Item.Human += resourceCount;
    }
}
