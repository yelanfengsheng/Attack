using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;//作用是保存配置数据
    protected bool isStunTimeOver;//眩晕时间是否结束
    protected bool isGrounded;//是否着地
    protected bool isMovementStopped;//移动是否停止
    protected bool performCloseRangeAction;//执行近战攻击动作
    protected bool isPlayerInMinAgroRange;//玩家是否在最小攻击范围内

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = entity.CheckGround();//检查是否着地
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();//检查玩家是否在近战范围内
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAggroRange();//检查玩家是否在最小攻击范围内
    }

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false;//初始化眩晕时间未结束
        isMovementStopped = false;//初始化移动未停止
        entity.SetVelocity(stateData.stunKnockbackSpeed,stateData.stunKnockbackAngle,entity.lastDamageDirection);//进入眩晕状态时水平速度设为0
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();//重置眩晕抗性
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.stunTime)//如果眩晕时间结束
        {
            isStunTimeOver = true;//眩晕时间结束
        }
        if(isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopped) //如果着地且击退时间结束且移动未停止
        {
            isMovementStopped = true;//移动已停止
            entity.SetVelocity(0f);//水平速度设为0
        }
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
