using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    private GameObject target; // 카메라가 따라다닐 대상
    private Vector3 offset = new Vector3(0.0f, 10.0f, -5.0f); // 캐릭터에서 얼마나 떨어질지 카메라 오프셋
    private Vector3 cameraRotation = new Vector3(60.0f, 0.0f, 0.0f);
    private float followSpeed = 5.0f; // 카메라가 따라다니는 스피드

    private void Awake()
    {
        target = GameObject.Find("PlayerCharacter");
    }

    private void FixedUpdate()
    {
        Vector3 offsetPos = target.transform.position + offset;
        // 원래 카메라 위치와 오프셋 위치와의 보간을 통해 움직임을 부드럽게 표현
        transform.position = Vector3.Lerp(transform.position, offsetPos, followSpeed * Time.fixedDeltaTime);
        // 카메라 시야각이 살짝 누운듯이 보이게
        transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
