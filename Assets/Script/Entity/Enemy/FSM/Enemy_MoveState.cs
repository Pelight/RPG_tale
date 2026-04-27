using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(!enemy.isGrounded || enemy.isWall)
            enemy.FlipRotation();
    } 


    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection,rb.linearVelocityY);

        if(!enemy.isGrounded || enemy.isWall)
            stateMachine.ChangeState(enemy.idleState);    
        
    }
}
