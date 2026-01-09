using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_LookForPlayerState : LookForPlayerState
{
    private Enemy4 enemy;

    public E4_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData,Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isPlayerInMinAgroRange)//如果玩家在最小仇恨范围内
        {
            stateMachine.ChangeState(enemy.playerDetectedState);//切换到玩家检测状态
        }
        else if (isAllTurnsDone)//如果所有转向都完成了
        {

            stateMachine.ChangeState(enemy.moveState);//切换到移动状态
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
