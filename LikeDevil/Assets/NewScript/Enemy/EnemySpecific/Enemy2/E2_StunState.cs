using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_StunState : StunState
{
    private Enemy2 enemy;
    public E2_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        //如果眩晕时间结束
        if (isStunTimeOver)
        {
            if(isPlayerInMinAgroRange)//如果玩家在最小仇恨范围内
            {
                stateMachine.ChangeState(enemy.playerDetectedState);//切换到玩家发现状态
            }
            else//如果玩家不在最小仇恨范围内
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);//切换到寻找玩家状态
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
