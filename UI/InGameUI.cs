using UnityEngine;
using UnityEngine.SceneManagement;

// �ΰ��ӿ��� ���Ǵ� main UI
public class InGameUI : MonoBehaviour
{
    // �ɼ� ���� �г�
    private GameObject optionalUI;

    private void Awake()
    {
        optionalUI = GameObject.Find("OptionUIPanel");
    }

    // X��ư�� Ŭ���� â �ݱ�
    public void OnXButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    // ����� ��ư
    public void OnRetryButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        SceneManager.LoadScene("MainGame");
    }

    // ���� �޴� ��ư
    public void OnMainMenuButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LobbyScene");
    }

    // �ɼ� ��ư
    public void OnOptionButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        optionalUI.SetActive(true);
        gameObject.SetActive(false);
    }

    // ���ӿ��� ������ ��ư
    public void OnExitButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Application.Quit();
    }
}
