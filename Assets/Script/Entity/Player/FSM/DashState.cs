using UnityEngine;

public class DashState : PlayerState
{
    private float direction;
    private float rbGravityScale;
    private bool QueuedAttack;
    public DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        rbGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        direction = player.dashInput;
        QueuedAttack = false;
    }

    public override void Update()
    {
        base.Update();
        CancelDashIfNeed();

        player.SetVelocity(player.dashSpeed * direction,0);
        
        if(input.Player.Attack.WasPressedThisFrame())
            QueuedAttack = true;

        if(stateTimer < 0)
        {
            if(player.isGrounded)    
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }
    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0,0);
        rb.gravityScale = rbGravityScale;
    }
    public void CancelDashIfNeed()
    {
        if(QueuedAttack)
            player.EnterDashAttackState();
        
        if(player.isWall)
        {
            if(player.isGrounded)
                    stateMachine.ChangeState(player.idleState);
                else
                    stateMachine.ChangeState(player.wallSlideState);
        }

    }
}
