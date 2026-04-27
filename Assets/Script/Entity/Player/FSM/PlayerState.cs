using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();

        if(input.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.dashState);
    }

    public override void UpdateAnimParam()
    {
        base.UpdateAnimParam();
        anim.SetFloat("yVelocity", rb.linearVelocityY);
    }

    public bool CanDash()
    {
        if(player.isWall)
            return false;
        if(stateMachine.currentState == player.dashState)
            return false;
        return true;
    }
}
