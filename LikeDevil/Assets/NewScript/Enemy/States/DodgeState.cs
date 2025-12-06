using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//闪避状态类 继承自State基类 闪避状态的基本功能都在这里实现
public class DodgeState : State
{
    protected D_DodgeState stateData;//来获取闪避状态数据

    protected bool performCloseRangeAction;//是否执行近战攻击动作
    protected bool isPlayerInMaxAgroRange;//玩家是否在最大仇恨范围内
    protected bool isGrounded;//是否在地面上
    protected bool isDodgeOver;//闪避动作是否结束





    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();//检测玩家是否在近战范围内
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAggroRange();//检测玩家是否在最大攻击范围内
        isGrounded = entity.CheckGround();//检测是否在地面上
    }

    public override void Enter()
    {
        base.Enter();
        isDodgeOver = false;//闪避动作未结束

        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle,-entity.facingDirection);//设置实体速度，实现闪避效果
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.dodgeTime&&isGrounded)//如果当前时间大于等于开始时间加上闪避时间
        {
            isDodgeOver = true;//闪避动作结束
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
