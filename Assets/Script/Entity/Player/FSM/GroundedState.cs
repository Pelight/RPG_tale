
using UnityEngine;

public class GroundedState : PlayerState
{
    private float coyotteJumpTimer;
    private bool falled;
    public GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        falled = false;
    }

    public override void Update()
    {
        base.Update();

        if (!falled)
            coyotteJumpTimer = Time.time;

        if (rb.linearVelocityY<0 && !player.isGrounded ){
            falled = true;
            if (Time.time > player.coyotteTime + coyotteJumpTimer)
                stateMachine.ChangeState(player.fallState);
        }

        if(input.Player.Jump.WasPerformedThisFrame())
            stateMachine.ChangeState(player.jumpState);

        if(input.Player.Attack.WasPerformedThisFrame())
            stateMachine.ChangeState(player.basicAttackState);
        
    }
}
