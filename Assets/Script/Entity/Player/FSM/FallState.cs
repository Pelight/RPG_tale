using UnityEngine;

public class FallState : AirState
{
    public FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(player.isGrounded)
            stateMachine.ChangeState(player.idleState);

        if(player.isWall)
            stateMachine.ChangeState(player.wallSlideState);
    }
}
