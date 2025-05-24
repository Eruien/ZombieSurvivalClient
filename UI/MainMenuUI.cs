using UnityEngine;
using UnityEngine.SceneManagement;

// 메인 메뉴 UI
public class MainMenuUI : MonoBehaviour
{
    // 옵션 UI
    [SerializeField]
    private GameObject optionalUI;
    
    private void Awake()
    {
        Managers.Sound.SetMixerSlider();
        Managers.Sound.PlayBackgroundSound("MainMenuBgm");
        optionalUI.SetActive(false);
    }

    // 게임 시작 버튼
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainGame");
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }

    // 옵션 버튼
    public void OnOptionButton()
    {
        optionalUI.SetActive(true);
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }

    // 게임 나가기 버튼
    public void OnExitButton()
    {
        Application.Quit();
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }

}
