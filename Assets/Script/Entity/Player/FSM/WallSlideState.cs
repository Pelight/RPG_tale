using Unity.VisualScripting;
using UnityEngine;

public class WallSlideState : PlayerState
{
    public WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Update()
    {
        base.Update();
        HandleWallSlide();


        if(input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);
 
        if(!player.isWall)
            stateMachine.ChangeState(player.fallState);
        
        if(player.isGrounded){
            stateMachine.ChangeState(player.idleState);
            if(player.facingDirection != player.moveInput.x)
                player.FlipRotation();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x,rb.linearVelocityY);
        else
            player.SetVelocity(player.moveInput.x,rb.linearVelocityY * player.wallSlideMultiplier);
    }
}
