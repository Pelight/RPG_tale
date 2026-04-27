using System;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeWasInBattle;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if(player == null)
            player = enemy.PlayerDetected().transform;

        if(ShouldRetreat())
        {
            rb.linearVelocity = new(enemy.retreatVelocity.x * -DirectionToPlayer(),enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if(enemy.PlayerDetected())
            UpdateBattleTimer();

        if(BattleIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if(WithinAttackRange() && enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.attackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(),rb.linearVelocityY);
    }

    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;
    private bool BattleIsOver() => Time.time > (lastTimeWasInBattle + enemy.battleTimeDuration);

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;
    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    private float DistanceToPlayer()
    {
        if(player == null)
            return float.MaxValue;

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }
 
    private int DirectionToPlayer()
    {
        if(player == null)
            return 0;
        
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
