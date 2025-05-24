using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    private GameObject target; // ī�޶� ����ٴ� ���
    private Vector3 offset = new Vector3(0.0f, 10.0f, -5.0f); // ĳ���Ϳ��� �󸶳� �������� ī�޶� ������
    private Vector3 cameraRotation = new Vector3(60.0f, 0.0f, 0.0f);
    private float followSpeed = 5.0f; // ī�޶� ����ٴϴ� ���ǵ�

    private void Awake()
    {
        target = GameObject.Find("PlayerCharacter");
    }

    private void FixedUpdate()
    {
        Vector3 offsetPos = target.transform.position + offset;
        // ���� ī�޶� ��ġ�� ������ ��ġ���� ������ ���� �������� �ε巴�� ǥ��
        transform.position = Vector3.Lerp(transform.position, offsetPos, followSpeed * Time.fixedDeltaTime);
        // ī�޶� �þ߰��� ��¦ ������� ���̰�
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
