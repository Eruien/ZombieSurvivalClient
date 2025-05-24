using System;
using UnityEngine;

// 몬스터를 막는 장벽 클래스
public class Gate : BaseObject
{
    // 장벽이 활성화 되고 비활서화 될때 이벤트
    public Action gateActiveObserver;
    public Action gateDeActiveObserver;
    // 장벽의 회복 게이지
    public float recoveryRate = 0.0f;

    // 유니티 함수
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
