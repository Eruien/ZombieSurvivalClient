using System;
using TMPro;
using UnityEngine;

// 패널에서 자원선택을 관리하는 클래스
public class PanelSelect : MonoBehaviour
{
    // 자원을 선택했을 때 이벤트 등록
    public static Action resourceClickButtonObserver;
    // 자원 패널에서 자원의 수량을 나타낼 text
    protected TextMeshProUGUI textUI;

    // 자원 수량
    protected int resourceCount = 0;


    protected virtual void Awake()
    {
        Managers.Memory.panelUIList.Add(this);
        textUI = GetComponent<TextMeshProUGUI>();
    }

    public virtual void SetResourceCount()
    {
        
    }

    // 자원 버튼을 클릭했을 때
    public virtual void OnSelectButton()
    {
        resourceClickButtonObserver.Invoke();
    }

    // OnDisable에 액션을 해제할 경우 Panel은 비활성화가 수시로 일어나므로 초기에 등록했던 이벤트가 사라진다
    private void OnDestroy()
    {
        resourceClickButtonObserver = null;
    }
}
