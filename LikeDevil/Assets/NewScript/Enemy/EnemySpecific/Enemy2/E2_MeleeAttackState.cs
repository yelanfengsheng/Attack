using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeleeAttackState : MeleeAttackState
{
    private Enemy2 enemy;
    public E2_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData,Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
        //攻击动画结束后 根据玩家是否在最小攻击范围切换状态
        if (isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)//如果玩家在最小仇恨范围内
            {
                stateMachine.ChangeState(enemy.playerDetectedState);//切换到玩家检测状态
            }
            else if(!isPlayerInMinAgroRange)//如果玩家不在最小仇恨范围内
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
