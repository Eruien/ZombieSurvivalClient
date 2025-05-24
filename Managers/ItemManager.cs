using System;

// 아이템 관리 클래스
public class ItemManager
{
    // 초기 돈
    private float InitialMoney = 500.0f;
    // 현재 돈
    private float _CurrentMoney = 0.0f;

    public float CurrentMoney
    {
        get { return _CurrentMoney; }
        set
        {
            _CurrentMoney = value;

            if (moneyObserver != null)
            {
                moneyObserver.Invoke(_CurrentMoney);
            }

            if (humanObserver != null)
            {
                humanObserver.Invoke(_Human);
            }

            if (foodObserver != null)
            {
                foodObserver.Invoke(_Food);
            }
        }
    }

    // 초기 인적 자원
    private int InitialHuman = 20;
    // 현재 인적 자원
    private int _Human = 0;
    public int Human
    {
        get { return _Human; }
        set
        {
            _Human = value;

            if (humanObserver != null)
            {
                humanObserver.Invoke(_Human);
            }
        } 
    }

    // 초기 음식 자원
    private int InitialFood = 20;
    // 현재 음식 자원
    private int _Food = 0;
    public int Food
    {
        get { return _Food; }
        set
        {
            _Food = value;

            if (foodObserver != null)
            {
                foodObserver.Invoke(_Food);
            }
        }
    }

    // 현재 돈 보유량이 바뀔때마다 이벤트 발생
    public Action<float> moneyObserver;
    // 인적 자원이 바뀔때마다 이벤트 발생
    public Action<int> humanObserver;
    // 음식 자원이 바뀔때마다 이벤트 발생
    public Action<int> foodObserver;

    // 처음 초기화
    public void Init()
    {
        CurrentMoney = InitialMoney;
        Human = InitialHuman;
        Food = InitialFood;
    }
    
    // 씬이 바뀔때 다시 세팅
    public void Clear()
    {
        CurrentMoney = InitialMoney;
        Human = InitialHuman;
        Food = InitialFood;
        humanObserver = null;
        foodObserver = null;
    }
}
