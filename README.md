# ZombieSurvival
* 플레이 영상 : <https://youtu.be/WEAWCcp3GR4>
* 다운로드 : <https://naver.me/GJ5cFw67>
  
<img src="Image/Echo.png" width="600" height="350"/>

***

* 작업 기간 : 2024. 07. 01 ~ 2024. 08. 08 (1달)
* 인력 구성 : 4명
* 담당 파트 : 캐릭터
* 사용언어 및 개발환경 : C#, Unity
  
# 조작법    
* W, A, S, D : 이동
* F : 상호작용
* Space bar : 지팡이 찍기
  
# New Input System
* PC와 Mobile 크로스 플랫폼에서 움직임 구현을 위해 New Input System 사용
  
* PC

<img src="Image/PC.gif" width="600" height="350"/>

* Mobile
  
<img src="Image/Mobile.gif" width="600" height="350"/>

* New Input System
  
<img src="Image/NewInputSystem.png" width="600" height="350"/>

<details>
<summary> New Input System 이벤트 바인딩 함수</summary>
	
```cs
// 이벤트 바인딩 함수들

// 캐릭터 키보드 입력이 들어올 때 방향을 전달받음
public void OnMove(InputAction.CallbackContext context)
{
    keyboardInput = context.ReadValue<Vector2>();
}

// PC용 마우스 입력받을 때 카메라 회전
public void OnMouse(InputAction.CallbackContext context)
{
    if (characterStop) return;

    Vector2 mousePosition = context.ReadValue<Vector2>();

    float trunPlayer = mousePosition.x * mouseSensitivity * Time.deltaTime;
    MouseX += trunPlayer;

    if (MouseX > 360) MouseX -= 360.0f;
    if (MouseX < 0) MouseX += 360.0f;

    MouseY -= mousePosition.y * mouseSensitivity * Time.deltaTime;
    MouseY = Mathf.Clamp(MouseY, -90f, 50f); //Clamp를 통해 최소값 최대값을 넘지 않도록함

    characterCamera.transform.rotation = Quaternion.Euler(MouseY, MouseX, 0.0f);// 각 축을 한꺼번에 계산
    transform.rotation = Quaternion.Euler(0.0f, MouseX, 0.0f);
}

// 모바일용 터치 조이스틱을 사용하기 때문에 달리기 함수를 따로 만듬
public void OnRunMobile(InputAction.CallbackContext context)
{
    if (IsRun && context.performed)
    {
        IsRun = false;
        runButton.isRun(false);
        return;
    }

    if (context.performed)
    {
        IsRun = true;
        runButton.isRun(true);
        return;
    }
}

// 달리기 버튼을 입력받을 때 상황 체크
public void OnRun(InputAction.CallbackContext context)
{
    if (keyboardInput.y >= 0)
    {
        WalkRun(context.performed);
    }
}

// 상호작용키를 입력받을 때 키패드와 문에서 상호작용을 다르게 처리
public void OnInteraction(InputAction.CallbackContext context)
{
    if (isKeyPadSight && context.performed)
    {
        keypadCollisionCheck.OneCheckInteraciton = false;
        isKeyPadSight = false;
        IsEscape = true;

        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        keypad.ResetUserInput();
        return;
    }
    else
    {
        IsInteraction = context.performed;
    }

    if (context.performed)
    {
        StartCoroutine(interactObject.ClickedButton());
    }

}

// 바닥을 찍을 때 애니메이션 판정
public void OnStamping(InputAction.CallbackContext context)
{
    if (IsWalk) return;
    if (characterStop) return;

    if (context.performed)
    {
        characterAnimation.SetBool("IsStamping", true);
        return;
    }
}
```

</details>

# Character Movement
* 캐릭터의 기본적인 움직임과 캐릭터와 상호작용 하는 오브젝트(카메라, 문, 스태미나, 키패드, 에코, 지팡이)의 연결 구현

<details>
<summary> Character Movement 코드</summary>
	
