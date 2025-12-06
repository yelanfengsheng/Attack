using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//敌人2闪避状态类 此类表示只属于敌人2的闪避状态 是敌人的特有状态
public class E2_DodgeState : DodgeState
{
    private Enemy2 enemy;
    public E2_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        //是否闪避动作结束
        if (isDodgeOver)
        {
            if(performCloseRangeAction&&isPlayerInMaxAgroRange)//如果执行近战攻击动作且玩家在最大仇恨范围内
            {
                stateMachine.ChangeState(enemy.meleeAttackState);//切换到近战攻击状态
            }
            else if(!performCloseRangeAction&&isPlayerInMaxAgroRange)//如果执行远程攻击动作且玩家在最大仇恨范围内
            {
                stateMachine.ChangeState(enemy.rangedAttackState);//切换到远程攻击状态
            }
            else if (!isPlayerInMaxAgroRange)//如果玩家不在最大仇恨范围内
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
