using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]

public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectilePrefab;//投射物预制体
    public float projectileSpeed = 12f;//投射物速度
    public float projectileDamage = 10f;//投射物伤害
    public float projectileTravelDistance = 5f;//投射物飞行距离
}
