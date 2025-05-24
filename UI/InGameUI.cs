using UnityEngine;
using UnityEngine.SceneManagement;

// 인게임에서 사용되는 main UI
public class InGameUI : MonoBehaviour
{
    // 옵션 관련 패널
    private GameObject optionalUI;

    private void Awake()
    {
        optionalUI = GameObject.Find("OptionUIPanel");
    }

    // X버튼을 클릭시 창 닫기
    public void OnXButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    // 재시작 버튼
    public void OnRetryButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        SceneManager.LoadScene("MainGame");
    }

    // 메인 메뉴 버튼
    public void OnMainMenuButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LobbyScene");
    }

    // 옵션 버튼
    public void OnOptionButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        optionalUI.SetActive(true);
        gameObject.SetActive(false);
    }

    // 게임에서 나가는 버튼
    public void OnExitButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Application.Quit();
    }
}
