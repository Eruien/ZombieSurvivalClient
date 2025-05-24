using System;
using System.Collections.Generic;
using UnityEngine;

// 전체 메모리 관리 클래스
public class MemoryManager
{
    // 현재 씬에 있는 몬스터 수
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

    // 현재 씬에 있는 시민 수
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

    // 몬스터가 0이 됬을 때 이벤트
    public Action monsterNumberZeroObserver;
    // 시민이 0이 됬을 때 이벤트
    public Action citizenNumberZeroObserver;
    // 군인수에 따른 이벤트
    public Action<int> armyCountObserver;

    // 전체 메모리 리스트
    public Dictionary<string, List<BaseObject>> memoryList = new Dictionary<string, List<BaseObject>>();
    // 패널 오브젝트 관리
    public List<PanelSelect> panelUIList = new List<PanelSelect>();

    // 군인, 좀비 리스폰 포인트
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

    // 가까운 적을 찾을 때 사용
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

    // 씬이 바뀔 때 클리어 작업
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

    
    // 스테이지가 끝나면 몬스터 메모리 정리
    public void ClearMonster()
    {
        foreach (var monster in memoryList["Monster"])
        {
            GameObject.Destroy(monster.gameObject);
        }

        memoryList["Monster"].Clear();
    }

    // 좀비 스폰
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

    // 보스 스폰
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

    // 특정 상황에서 몬스터 타겟을 전부 리셋해야 할 때
    public void MonsterAllTargetReset()
    {
        foreach (var monster in memoryList["Monster"])
        {
            monster.ChangeTarget();
        }
    }

    // 군인 스폰
    public void ArmySpawn()
    {
        Vector3 randomPos = armyRespawnPoint + UnityEngine.Random.insideUnitSphere * armySpawnRange;
        randomPos.y = armyRespawnPoint.y;
        BaseObject obj = Managers.Resource.Instantiate("Citizen_Army", randomPos).GetComponent<BaseObject>();
        CurrentCitizenNumber++;
        obj.deathObserver += MinusCitizenCount;
    }

    // 몬스터가 죽을 때 카운트 이벤트
    private void MinusMonsterCount()
    {
        CurrentMonsterNumber -= 1;
    }

    // 시민이 죽을 때 카운트 이벤트
    private void MinusCitizenCount()
    {
        CurrentCitizenNumber -= 1;
    }
}
