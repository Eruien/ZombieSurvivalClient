using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

// 딕셔너리를 사용할 경우 딕셔너리 데이터를 추가하게 강제
public interface IDict<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

// 오브젝트의 데이터 형식을 json데이터 형식과 같게 선언
[Serializable]
public class ObjectStat
{
    public string name;
    public AttackType attackType;
    public float maxhp;
    public float defense;
    public float attackSpeed;
    public float attackRange;
    public float attackRangeCorrectionValue;
    public float defaultAttackDamage;
    public float moveSpeed;
    public float projectTileSpeed;
}

[Serializable]
public class ObjectData : IDict<string, ObjectStat>
{
    // 오브젝트의 데이터 리스트
    public List<ObjectStat> objectStatList = new List<ObjectStat>();

    // 데이터 추가
    public Dictionary<string, ObjectStat> MakeDict()
    {
        Dictionary<string, ObjectStat> dict = new Dictionary<string, ObjectStat>();
        foreach (ObjectStat state in objectStatList)
        {
            dict.Add(state.name, state);
        }
        return dict;
    }
}

// 오브젝트의 데이터 형식을 json데이터 형식과 같게 선언
[Serializable]
public class StageStat
{
    public int stageNumber;
    public int zombieNumber;
    public int bossNumber;
}

[Serializable]
public class StageData : IDict<int, StageStat>
{
    // 스테이지 데이터 리스트
    public List<StageStat> stageDataList = new List<StageStat>();

    // 데이터 추가
    public Dictionary<int, StageStat> MakeDict()
    {
        Dictionary<int, StageStat> dict = new Dictionary<int, StageStat>();
        foreach (StageStat state in stageDataList)
        {
            dict.Add(state.stageNumber, state);
        }
        return dict;
    }
}

// 오브젝트의 데이터 형식을 json데이터 형식과 같게 선언
[Serializable]
public class CostStat
{
    public string name;
    public float cost;
    public int itemCost;
}

[Serializable]
public class CostData : IDict<string, CostStat>
{
    // 코스트 데이터 리스트
    public List<CostStat> costDataList = new List<CostStat>();

    // 데이터 추가
    public Dictionary<string, CostStat> MakeDict()
    {
        Dictionary<string, CostStat> dict = new Dictionary<string, CostStat>();
        foreach (CostStat state in costDataList)
        {
            dict.Add(state.name, state);
        }
        return dict;
    }
}

// 데이터 관리 클래스
public class DataManager
{
    // 오브젝트 데이터 딕셔너리
    public Dictionary<string, ObjectStat> objectDict { get; private set; } = new Dictionary<string, ObjectStat>();
    // 스테이지 데이터 딕셔너리
    public Dictionary<int, StageStat> stageDict { get; private set; } = new Dictionary<int, StageStat>();
    // 코스트 데이터 딕셔너리
    public Dictionary<string, CostStat> costDict { get; private set; } = new Dictionary<string, CostStat>();

    // 처음 초기화
    public void Init()
    {
        objectDict = LoadJson<ObjectData, string, ObjectStat>("ObjectData").MakeDict();
        stageDict = LoadJson<StageData, int, StageStat>("StageData").MakeDict();
        costDict = LoadJson<CostData, string, CostStat>("costData").MakeDict();
    }

    // 씬이 다시 로드 될 때 초기화
    public void Clear()
    {
        objectDict.Clear();
        stageDict.Clear();
        costDict.Clear();
        objectDict = LoadJson<ObjectData, string, ObjectStat>("ObjectData").MakeDict();
        stageDict = LoadJson<StageData, int, StageStat>("StageData").MakeDict();
        costDict = LoadJson<CostData, string, CostStat>("costData").MakeDict();
    }

    // json데이터 파싱
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : IDict<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"GameData/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}