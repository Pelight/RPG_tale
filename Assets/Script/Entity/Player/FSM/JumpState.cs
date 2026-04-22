using UnityEngine;

public class JumpState : AirState
{
    public JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocityX , player.jumpForce);
    }
    public override void Update()
    {
        base.Update();

        if(rb.linearVelocityY < 0 && stateMachine.currentState != player.jumpAttackState) 
            stateMachine.ChangeState(player.fallState);
    }
}
