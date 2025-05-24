using System;
using TMPro;
using UnityEngine;

// �гο��� �ڿ������� �����ϴ� Ŭ����
public class PanelSelect : MonoBehaviour
{
    // �ڿ��� �������� �� �̺�Ʈ ���
    public static Action resourceClickButtonObserver;
    // �ڿ� �гο��� �ڿ��� ������ ��Ÿ�� text
    protected TextMeshProUGUI textUI;

    // �ڿ� ����
    protected int resourceCount = 0;


    protected virtual void Awake()
    {
        Managers.Memory.panelUIList.Add(this);
        textUI = GetComponent<TextMeshProUGUI>();
    }

    public virtual void SetResourceCount()
    {
        
    }

    // �ڿ� ��ư�� Ŭ������ ��
    public virtual void OnSelectButton()
    {
        resourceClickButtonObserver.Invoke();
    }

    // OnDisable�� �׼��� ������ ��� Panel�� ��Ȱ��ȭ�� ���÷� �Ͼ�Ƿ� �ʱ⿡ ����ߴ� �̺�Ʈ�� �������
    private void OnDestroy()
    {
        resourceClickButtonObserver = null;
    }
}
