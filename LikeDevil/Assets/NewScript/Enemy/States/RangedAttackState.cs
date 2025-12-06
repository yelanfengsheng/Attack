using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//远程攻击状态类 继承自基础攻击状态类 攻击状态的具体实现
public class RangedAttackState : AttackState
{
    protected D_RangedAttackState stateData;

    protected GameObject projectile; //远程攻击的投射物
    protected Projectile projectileScript; //投射物脚本
    public RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition,D_RangedAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        projectile = GameObject.Instantiate(stateData.projectilePrefab, attackPosition.position, attackPosition.rotation);//实例化投射物
        projectileScript = projectile.GetComponent<Projectile>();//获取投射物脚本
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);//调用投射物脚本中的发射方法 传入速度 飞行距离和伤害值
    }
}
