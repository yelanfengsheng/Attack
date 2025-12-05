using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实体数据类 通过ScriptableObject创建实例
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity :ScriptableObject
{
    public float macHealth = 30f;
    public float damageHopSpeed = 3f;//受伤弹跳速度

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float groundCheckRadius = 0.3f;//地面检测半径 用于检查实体是否在地面上
    

    public float minAgroDistance = 2f;//最小检测范围
    public float maxAgroDistance = 4f;//最大检测范围

    public float stunResistance = 3f;//眩晕抗性 用来计算实体被眩晕的难易程度
    public float stunRecoveryTime = 2f;//眩晕恢复时间

    public float closeRangeActionDistance = 1f;//近战范围距离

    public GameObject hitPartical;//受击特效  

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

}
