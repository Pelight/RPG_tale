using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Player : Entity
{
    [Header("Definition")]
    public PlayerInputSet input{get; private set;}
    
    [Header("States")]
    public IdleState idleState{get; private set;}
    public MoveState moveState{get; private set;}
    public JumpState jumpState{get; private set;}
    public FallState fallState{get; private set;}
    public WallSlideState wallSlideState{get; private set;}
    public WallJumpState wallJumpState{get; private set;}
    public DashState dashState{get; private set;}
    public BasicAttackState basicAttackState{get; private set;}
    public JumpAttackState jumpAttackState{get; private set;}

    [Header("Attack details")]
    public Vector2[] attackVelocity ;
    public Vector2 jumpAttackVelocity ;
    public float attackVelocityDuration = .1f;
    public float comboResetTimer = .5f;
    private Coroutine queuedComboCo;
    private Coroutine queuedDashAttackCo;

    [Header("Movement details")]
    public float moveSpeed = 10;
    public float jumpForce = 17;
    public Vector2 wallJumpForce;
    [Range(0,1)]
    public float inAirMoveMultiplier;
    [Range(0,1)]
    public float wallSlideMultiplier;
    public float dashSpeed = 20;
    public float dashDuration = 0.25f;
    [Space]
    public float coyotteTime = 0.15f;
    public Vector2 moveInput{get; private set;}
    public float dashInput{get; private set;}
    


    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();
        

        //States
        idleState = new IdleState(this, stateMachine,"Idle");
        moveState = new MoveState(this, stateMachine,"Move");
        jumpState = new JumpState(this, stateMachine,"Jump_Fall");
        fallState = new FallState(this, stateMachine,"Jump_Fall");
        wallSlideState = new WallSlideState(this, stateMachine,"WallSlide");
        wallJumpState = new WallJumpState(this, stateMachine,"Jump_Fall");
        dashState = new DashState(this, stateMachine,"Dash");
        basicAttackState = new BasicAttackState(this, stateMachine,"BasicAttack");
        jumpAttackState = new JumpAttackState(this, stateMachine,"JumpAttack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        Reload();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        input.Player.Dash.started += ctx => dashInput = ctx.ReadValue<float>();
    }
    private void OnDisable()
    {
        input.Disable();   
    }
    public void EnterComboState()
    {
        if(queuedComboCo != null)
            StopCoroutine(queuedComboCo);

        queuedComboCo = StartCoroutine(EnterComboStateCo());
    }

    private IEnumerator EnterComboStateCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }
    public void EnterDashAttackState()
    {
        if(queuedDashAttackCo != null)
            StopCoroutine(queuedDashAttackCo);

        queuedDashAttackCo = StartCoroutine(EnterDashAttackCo());
    }
    private IEnumerator EnterDashAttackCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(jumpAttackState);
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
