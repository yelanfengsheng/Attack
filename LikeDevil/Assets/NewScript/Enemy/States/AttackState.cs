using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//作为基础攻击状态类 后续会有具体的攻击状态继承它 如近战攻击状态 远程攻击状态 等等
public class AttackState : State
{
    protected Transform attackPosition;//攻击位置
    protected bool isAnimationFinished;//动画是否结束

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        entity.atsm.attackState = this;//将当前攻击状态传递给动画到状态机脚本
        isAnimationFinished = false;//动画未结束
        entity.SetVelocity(0f);//攻击时停止移动
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public virtual void TriggerAttack()//所有的攻击状态都拥有这个方法 用于实现攻击触发逻辑 并方便以后再动画事件中调用
    {
        //攻击触发逻辑
    }
    public virtual void FinishAttack()//所有的攻击状态都拥有这个方法 用于实现攻击结束逻辑 并方便以后再动画事件中调用
    {
        isAnimationFinished = true;//动画结束
    }
}
