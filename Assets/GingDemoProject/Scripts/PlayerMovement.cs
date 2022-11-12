using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    
    public Transform TrPlayer;
    public float Speed = 5f;
    public float Gravity = -9.81f;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;
    public Vector3 Drag;
    public CharacterController _controller;
    private Vector3 _velocity;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private float HorizontalInput;
    private float VerticalInput;
    public bool canMove = false;
    public bool _enable = false;
    [SerializeField] private InputData inputData;
    public Animator anim;
    [SerializeField] public Collider _mainCol;

    private void OnEnable()
    {
        EventManager.onInputChange += SetPlayerInput;
    }
    
    private void OnDisable()
    {
        EventManager.onInputChange -= SetPlayerInput;
    }
    
    private void Awake()
    {
        TrPlayer = GetComponent<Transform>();
        _controller = TrPlayer.GetComponent<CharacterController>();
        _mainCol = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }
    
    public void Start()
    {
        _groundChecker = TrPlayer.transform;
        _enable = true;
        _mainCol.enabled = true;
    }
    
    public void Update()
    {
        if (canMove)
        {
            _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
            if (_isGrounded && _velocity.y < 0)
                _velocity.y = 0f;
            Vector3 move = new Vector3(VerticalInput, 0, HorizontalInput);
            if(_controller.enabled)
                _controller.Move(move * Time.deltaTime * Speed);
            if (move != Vector3.zero)
                TrPlayer.transform.forward = move;
            _velocity.y += Gravity * Time.deltaTime;
            _velocity.x /= 1 + Drag.x * Time.deltaTime;
            _velocity.y /= 1 + Drag.y * Time.deltaTime;
            _velocity.z /= 1 + Drag.z * Time.deltaTime;
            if(_controller.enabled)
                _controller.Move(_velocity * Time.deltaTime);
        }
    }
    
    void SetPlayerInput(bool isMoving)
    {
        HorizontalInput = inputData.InputVector.x;
        VerticalInput = inputData.InputVector.y;
        canMove = true;
        if (isMoving)
        {
            if(HorizontalInput > 0 || HorizontalInput < 0 && VerticalInput > 0 || VerticalInput < 0){anim.SetBool("Walk", true);}
        }
        else
        {
            //PlayerAnimSet();
            anim.SetBool("Walk", false);
        }
    }
    
    private void PlayerAnimSet()
    {
        if (inputData.isChase)
        {
            AnimationOnOff(true, "Running");
        }
        else
        {
            AnimationOnOff(false, "Running");
        }
    }
    
    private void AnimationOnOff(bool onoff, string animName)
    {
        anim.SetBool(animName, onoff);
    }
}
