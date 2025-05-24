using UnityEngine;

// 플레이어 캐릭터 클래스
public class PlayerCharacter : MonoBehaviour
{
    private Joystick joystick;
    private Rigidbody rigidBody;
    private Animator characterAnimation;

    private Vector2 keyboardInput = Vector2.zero; // 키보드 + -
    private Vector3 moveForward = Vector3.zero; // 키보드 위, 아래 판정용
    private Vector3 moveRight = Vector3.zero; // 키보드 왼, 오 판정용

    private float moveSpeed = 5.0f; // 현재 움직이는 속도
    private float rotationSpeed = 5.0f; // 회전 할 때 속도


    // 유니티 함수

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        characterAnimation = GetComponentInChildren<Animator>();
        joystick = GameObject.Find("Joystick").GetComponent<Joystick>();
    }

    private void OnEnable()
    {
       
    }

    private void FixedUpdate()
    {
        Move();
        characterAnimation.SetBool("IsMove", joystick.Direction() != Vector3.zero);
    }

    private void OnDisable()
    {
        
    }

    // 일반 함수
    private void Move()
    {
        // 조이스틱에서 방향이 없을 경우 패스
        if (joystick.Direction() != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(joystick.Direction());
            // 부드러운 회전을 위해 보간
            Quaternion lerpRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
            rigidBody.MoveRotation(lerpRotation);
        }
        // 조이스틱에서 방향을 받아와서 이동
        rigidBody.MovePosition(transform.position + joystick.Direction() * moveSpeed * Time.fixedDeltaTime);
    }
}
