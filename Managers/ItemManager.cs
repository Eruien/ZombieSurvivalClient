using System;

// ������ ���� Ŭ����
public class ItemManager
{
    // �ʱ� ��
    private float InitialMoney = 500.0f;
    // ���� ��
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

    // �ʱ� ���� �ڿ�
    private int InitialHuman = 20;
    // ���� ���� �ڿ�
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

    // �ʱ� ���� �ڿ�
    private int InitialFood = 20;
    // ���� ���� �ڿ�
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

    // ���� �� �������� �ٲ𶧸��� �̺�Ʈ �߻�
    public Action<float> moneyObserver;
    // ���� �ڿ��� �ٲ𶧸��� �̺�Ʈ �߻�
    public Action<int> humanObserver;
    // ���� �ڿ��� �ٲ𶧸��� �̺�Ʈ �߻�
    public Action<int> foodObserver;

    // ó�� �ʱ�ȭ
    public void Init()
    {
        CurrentMoney = InitialMoney;
        Human = InitialHuman;
        Food = InitialFood;
    }
    
    // ���� �ٲ� �ٽ� ����
    public void Clear()
    {
        CurrentMoney = InitialMoney;
        Human = InitialHuman;
        Food = InitialFood;
        humanObserver = null;
        foodObserver = null;
    }
}
