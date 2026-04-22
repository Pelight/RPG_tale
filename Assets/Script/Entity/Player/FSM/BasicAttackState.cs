using UnityEngine;

public class BasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    private float lastAttackTime;
    
    private int attackDir;

    private bool comboAttackQueued;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private const int FirstComboIndex = 1;
    public BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if(comboLimit != player.attackVelocity.Length){
            Debug.LogWarning("Not enought comboLimit");    
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ComboCounter();

        attackDir = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDirection;


        anim.SetInteger("BasicAttackIndex", comboIndex);
        ApplyAttackVelocity();

    }

    public override void Update()
    {
        base.Update();
        HandleBasicAttack();

        if(input.Player.Attack.WasPressedThisFrame())
            QueuedNextAttack();

        if(triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastAttackTime = Time.time;
    }

    private void QueuedNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued){
                anim.SetBool(animBoolName,false);
                player.EnterComboState();
            }    
            else    
                stateMachine.ChangeState(player.idleState);
    }

    private void HandleBasicAttack()
    {
        attackVelocityTimer -= Time.deltaTime;

        if(attackVelocityTimer<0)
            player.SetVelocity(0, rb.linearVelocityY);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDir, attackVelocity.y);
    }

    private void ComboCounter()
    {
        if(Time.time > lastAttackTime + player.comboResetTimer)
            comboIndex = FirstComboIndex;
        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;
    }
}