```cs
public class MovePlayer : MonoBehaviour
{
    // 사용자 정의, 유니티 클래스

    // 키패드 비밀번호 리셋용
    [SerializeField]
    private GameObject keypadDark;
    // F키 UI 활성, 비활성용
    [SerializeField]
    private GameObject startDoorUI;
    [SerializeField]
    private GameObject endDoorUI;
    // 달리기 버튼과 연결용
    [SerializeField]
    private RunButton runButton;

    private Keypad keypad;
    private Camera characterCamera;
    private Rigidbody rigidBody;
    private Animator characterAnimation;
    private FollowCamera followCamera;
    // PC용 NewInputSystem
    private PlayerNewInput playerNewInput;
    // Mobile용 NewInputSystem
    private PlayerInputMobile playerInputMobile;
    // 달리기 이벤트 함수 등록용
    private InputAction runAction;
    // Mobile용 카메라 회전 
    private InputAction lookLeftAction;
    // Mobile용 카메라 회전 
    private InputAction lookRightAction;
    // 버튼 클릭할 때 소리 한 번만 나게
    private InteractObject interactObject;
    // 키패드랑 충돌 났는지 체크용
    private KeypadCollisionCheck keypadCollisionCheck;

    public event PropertyChangedEventHandler PropertyChanged;

    // 스태미너 UI 인스펙터 등록
    public StaminaUI staminaUI;

    // Vector나 기본 자료형
    private Vector3 moveDirection = Vector3.zero; // 현재 방향 벡터
    public Vector3 MoveDirection
    {
        get { return moveDirection; }
        set { moveDirection = value; }
    }

    [SerializeField]
    private Vector3 rayUp = Vector3.zero; // 현재 캐릭터 위치에서 살짝 위에 포지션을 주기 위해
    private Vector2 keyboardInput = Vector2.zero; // 키보드 + -
    private Vector3 moveForward = Vector3.zero; // 키보드 위, 아래 판정용
    private Vector3 moveRight = Vector3.zero; // 키보드 왼, 오 판정용
    private Vector3 prevPlayerPos = Vector3.zero; // 키보드 왼, 오 판정용

    [SerializeField]
    private float currentStamina = 0.0f;

    public float CurrentStamina
    {
        set
        {
            currentStamina = value;
            OnPropertyChanged("CurrentStamina");
        }
        get { return currentStamina / fullStamina; }
    }

    [SerializeField]
    private float mouseSensitivity = 15.0f; // 마우스 감도

    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    [SerializeField]
    private bool isEcho = true; // 에코가 켜져 있는지 여부

    public bool IsEcho
    {
        get { return isEcho; }
        set { isEcho = value; }
    }

    // true일 경우 캐릭터의 움직임이 멈추게
    private bool characterStop = false;

    public bool CharacterStop
    {
        get { return characterStop; }
        set { characterStop = value; }
    }

    // 키패드를 보고 있는 시야인지
    private bool isKeyPadSight = false;

    public bool IsKeyPadSight
    {
        get { return isKeyPadSight; }
        set { isKeyPadSight = value; }
    }

    private bool isInteraction = false; // 키패드를 확대하고 있는지 여부

    public bool IsInteraction
    {
        get { return isInteraction; }
        set { isInteraction = value; }
    }

    [SerializeField]
    private float defaultSpeed = 5.0f; // 초기 스피드
    [SerializeField]
    private float moveSpeed = 5.0f; // 현재 움직이는 속도
    [SerializeField]
    private float runSpeed = 7.0f; // 달리기 속도
    [SerializeField]
    private float consumeStaminaRate = 20.0f; // 스태미나 소비 비율
    [SerializeField]
    private float recoveryStaminaRate = 10.0f; // 스태미나 회복 비율
    [SerializeField]
    private float fullStamina = 200.0f; // 총 스태미나

    private float MouseX = 0.0f;
    private float MouseY = 0.0f;
    private float time = 0.0f;
    private bool IsWalk = false;
    private bool IsEscape = false;
    private bool IsBorder = false;
    private bool IsStaminaZero = false;
    private bool IsUI = false;
    private bool IsRun = false;

    private void Awake()
    {
        playerNewInput = new PlayerNewInput();
        playerInputMoblie = new PlayerInputMobile();
        TryGetComponent(out rigidBody);
        characterAnimation = GetComponentInChildren<Animator>();
        characterCamera = Camera.main;
        followCamera = characterCamera.GetComponent<FollowCamera>();
        keypad = keypadDark.GetComponent<Keypad>();
        interactObject = GetComponent<InteractObject>();
        keypadCollisionCheck = keypad.GetComponent<KeypadCollisionCheck>();
    }

    // NewInputSystem 과 모바일용 이벤트 등록
    private void OnEnable()
    {
        runAction = playerNewInput.PlayerActions.Run;
        runAction.Enable();
        runAction.performed += OnRun;

        lookLeftAction = playerInputMoblie.PlayerAction.LookLeft;
        lookLeftAction.Enable();
        lookLeftAction.performed += OnLookLeft;

        lookRightAction = playerInputMoblie.PlayerAction.LookRight;
        lookRightAction.Enable();
        lookRightAction.performed += OnLookRight;
    }

    // 위의 이벤트 해제
    private void OnDisable()
    {
        runAction.Disable();
        lookLeftAction.Disable();
        lookRightAction.Disable();
    }

    private void Start()
    {
        CurrentStamina = fullStamina;
        MouseX = transform.localEulerAngles.y;
    }

    private void Update()
    {
        PauseUI();

        // 각종 행동 취소
        if (IsEscape)
        {
            IsEscape = false;
            characterStop = false;
            followCamera.IsTargetKeypad = false;
            VisibleMousePointer(false);
        }

        // 스태미너 계산
        //staminaUI.SetValue(currentStamina / fullStamina);
    }

    // 각종 물리 계산(벽 체크, 움직임, 모바일용 카메라)
    private void FixedUpdate()
    {
        if (characterStop) return;

        moveDirection = Vector3.zero;
        time += Time.deltaTime;

        StopToWall();
        UICheck();
        Move();
        MoblieCameraLook();

        characterAnimation.SetBool("IsWalk", moveDirection != Vector3.zero);
        IsWalk = characterAnimation.GetBool("IsWalk");

        if (IsWalk)
        {
            characterAnimation.SetBool("IsStamping", false);
        }

        // 실시간 Run 체크

        if (IsRun)
        {
            if (keyboardInput.y >= 0)
            {
                if (moveDirection != Vector3.zero)
                {
                    WalkRun(true);
                    StaminaSystem(true);
                    staminaUI.gameObject.SetActive(true);
                    GameManager.Instance.PlayerPresenter.progressUIView = staminaUI;
                }
            }

            if (keyboardInput.y < 0)
            {
                WalkRun(false);
            }
        }
        else
        {
            StaminaSystem(false);
            staminaUI.gameObject.SetActive(false);
            GameManager.Instance.PlayerPresenter.progressUIView = null;
        }

        // PC버전 달리기 로직 주석
        /*   if (runAction.ReadValue<float>() > 0)
           {
               if (keyboardInput.y >= 0)
               {
                   if (moveDirection != Vector3.zero)
                   {
                       WalkRun(true);
                       StaminaSystem(true);
                       staminaUI.gameObject.SetActive(true);
                       GameManager.Instance.PlayerPresenter.progressUIView = staminaUI;
                   }
               }

               if (keyboardInput.y < 0)
               {
                   WalkRun(false);
               }
           }
           else
           {
               StaminaSystem(false);
               staminaUI.gameObject.SetActive(false);
               GameManager.Instance.PlayerPresenter.progressUIView = null;
           }*/

        // 애니메이션이 전부 끝났을 때를 체크 끝났을 때 false로 돌려주어 다시 반복되게
        if (characterAnimation.GetCurrentAnimatorStateInfo(1).IsName("RightHand Layer.Stamping")
       && characterAnimation.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f)
        {
            characterAnimation.SetBool("IsStamping", false);
        }
    }

    // 사용자 정의 함수
    // 공개용

    // 스태미나 초기화
    public void InitStamina()
    {
        CurrentStamina = fullStamina;
    }

    // 플레이어 위치 저장용
    public void SavePlayerPos()
    {
        prevPlayerPos = transform.position;
    }

    // 플레이어 위치 불러오기용
    public Vector3 LoadPlayerPos()
    {
        return prevPlayerPos;
    }

    // 이벤트 영상이 끝난다음 포지션 원래대로 되돌리기
    public void MousePosFront()
    {
        MouseX = 90;
        MouseY = 0;
    }

    // 에코 사용여부 판정
    public void UseEcho(bool use)
    {
        if (use)
        {
            IsEcho = true;
        }
        else
        {
            IsEcho = false;
        }
    }

    // 마우스 포인터 키패드때는 보이게 아닐땐 감추기
    public void VisibleMousePointer(bool visible)
    {
        if (visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // 에코를 멈추는 메서드
    public void StopEcho(Transform EchoPos)
    {
        var t1 = GameManager.Instance.ObjectPool.Get(PoolingType.PlayerEcho);
        t1.Activate(EchoPos);
        GameManager.Instance.ObjectPool.Return(t1);
    }

    // 걸을 때 에코를 쏘는 메서드
    public void WalkEcho(Transform EchoPos)
    {
        var t1 = GameManager.Instance.ObjectPool.Get(PoolingType.PlayerEcho);
        t1.Activate(EchoPos);
        GameManager.Instance.ObjectPool.Return(t1);
    }

    // 달릴 때 에코를 쏘는 메서드
    public void RunEcho(Transform EchoPos)
    {
        var t1 = GameManager.Instance.ObjectPool.Get(PoolingType.PlayerRunEcho);
        t1.Activate(EchoPos);
        GameManager.Instance.ObjectPool.Return(t1);
    }

    // 지팡이와 관련되서 에코를 쏘는 메서드
    public void CaneEcho(Transform EchoPos)
    {
        var t1 = GameManager.Instance.ObjectPool.Get(PoolingType.PlayerEcho);
        t1.Activate(EchoPos);
        GameManager.Instance.ObjectPool.Return(t1);
    }

    // 비공개

    private void PauseUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            GameManager.Instance.UIManager.ShowUI<PauseUI>("UI/Pause UI");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            GameManager.Instance.UIManager.CloaseCurrentUI();
        }
    }

    // 벽에 닿았는지 판정
    private void StopToWall()
    {
        if (Physics.Raycast(transform.position + rayUp, transform.forward, 2.5f, LayerMask.GetMask("Wall")) ||
             Physics.Raycast(transform.position + rayUp, transform.right, 1.0f, LayerMask.GetMask("Wall")) ||
           Physics.Raycast(transform.position + rayUp, -transform.right, 1.0f, LayerMask.GetMask("Wall")))
        {
            IsBorder = true;
        }
        else
        {
            IsBorder = false;
        }
    }

    private void UICheck()
    {
        IsUI = Physics.Raycast(transform.position + rayUp, transform.forward, 5.0f, LayerMask.GetMask("UI"));

        if (IsUI)
        {
            startDoorUI.SetActive(true);
            endDoorUI.SetActive(true);
        }
        else
        {
            startDoorUI.SetActive(false);
            endDoorUI.SetActive(false);
        }
    }

    // 걸을때와 달릴 때 전환용 메서드
    private void WalkRun(bool run)
    {
        if (run)
        {
            moveSpeed = runSpeed;
            characterAnimation.SetBool("IsRun", true);
            return;
        }
        else
        {
            moveSpeed = defaultSpeed;
            characterAnimation.SetBool("IsRun", false);
        }
    }

    // 스태미나 전체적으로 관리
    private void StaminaSystem(bool run)
    {
        if (IsStaminaZero)
        {
            float maxStamina = currentStamina + (rEchoveryStaminaRate * Time.deltaTime);
            CurrentStamina = Mathf.Min(maxStamina, fullStamina);
            WalkRun(false);

            if (currentStamina >= 20) IsStaminaZero = false;
            return;
        }

        if (run)
        {
            float minStamina = currentStamina - (cousumeStaminaRate * Time.deltaTime);
            CurrentStamina = Mathf.Max(minStamina, 0);
        }
        else
        {
            float maxStamina = currentStamina + (rEchoveryStaminaRate * Time.deltaTime);
            CurrentStamina = Mathf.Min(maxStamina, fullStamina);
        }

        if (currentStamina <= 1)
        {
            IsStaminaZero = true;
        }
    }

    // 움직임 전체적으로 관리
    private void Move()
    {
        moveForward = keyboardInput.y * transform.forward;
        moveRight = keyboardInput.x * transform.right;


        if (IsBorder && moveSpeed > defaultSpeed)
        {
            moveSpeed = defaultSpeed;
        }

        moveDirection = moveForward + moveRight;
        moveDirection.Normalize();

        rigidBody.MovePosition(this.gameObject.transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    // 모바일때 화면 설정
    private void MobileCameraLook()
    {
        if (lookLeftAction.ReadValue<float>() > 0)
        {
            Look(false, lookLeftAction);
        }
        else if (lookRightAction.ReadValue<float>() > 0)
        {
            Look(true, lookRightAction);
        }
    }

    // 카메라 좌, 우 버튼 입력이 들어왔을 때 작동하는 함수 좌 - 우 +
    private void Look(bool direction, InputAction action)
    {
        if (characterStop) return;

        float lookPos = action.ReadValue<float>();

        float turnPlayer = lookPos * mouseSensitivity * Time.deltaTime;

        if (direction)
        {
            MouseX += trunPlayer;
        }
        else
        {
            MouseX -= trunPlayer;
        }

        if (MouseX > 360) MouseX -= 360.0f;
        if (MouseX < 0) MouseX += 360.0f;

        characterCamera.transform.rotation = Quaternion.Euler(MouseY, MouseX, 0.0f);// 각 축을 한꺼번에 계산
        transform.rotation = Quaternion.Euler(0.0f, MouseX, 0.0f);
    }
}

```

