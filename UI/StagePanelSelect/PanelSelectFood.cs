using UnityEngine;

// �гο��� ���� �ڿ��� ������ �� Ŭ����
public class PanelSelectFood : PanelSelect
{
    protected override void Awake()
    {
        base.Awake();
    }

    // �ڿ� ���� ����
    public override void SetResourceCount()
    {
        resourceCount = Random.Range(4, 10);
        textUI.text = resourceCount.ToString();
    }

    // �ڿ� ��ư�� Ŭ������ ��
    public override void OnSelectButton()
    {
        base.OnSelectButton();
        Managers.Item.Food += resourceCount;
    }
}
