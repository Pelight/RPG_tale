using UnityEngine;

public class WallJumpState : PlayerState
{
    public WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpForce.x * -player.facingDirection, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if(rb.linearVelocityY < 0)
            stateMachine.ChangeState(player.fallState);

        if(player.isWall)
            stateMachine.ChangeState(player.wallSlideState);

        if(input.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.jumpAttackState);
    }
}
