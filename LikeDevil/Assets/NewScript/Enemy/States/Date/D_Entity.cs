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

    public float minAgroDistance = 2f;//最小检测范围
    public float maxAgroDistance = 4f;//最大检测范围

    public float closeRangeActionDistance = 1f;//近战范围距离

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

}
