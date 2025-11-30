using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private Enemy1 enemy;
    public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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


        if (performCloseRangeAction)//执行近程动作
        {
            stateMachine.ChangeState(enemy.meleeAttackState);//切换到近战攻击状态
        }
        else if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);//切换到寻找玩家状态
        }
        else if (isChargeTimeOver)//如果冲锋时间结束 可以进行冲刺
        {
             if (isPlayerInMinAgroRange)//如果玩家在最小攻击范围内
            {
                stateMachine.ChangeState(enemy.playerDetectedState);//切换到玩家检测状态
            }
            else
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
