using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_PlayerDetectedState : PlayerDetectedState
{
    private Enemy4 enemy;

    public E4_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData,Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //根据条件切换状态
        if (performCloseRangeAction)//如果执行近战攻击动作
        {
            
                stateMachine.ChangeState(enemy.meleeAttackState);//切换到近战攻击状态
            
        }
        else if (performLongRangeAction)//如果执行远程攻击动作
        {
            stateMachine.ChangeState(enemy.rangedAttackState);//切换到远程攻击状态
        }


        else if (!isPlayerInMaxAgroRange)//如果玩家不在最大仇恨范围内
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);//切换到寻找玩家状态
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
