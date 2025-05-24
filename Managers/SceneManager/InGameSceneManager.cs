using System;
using UnityEngine;

// �ΰ��Ӿ� ������ Ŭ����
public class InGameSceneManager : MonoBehaviour
{
    // �ɼ� UI
    private GameObject optionalUI;

    // ������ ���۵� �� �̺�Ʈ
    public static Action observerStageStart;
    // �庮 ��ũ��Ʈ
    public static Gate fenceScript;

    // ó�� ������������ �ڿ��� ������ �� �г� ������Ʈ
    private GameObject stageSelectPanel;
    // �ΰ��ӿ��� ����� ���� UI
    private GameObject inGameUIPanel;
    // ���� ���� �޽���
    private GameObject gameOverMessage;
    // ���� Ŭ���� �޽���
    private GameObject gameClearMessage;

    // ���� ��������
    private int currentStage = 0;
    private bool IsGameOver = false;

    private void Awake()
    {
        fenceScript = GameObject.Find("Fence").GetComponent<Gate>();
        stageSelectPanel = GameObject.Find("StageSelectPanel");
        inGameUIPanel = GameObject.Find("InGameUIPanel");
        gameOverMessage = GameObject.Find("GameOverMessage");
        gameClearMessage = GameObject.Find("GameClearMessage");
        optionalUI = GameObject.Find("OptionUIPanel");
    }

    private void Start()
    {
        Time.timeScale = 0.0f;
        SetPanelCount();
        Managers.Memory.ArmySpawn();
        inGameUIPanel.SetActive(false);
        gameOverMessage.SetActive(false);
        gameClearMessage.SetActive(false);
        Managers.Sound.SetMixerSlider();
        optionalUI.SetActive(false);
        Managers.Sound.PlayBackgroundSound("CasualBgm");
    }

    private void OnEnable()
    {
        // ó���� ���ҽ� �г��� Ŭ���ϰ� ���� ���� ����
        PanelSelect.resourceClickButtonObserver += StageStart;
        // �ùε��� �� �׾��� ��� ���ӿ���
        Managers.Memory.citizenNumberZeroObserver += GameOver;
        // ���Ͱ� �� �׾��� ��� ���� ��������
        Managers.Memory.monsterNumberZeroObserver += StageEnd;
    }

    private void Update()
    {
        if (IsGameOver) return;

        // Escape Ű�� ���� ��� ���� UI Ȱ��ȭ ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionalUI.activeSelf)
            {
                optionalUI.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                if (inGameUIPanel.activeSelf)
                {
                    Time.timeScale = 1.0f;
                    inGameUIPanel.SetActive(false);
                }
                else
                {
                    Time.timeScale = 0.0f;
                    inGameUIPanel.SetActive(true);
                }
            }
        }
    }

    private void OnDisable()
    {
        observerStageStart = null;
        fenceScript = null;
        Managers.IsClear = true;
    }

    // �Ϲ� �Լ�

    // �庮�� �ı��Ǿ��� ��� �庮 ���� ����
    public static void CreateNewFence()
    {
        fenceScript.hp = fenceScript.objectStat.maxhp;
        fenceScript.IsDeath = false;
        fenceScript.gameObject.SetActive(true);
        Managers.Memory.MonsterAllTargetReset();
    }

    // �г��� �ڿ� ���� ���� ����
    public void SetPanelCount()
    {
        foreach (var panelUI in Managers.Memory.panelUIList)
        {
            panelUI.SetResourceCount();
        }
    }

    // �������� ����
    // ���� ����, �������� ���� �̺�Ʈ �ߵ�
    private void StageStart()
    {
        currentStage++;

        Managers.Memory.SpawnZombie(Managers.Data.stageDict[currentStage].zombieNumber);
        Managers.Memory.SpawnDemonCreature(Managers.Data.stageDict[currentStage].bossNumber);
        observerStageStart.Invoke();
        Time.timeScale = 1.0f;
        stageSelectPanel.SetActive(false);
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }

    // ���������� ������ �� ���� ������ �ڿ� UI Ȱ��ȭ
    private void StageEnd()
    {
        if (currentStage >= Managers.Data.stageDict.Count)
        {
            GameClear();
            return;
        }

        Time.timeScale = 0.0f;
        Managers.Memory.ClearMonster();
        stageSelectPanel.SetActive(true);
        SetPanelCount();
    }

    // ���� ���� ���� �� �Լ�
    private void GameOver()
    {
        GameOverUI();
    }

    // ���� Ŭ���� ���� �� �Լ�
    private void GameClear()
    {
        Time.timeScale = 0.0f;
        inGameUIPanel.SetActive(true);
        gameClearMessage.SetActive(true);
    }

    // ���� ���� ���� �� UI
    private void GameOverUI()
    {
        Time.timeScale = 0.0f;
        inGameUIPanel.SetActive(true);
        gameOverMessage.SetActive(true);
    }
}