</details>

# Cane Animation
* 동작이 정확히 일치하는 애니메이션이 없어 지팡이 애니메이션과 앞으로 이동하는 애니메이션을 블렌딩

<img src="Image/Cane.gif" width="600" height="350"/>

# Camera
* 일인칭 구현을 위해 머리 머터리얼을 제거하고 캐릭터 계층에서 머리 본을 불러와 카메라가 따라다니게 함

<img src="Image/Head.png" width="600" height="350"/>

<details>
<summary> Follow Camera 코드</summary>
	
```cs
public class FollowCamera : MonoBehaviour
{
    // 카메라는 플레이어 머리를 기준으로 따라다님
    // 키패드를 보여줘야 하는 용도도 있기 때문에 키패드용 변수도 선언
    [SerializeField]
    private GameObject targetPlayer;
    [SerializeField]
    private GameObject targetKeypad;
    [SerializeField]
    private Vector3 keypadOffset;

    private MovePlayer playerCharacter;
    private GameObject playerHead;
    private GameObject target;
    private Camera cameraComponent;

    private bool isTargetKeypad;

    public bool IsTargetKeypad
    {
        get { return isTargetKeypad; }
        set { isTargetKeypad = value; }
    }

    // 카메라 시야각 조절용 프로퍼티
    [SerializeField]
    private float cameraFOV = 60.0f;

    public float CameraFOV
    {
        get { return cameraFOV; }
        set { cameraFOV = value; }
    }

    void Awake()
    {
        cameraComponent = GetComponent<Camera>();
        cameraComponent.fieldOfView = CameraFOV;
        playerCharacter = targetPlayer.GetComponent<MovePlayer>();
        FindChildBone(playerCharacter.transform, "head");
        target = targetPlayer;
        playerCharacter.VisibleMousePointer(true);
    }

    // 키패드가 타겟이 되었을때와 플레이어가 타겟이 되었을때를 분리
    void Update()
    {
        // 카메라 FOV SET되면 설정
        cameraComponent.fieldOfView = CameraFOV;

        if (isTargetKeypad)
        {
            target = targetKeypad;
            transform.position = target.transform.position + target.transform.forward * keypadOffset.x;
            transform.LookAt(target.transform);
        }
        else
        {
            target = targetPlayer;
            transform.position = playerHead.transform.position;
        }
    }

    // 계층에서 자식이 여러개일 경우 찾는 용
    void FindChildBone(Transform node, string boneName)
    {
        if (node.name == boneName)
        {
            playerHead = node.gameObject;
           
            return;
        }

        foreach (Transform child in node)
        {
            FindChildBone(child, boneName);
        }
    }
}

```

