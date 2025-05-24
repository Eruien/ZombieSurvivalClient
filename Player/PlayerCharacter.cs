using UnityEngine;

// �÷��̾� ĳ���� Ŭ����
public class PlayerCharacter : MonoBehaviour
{
    private Joystick joystick;
    private Rigidbody rigidBody;
    private Animator characterAnimation;

    private Vector2 keyboardInput = Vector2.zero; // Ű���� + -
    private Vector3 moveForward = Vector3.zero; // Ű���� ��, �Ʒ� ������
    private Vector3 moveRight = Vector3.zero; // Ű���� ��, �� ������

    private float moveSpeed = 5.0f; // ���� �����̴� �ӵ�
    private float rotationSpeed = 5.0f; // ȸ�� �� �� �ӵ�


    // ����Ƽ �Լ�

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

    // �Ϲ� �Լ�
    private void Move()
    {
        // ���̽�ƽ���� ������ ���� ��� �н�
        if (joystick.Direction() != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(joystick.Direction());
            // �ε巯�� ȸ���� ���� ����
            Quaternion lerpRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
            rigidBody.MoveRotation(lerpRotation);
        }
        // ���̽�ƽ���� ������ �޾ƿͼ� �̵�
        rigidBody.MovePosition(transform.position + joystick.Direction() * moveSpeed * Time.fixedDeltaTime);
    }
}
