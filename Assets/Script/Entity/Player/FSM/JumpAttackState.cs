using UnityEngine;

public class JumpAttackState : PlayerState
{

    private bool touchedGround;
    public JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        touchedGround = false;
        player.SetVelocity(player.jumpAttackVelocity.x * player.facingDirection, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if(player.isGrounded && !touchedGround){
            touchedGround = true;
            anim.SetTrigger("JumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocityY);
        }
    
        if(triggerCalled && player.isGrounded)
            stateMachine.ChangeState(player.idleState);
    }

}
