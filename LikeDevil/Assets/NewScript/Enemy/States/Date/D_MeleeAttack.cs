using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newMeleeAttackData",menuName = "Data/State Data/Melee Attack Data")]
public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 0.5f;//¹¥»÷°ë¾¶
    public float attackDamage = 10f;//¹¥»÷ÉËº¦
    public LayerMask whatIsPlayer;//Íæ¼ÒÍ¼²ã
}
