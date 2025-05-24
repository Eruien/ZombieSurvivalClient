using System;
using System.Collections.Generic;
using UnityEngine;

// ��ü �޸� ���� Ŭ����
public class MemoryManager
{
    // ���� ���� �ִ� ���� ��
    private int _CurrentMonsterNumber = 0;

    public int CurrentMonsterNumber
    {
        get { return _CurrentMonsterNumber; }
        set
        {
            _CurrentMonsterNumber = value;

            if (_CurrentMonsterNumber <= 0)
            {
                monsterNumberZeroObserver.Invoke();
            }
        }
    }

    // ���� ���� �ִ� �ù� ��
    private int _CurrentCitizenNumber = 0;

    public int CurrentCitizenNumber
    {
        get { return _CurrentCitizenNumber; }
        set
        {
            _CurrentCitizenNumber = value;

            if (armyCountObserver != null)
            {
                armyCountObserver.Invoke(_CurrentCitizenNumber);
            }

            if (_CurrentCitizenNumber <= 0)
            {
                citizenNumberZeroObserver.Invoke();
            }
        }
    }

    // ���Ͱ� 0�� ���� �� �̺�Ʈ
    public Action monsterNumberZeroObserver;
    // �ù��� 0�� ���� �� �̺�Ʈ
    public Action citizenNumberZeroObserver;
    // ���μ��� ���� �̺�Ʈ
    public Action<int> armyCountObserver;

    // ��ü �޸� ����Ʈ
    public Dictionary<string, List<BaseObject>> memoryList = new Dictionary<string, List<BaseObject>>();
    // �г� ������Ʈ ����
    public List<PanelSelect> panelUIList = new List<PanelSelect>();

    // ����, ���� ������ ����Ʈ
    private Vector3 armyRespawnPoint = new Vector3(-5.56f, 0.0f, 26.12f);
    private Vector3 zombieRespawnPoint = new Vector3(16.63f, 0.0f, 25.84f);

    private float armySpawnRange = 3.0f;
    private float zombieSpawnRange = 5.0f;

    public void Init()
    {
        List<BaseObject> citizenList = new List<BaseObject>();
        List<BaseObject> monsterList = new List<BaseObject>();
        memoryList.Add("Citizen", citizenList);
        memoryList.Add("Monster", monsterList);
    }

    // ����� ���� ã�� �� ���
    public BaseObject GetNearEnemy(string enemyType, BaseObject baseObj)
    {
        if (baseObj == null) return null;

        BaseObject resultObj = null;
        float currentDis = float.PositiveInfinity;

        foreach (var obj in memoryList[enemyType])
        {
            if (obj.IsDeath) continue;
            
            Vector3 vec = obj.transform.position - baseObj.transform.position;
            float dis = Mathf.Pow(vec.x * vec.x + vec.z * vec.z, 0.5f);

            if (dis <= currentDis)
            {
                currentDis = dis;
                resultObj = obj;
            }
        }

        return resultObj;
    }

    // ���� �ٲ� �� Ŭ���� �۾�
    public void Clear()
    {
        _CurrentMonsterNumber = 0;
        _CurrentCitizenNumber = 0;
        monsterNumberZeroObserver = null;
        citizenNumberZeroObserver = null;
        armyCountObserver = null;
        panelUIList.Clear();
        memoryList["Monster"].Clear();
        memoryList["Citizen"].Clear();
    }

    
    // ���������� ������ ���� �޸� ����
    public void ClearMonster()
    {
        foreach (var monster in memoryList["Monster"])
        {
            GameObject.Destroy(monster.gameObject);
        }

        memoryList["Monster"].Clear();
    }

    // ���� ����
    public void SpawnZombie(int zombieCount)
    {
        for (int i = 0; i < zombieCount; i++)
        {
            Vector3 randomPos = zombieRespawnPoint + UnityEngine.Random.insideUnitSphere * zombieSpawnRange;
            randomPos.y = zombieRespawnPoint.y;
            BaseObject zombie = Managers.Resource.Instantiate("Zombie", randomPos).GetComponent<BaseObject>();
            CurrentMonsterNumber++;
            zombie.deathObserver += MinusMonsterCount;
        }
    }

    // ���� ����
    public void SpawnDemonCreature(int creatureCount)
    {
        for (int i = 0; i < creatureCount; i++)
        {
            Vector3 randomPos = zombieRespawnPoint + UnityEngine.Random.insideUnitSphere * zombieSpawnRange;
            randomPos.y = zombieRespawnPoint.y;
            BaseObject creature = Managers.Resource.Instantiate("DemonCreature", randomPos).GetComponent<BaseObject>();
            CurrentMonsterNumber++;
            creature.deathObserver += MinusMonsterCount;
        }
    }

    // Ư�� ��Ȳ���� ���� Ÿ���� ���� �����ؾ� �� ��
    public void MonsterAllTargetReset()
    {
        foreach (var monster in memoryList["Monster"])
        {
            monster.ChangeTarget();
        }
    }

    // ���� ����
    public void ArmySpawn()
    {
        Vector3 randomPos = armyRespawnPoint + UnityEngine.Random.insideUnitSphere * armySpawnRange;
        randomPos.y = armyRespawnPoint.y;
        BaseObject obj = Managers.Resource.Instantiate("Citizen_Army", randomPos).GetComponent<BaseObject>();
        CurrentCitizenNumber++;
        obj.deathObserver += MinusCitizenCount;
    }

    // ���Ͱ� ���� �� ī��Ʈ �̺�Ʈ
    private void MinusMonsterCount()
    {
        CurrentMonsterNumber -= 1;
    }

    // �ù��� ���� �� ī��Ʈ �̺�Ʈ
    private void MinusCitizenCount()
    {
        CurrentCitizenNumber -= 1;
    }
}
