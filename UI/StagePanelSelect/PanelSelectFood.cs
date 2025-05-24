using UnityEngine;

// 패널에서 음식 자원을 선택할 때 클래스
public class PanelSelectFood : PanelSelect
{
    protected override void Awake()
    {
        base.Awake();
    }

    // 자원 랜덤 세팅
    public override void SetResourceCount()
    {
        resourceCount = Random.Range(4, 10);
        textUI.text = resourceCount.ToString();
    }

    // 자원 버튼을 클릭했을 때
    public override void OnSelectButton()
    {
        base.OnSelectButton();
        Managers.Item.Food += resourceCount;
    }
}
