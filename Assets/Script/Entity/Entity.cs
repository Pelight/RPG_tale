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
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform groundCheck;
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

    public void CurrentStateAnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new (xVelocity,yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
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
        isGrounded = Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance, groundLayer);

        if(secondWallCheck != null)
        {
            isWall = Physics2D.Raycast(firstWallCheck.position,Vector2.right * facingDirection,wallCheckDistance, groundLayer) 
                && Physics2D.Raycast(secondWallCheck.position,Vector2.right * facingDirection,wallCheckDistance, groundLayer);
        }
        else
            isWall = Physics2D.Raycast(firstWallCheck.position,Vector2.right * facingDirection,wallCheckDistance, groundLayer) ;
        
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0,-groundCheckDistance));
        Gizmos.DrawLine(firstWallCheck.position, firstWallCheck.position + new Vector3(wallCheckDistance * facingDirection,0));
        if(secondWallCheck != null)
            Gizmos.DrawLine(secondWallCheck.position, secondWallCheck.position + new Vector3(wallCheckDistance * facingDirection,0));
    }
}
