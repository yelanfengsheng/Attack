using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttackState : AttackState
{
    protected D_SkillAttackState stateData;

    protected GameObject projectile; //技能攻击的投射物
    protected SkilProjectile skillScript; //技能脚本
    public SkillAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition,D_SkillAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData  = stateData;
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

        skillScript = GameObject.FindObjectOfType<SkilProjectile>();//获取技能脚本
        if (skillScript != null)
        {
            skillScript.SpawnAroundPlayerXAxis();//调用生成方法
        }
    }
}
