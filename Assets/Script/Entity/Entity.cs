using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Definition")]
    public Animator anim{get ; private set;}
    public Rigidbody2D rb{get; private set;}
    protected StateMachine stateMachine;
    
    private bool facingRight = true;
    public int facingDirection {get; private set;} = 1;

    [Header("Collision detector")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform firstWallCheck;
    [SerializeField] private Transform secondWallCheck;
    public bool isGrounded{get; private set;}
    public bool isWall{get; private set;}

    protected virtual void Awake() 
    {
        //get components
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //definition
        stateMachine = new StateMachine();
    }
    protected virtual void Start(){}
    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void CallAnimationTrigger() => stateMachine.currentState.CallAnimationTrigger();
    
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new (xVelocity,yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if(xVelocity > 0 && !facingRight)
            FlipRotation();
        else if(xVelocity < 0 && facingRight)
            FlipRotation();
    }

    public void FlipRotation()
    {
        transform.Rotate(0,180,0);
        facingRight = !facingRight;
        facingDirection *= -1;
    }

    private void HandleCollisionDetection()
    {
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance, groundLayer);
        isWall = Physics2D.Raycast(firstWallCheck.position,Vector2.right * facingDirection,wallCheckDistance, groundLayer) 
              && Physics2D.Raycast(secondWallCheck.position,Vector2.right * facingDirection,wallCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,-groundCheckDistance));
        Gizmos.DrawLine(firstWallCheck.position, firstWallCheck.position + new Vector3(wallCheckDistance * facingDirection,0));
        Gizmos.DrawLine(secondWallCheck.position, secondWallCheck.position + new Vector3(wallCheckDistance * facingDirection,0));
    }
}