</details>

# QTE(Quick Time Event)
* 간단한 이벤트 상황 처리를 위한 QTE를 사용

<img src="Image/QTE.gif" width="600" height="350"/>

<details>
<summary> QTESystem 코드</summary>
	
```cs
public class QTESystem : MonoBehaviour, INotifyPropertyChanged
{
    // QTE Scene을 종료하기 위한 트리거
    [SerializeField]
    private CutSceneTrigger cutSceneTrigger;
   // 프로그래스바 조절용 변수
    [SerializeField]
    private float progressbar = 0.0f;
    [SerializeField]
    private float progressbarInitialValue = 50.0f;
    [SerializeField]
    private float progressbarComplete = 100.0f;
    [SerializeField]
    private float progressbarFail = 0.0f;
    [SerializeField]
    private float QTEReduceRate = 2.5f;
    [SerializeField]
    private float QTERecoveryRate = 5.0f;

    // QTE Scene 시작용 트리거
    [SerializeField]
    private bool QTEStartTrigger = false;

    private bool IsQTEStart = false;

    public event PropertyChangedEventHandler PropertyChanged;

    public float Progressbar
    {
        get { return progressbar; }
        set 
        {
            progressbar = value;
            OnPropertyChanged("Progressbar");
        }
    }

    private void Awake()
    {
        Progressbar = progressbarInitialValue;
    }

    // QTE 이벤트 체크
    private void Update()
    {
        if (QTEStartTrigger)
        {
            QTEStart();
            QTEStartTrigger = false;
        }

        if (IsQTEStart)
        {
            CheckQTE();
        }
        else
        {
            Progressbar = progressbarInitialValue;
        }
    }

    // QTE 시작때 UI를 보여주기
    public void QTEStart()
    {
        GameManager.Instance.UIManager.ShowUI<QTEUI>("UI/QTE UI");
        IsQTEStart = true;
    }

    // QTE 성공과 실패 체크
    void CheckQTE()
    {
        Progressbar = progressbar - (QTEReduceRate * Time.deltaTime);
        
        if (progressbar >= progressbarComplete)
        {
            Progressbar = progressbarInitialValue;
            IsQTEStart = false;
         
            cutSceneTrigger.FinishCutScene();
            GameManager.Instance.UIManager.CloaseCurrentUI();
        }
        else if (progressbar <= progressbarFail)
        {
            Progressbar = progressbarInitialValue;
            IsQTEStart = false;
            
            cutSceneTrigger.TrueEndingScene();
            GameManager.Instance.UIManager.CloaseCurrentUI();
        }
    }

    // 플레이어가 QTE를 눌렀을때 회복
    public void OnQTEPush(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Progressbar += QTERecoveryRate;
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

</details>
