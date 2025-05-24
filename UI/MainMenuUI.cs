using UnityEngine;
using UnityEngine.SceneManagement;

// ���� �޴� UI
public class MainMenuUI : MonoBehaviour
{
    // �ɼ� UI
    [SerializeField]
    private GameObject optionalUI;
    
    private void Awake()
    {
        Managers.Sound.SetMixerSlider();
        Managers.Sound.PlayBackgroundSound("MainMenuBgm");
        optionalUI.SetActive(false);
    }

    // ���� ���� ��ư
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainGame");
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }

    // �ɼ� ��ư
    public void OnOptionButton()
    {
        optionalUI.SetActive(true);
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }

    // ���� ������ ��ư
    public void OnExitButton()
    {
        Application.Quit();
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }

}
