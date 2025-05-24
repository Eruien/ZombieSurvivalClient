using System;
using UnityEngine;

// 인게임씬 관리자 클래스
public class InGameSceneManager : MonoBehaviour
{
    // 옵션 UI
    private GameObject optionalUI;

    // 게임이 시작될 때 이벤트
    public static Action observerStageStart;
    // 장벽 스크립트
    public static Gate fenceScript;

    // 처음 스테이지에서 자원을 선택할 때 패널 오브젝트
    private GameObject stageSelectPanel;
    // 인게임에서 사용할 메인 UI
    private GameObject inGameUIPanel;
    // 게임 오버 메시지
    private GameObject gameOverMessage;
    // 게임 클리어 메시지
    private GameObject gameClearMessage;

    // 현재 스테이지
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
        // 처음에 리소스 패널을 클릭하고 나면 게임 시작
        PanelSelect.resourceClickButtonObserver += StageStart;
        // 시민들이 다 죽었을 경우 게임오버
        Managers.Memory.citizenNumberZeroObserver += GameOver;
        // 몬스터가 다 죽었을 경우 다음 스테이지
        Managers.Memory.monsterNumberZeroObserver += StageEnd;
    }

    private void Update()
    {
        if (IsGameOver) return;

        // Escape 키를 누를 경우 메인 UI 활성화 비활성화
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

    // 일반 함수

    // 장벽이 파괴되었을 경우 장벽 새로 생성
    public static void CreateNewFence()
    {
        fenceScript.hp = fenceScript.objectStat.maxhp;
        fenceScript.IsDeath = false;
        fenceScript.gameObject.SetActive(true);
        Managers.Memory.MonsterAllTargetReset();
    }

    // 패널의 자원 갯수 랜덤 생성
    public void SetPanelCount()
    {
        foreach (var panelUI in Managers.Memory.panelUIList)
        {
            panelUI.SetResourceCount();
        }
    }

    // 스테이지 시작
    // 좀비 생성, 스테이지 시작 이벤트 발동
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

    // 스테이지가 끝났을 때 몬스터 삭제와 자원 UI 활성화
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

    // 게임 오버 됬을 때 함수
    private void GameOver()
    {
        GameOverUI();
    }

    // 게임 클리어 됬을 때 함수
    private void GameClear()
    {
        Time.timeScale = 0.0f;
        inGameUIPanel.SetActive(true);
        gameClearMessage.SetActive(true);
    }

    // 게임 오버 됬을 때 UI
    private void GameOverUI()
    {
        Time.timeScale = 0.0f;
        inGameUIPanel.SetActive(true);
        gameOverMessage.SetActive(true);
    }
}
