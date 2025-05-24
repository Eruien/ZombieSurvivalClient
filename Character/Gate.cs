using System;
using UnityEngine;

// ���͸� ���� �庮 Ŭ����
public class Gate : BaseObject
{
    // �庮�� Ȱ��ȭ �ǰ� ��Ȱ��ȭ �ɶ� �̺�Ʈ
    public Action gateActiveObserver;
    public Action gateDeActiveObserver;
    // �庮�� ȸ�� ������
    public float recoveryRate = 0.0f;

    // ����Ƽ �Լ�
    protected override void Awake()
    {
        base.Awake();
        Managers.Memory.memoryList[nameof(Citizen)].Add(this);
        ViewHPBarUI(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hpDecreaseObserver += HitSound;

        if (gateActiveObserver != null)
        {
            gateActiveObserver.Invoke();
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();
        hp += recoveryRate * Time.deltaTime;
    }

    protected override void OnDisable()
    {
        if (gateDeActiveObserver != null)
        {
            gateDeActiveObserver.Invoke();
        }
    }

    private void HitSound()
    {
        Managers.Sound.PlayOneShot(gameObject, "HitWood");
    }
}
