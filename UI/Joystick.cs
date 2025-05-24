using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// PC, ����� ����� ȯ���� ���� ���̽�ƽ Ŭ����
public class Joystick : MonoBehaviour
{
    // ���̽�ƽ�� �ڵ� ���̽�ƽ Ʈ������
    private Image joystick;
    private Image handle;
    private RectTransform joystickRectTransform;

    // ���� ��ũ�� ������� ���� ��ũ�� ����� �ҷ��� ĵ���� �������� ����
    private Vector2 originalScreenSize = new Vector2(800.0f, 600.0f);
    private Vector2 startMousePos = Vector2.zero;
    private Vector2 mouseDir = Vector2.zero;
    private bool IsDrag = false;
    private float joystickScaleFactor = 0.0f;
    private float joystickRectHalfSize = 0.0f;

    private void Awake()
    {
        joystick = GetComponent<Image>();
        handle = transform.GetChild(0).GetComponent<Image>();
        joystickRectTransform = joystick.gameObject.GetComponent<RectTransform>();

        joystickRectHalfSize = joystick.rectTransform.sizeDelta.x / 2;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsDrag = true;
            startMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            IsDrag = false;
            joystick.color = new Color(joystick.color.r, joystick.color.g, joystick.color.b, 0.0f);
            handle.color = new Color(handle.color.r, handle.color.g, handle.color.b, 0.0f);
            mouseDir = Vector2.zero;
            handle.rectTransform.anchoredPosition = Vector2.zero;
        }

        // PC���� �巡�׸� ���� ��
#if UNITY_STANDALONE
         if (IsDrag && !EventSystem.current.IsPointerOverGameObject())
        {
            joystick.color = new Color(joystick.color.r, joystick.color.g, joystick.color.b, 1.0f);
            handle.color = new Color(handle.color.r, handle.color.g, handle.color.b, 1.0f);

            Vector2 currentMousePos = Input.mousePosition;
            mouseDir = currentMousePos - startMousePos;
            mouseDir = (mouseDir.magnitude > 1.0f) ? mouseDir.normalized : mouseDir;

            joystickRectTransform.anchoredPosition = new Vector2(
                startMousePos.x / joystickScaleFactor - joystickRectHalfSize,
                startMousePos.y / joystickScaleFactor - joystickRectHalfSize);

            // �ڵ� �����̱�
            handle.rectTransform.anchoredPosition = new Vector2(
                mouseDir.x * (joystick.rectTransform.sizeDelta.x / 2),
                mouseDir.y * (joystick.rectTransform.sizeDelta.y / 2));
        }

        // Mobile���� �巡�׸� ���� ��
#elif UNITY_IOS || UNITY_ANDROID
        if (IsDrag && !(Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
            joystick.color = new Color(joystick.color.r, joystick.color.g, joystick.color.b, 1.0f);
            handle.color = new Color(handle.color.r, handle.color.g, handle.color.b, 1.0f);

            Vector2 currentMousePos = Input.mousePosition;
            mouseDir = currentMousePos - startMousePos;
            mouseDir = (mouseDir.magnitude > 1.0f) ? mouseDir.normalized : mouseDir;

            joystickRectTransform.anchoredPosition = new Vector2(
                startMousePos.x / joystickScaleFactor - joystickRectHalfSize,
                startMousePos.y / joystickScaleFactor - joystickRectHalfSize);

            // �ڵ� �����̱�
            handle.rectTransform.anchoredPosition = new Vector2(
                mouseDir.x * (joystick.rectTransform.sizeDelta.x / 2),
                mouseDir.y * (joystick.rectTransform.sizeDelta.y / 2));
        }
#endif
    }

    public float Horizontal()
    {
        return mouseDir.x;
    }

    public float Vertical()
    {
        return mouseDir.y;
    }

    public Vector3 Direction()
    {
        return new Vector3(Horizontal(), 0, Vertical());
    }

    // UI ũ�Ⱑ ���� ���� �� ���̽�ƽ�� �������� �˾Ƽ� ����
    void OnRectTransformDimensionsChange()
    {
        joystickScaleFactor = Screen.width / originalScreenSize.x;
    }
}