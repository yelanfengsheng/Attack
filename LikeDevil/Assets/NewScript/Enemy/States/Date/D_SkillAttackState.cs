using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Skill Attack State")]

public class D_SkillAttackState : MonoBehaviour
{
    public GameObject projectilePrefab;//技能预制体
    public float projectileDamage = 10f;//技能伤害
    //public float projectileTravelDistance = 5f;//技能飞行距离
}
