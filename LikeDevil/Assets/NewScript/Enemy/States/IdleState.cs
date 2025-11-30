using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_IdleState stateData;

    protected bool filpAfterIdle;//空闲状态后是否翻转
    protected bool isIdleTimeOver;//空闲时间是否结束
    protected bool isPlayerInMinAgroRange;//玩家是否在最小攻击范围内
    protected float idleTime;//空闲时间


    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
      
        isPlayerInMinAgroRange = entity.CheckPlayerInMaxAggroRange();//检测玩家是否在最小攻击范围内
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);//空闲状态下速度为0
        isIdleTimeOver = false;//空闲时间已经结束
        SetRandomIdleTime();//再随机设置一个空闲时间
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime)//如果当前时间大于等于开始时间加上空闲时间
        {
            isIdleTimeOver = true;//空闲时间结束
            if (filpAfterIdle)//如果空闲时间后需要翻转
            {
                entity.Flip();//翻转实体方向
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();//基于类的物理更新

    }
    public void SetFlipAfterIdle(bool filp)
    {
      filpAfterIdle = filp;
    }
    private void SetRandomIdleTime()//设置随机空闲时间
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
