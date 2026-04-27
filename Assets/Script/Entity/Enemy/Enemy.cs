using UnityEngine;

public class Enemy : Entity
{
    [Header("States")]
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

    [Header("Battle details")]
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2f;


    [Header("Movement details")]
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;
    [Range(0,2)]
    public float animSpeedMultiplier = 1;

    [Header("Player detection")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;


    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = 
            Physics2D.Raycast(playerCheck.position,Vector2.right * facingDirection, playerCheckDistance, playerLayer | groundLayer);
        
        if(hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;
        
        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position,new Vector3(playerCheck.position.x + facingDirection * playerCheckDistance,playerCheck.position.y ));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position,new Vector3(playerCheck.position.x + facingDirection * attackDistance,playerCheck.position.y ));
    }

}
