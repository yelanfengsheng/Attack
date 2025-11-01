using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController 
{
    /// <summary>
    /// 伤害的类型  直接伤害和持续伤害
    /// </summary>
    public enum E_DamageType
    {
        //Physical,
        //Magical,
        //True
        ContinuousDamage,
        InstantDamage
    }
    [Header("全局伤害设置")]
    [Tooltip("默认直接伤害值")]
    public float defaultInstantDamage = 5f;

    [Tooltip("默认持续伤害值")]
    public float defaultContinuousDamage = 2f;

    [Tooltip("默认持续伤害间隔")]
    public float defaultContinuousInterval = 1f;

    [Tooltip("默认持续伤害时间")]
    public float defaultContinuousDuration = 3f;



}
