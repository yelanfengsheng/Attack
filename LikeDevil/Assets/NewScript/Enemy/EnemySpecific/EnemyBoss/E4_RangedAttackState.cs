using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_RangedAttackState : RangedAttackState
{
    private Enemy4 enemy;

    public E4_RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData,Enemy4 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)//动画播放完成后切换状态
        {
            if (enemy.CheckPlayerInMinAggroRange())//如果玩家在最小激活范围内 切换到玩家检测状态
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else//否则切换到寻找玩家状态
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
