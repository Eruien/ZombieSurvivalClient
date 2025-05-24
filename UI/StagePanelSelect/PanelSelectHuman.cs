using UnityEngine;

// 패널에서 인적 자원을 선택할 때 클래스
public class PanelSelectHuman : PanelSelect
{
    protected override void Awake()
    {
        base.Awake();
    }

    // 자원 랜덤 세팅
    public override void SetResourceCount()
    {
        resourceCount = Random.Range(1, 3);
        textUI.text = resourceCount.ToString();
    }

    // 자원 버튼을 클릭했을 때
    public override void OnSelectButton()
    {
        base.OnSelectButton();
        Managers.Item.Human += resourceCount;
    }
}
