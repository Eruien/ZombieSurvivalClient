using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

// ��ųʸ��� ����� ��� ��ųʸ� �����͸� �߰��ϰ� ����
public interface IDict<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

// ������Ʈ�� ������ ������ json������ ���İ� ���� ����
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
    // ������Ʈ�� ������ ����Ʈ
    public List<ObjectStat> objectStatList = new List<ObjectStat>();

    // ������ �߰�
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

// ������Ʈ�� ������ ������ json������ ���İ� ���� ����
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
    // �������� ������ ����Ʈ
    public List<StageStat> stageDataList = new List<StageStat>();

    // ������ �߰�
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

// ������Ʈ�� ������ ������ json������ ���İ� ���� ����
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
    // �ڽ�Ʈ ������ ����Ʈ
    public List<CostStat> costDataList = new List<CostStat>();

    // ������ �߰�
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

// ������ ���� Ŭ����
public class DataManager
{
    // ������Ʈ ������ ��ųʸ�
    public Dictionary<string, ObjectStat> objectDict { get; private set; } = new Dictionary<string, ObjectStat>();
    // �������� ������ ��ųʸ�
    public Dictionary<int, StageStat> stageDict { get; private set; } = new Dictionary<int, StageStat>();
    // �ڽ�Ʈ ������ ��ųʸ�
    public Dictionary<string, CostStat> costDict { get; private set; } = new Dictionary<string, CostStat>();

    // ó�� �ʱ�ȭ
    public void Init()
    {
        objectDict = LoadJson<ObjectData, string, ObjectStat>("ObjectData").MakeDict();
        stageDict = LoadJson<StageData, int, StageStat>("StageData").MakeDict();
        costDict = LoadJson<CostData, string, CostStat>("costData").MakeDict();
    }

    // ���� �ٽ� �ε� �� �� �ʱ�ȭ
    public void Clear()
    {
        objectDict.Clear();
        stageDict.Clear();
        costDict.Clear();
        objectDict = LoadJson<ObjectData, string, ObjectStat>("ObjectData").MakeDict();
        stageDict = LoadJson<StageData, int, StageStat>("StageData").MakeDict();
        costDict = LoadJson<CostData, string, CostStat>("costData").MakeDict();
    }

    // json������ �Ľ�
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : IDict<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"GameData/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}