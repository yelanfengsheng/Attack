using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_MoveState moveState { get; private set; }//ÒÆ¶¯×´Ì¬
    public E3_IdleState idleState { get; private set; }//¿ÕÏÐ×´Ì¬
    public E3_PlayerDetectedState playerDetectedState { get; private set; }//Íæ¼Ò¼ì²â×´Ì¬
    public E3_RangedAttackState rangedAttackState { get; private set; }//Ô¶³Ì¹¥»÷×´Ì¬s
    public E3_LookForPlayerState lookForPlayerState { get; private set; }//Ñ°ÕÒÍæ¼Ò×´Ì¬

    [SerializeField]
    private D_MoveState moveStateData;//ÒÆ¶¯×´Ì¬Êý¾Ý
    [SerializeField]
    private D_IdleState idleStateData;//¿ÕÏÐ×´Ì¬Êý¾Ý
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;//Íæ¼Ò¼ì²â×´Ì¬Êý¾Ý
    [SerializeField]
    private D_RangedAttackState rangedAttackStateData;//Ô¶³Ì¹¥»÷×´Ì¬Êý¾Ý
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;//Ñ°ÕÒÍæ¼Ò×´Ì¬Êý¾Ý


    [SerializeField]
    private Transform rangedAttackPosition;//Ô¶³Ì¹¥»÷Î»ÖÃ



    public override void Start()
    {
        base.Start();
        moveState = new E3_MoveState(this, stateMachinel, "move", moveStateData, this);//´´½¨ÒÆ¶¯×´Ì¬ÊµÀý
        idleState = new E3_IdleState(this, stateMachinel, "idle", idleStateData, this);//´´½¨¿ÕÏÐ×´Ì¬ÊµÀý
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachinel, "playerDetected", playerDetectedStateData, this);//´´½¨Íæ¼Ò¼ì²â×´Ì¬ÊµÀý
        rangedAttackState = new E3_RangedAttackState(this, stateMachinel, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        lookForPlayerState = new E3_LookForPlayerState(this, stateMachinel, "lookForPlayer", lookForPlayerStateData, this);//´´½¨Ñ°ÕÒÍæ¼Ò×´Ì¬ÊµÀý



        stateMachinel.Initialize(moveState);//³õÊ¼»¯×´Ì¬»ú ÉèÖÃ³õÊ¼×´Ì¬ÎªÒÆ¶¯×´Ì¬
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
