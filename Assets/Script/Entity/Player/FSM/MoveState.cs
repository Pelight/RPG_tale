using UnityEngine;

public class MoveState : GroundedState
{
    public MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocityY);

        if(player.moveInput.x == 0 || player.isWall)
            stateMachine.ChangeState(player.idleState);
    }
}
