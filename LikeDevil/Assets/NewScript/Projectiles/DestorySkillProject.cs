using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 定期检查场景中与“技能投射物”相关的物体（按 Tag 或具有 Projectile 组件）
/// 若数量超过 maxSkillProjectiles 则销毁多余的（优先销毁离玩家较远的）
/// </summary>
public class DestorySkillProject : MonoBehaviour
{
    [Header("检测/销毁设置")]
    public int maxSkillProjectiles = 5;         // 最多允许的技能投射物数量
    public string skillTag = "Skill";           // 可选：技能物体的 Tag（若预制体已打 Tag）
    public float checkInterval = 0.5f;         // 检查间隔，避免每帧查找造成性能问题

    private float checkTimer = 0f;

    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            checkTimer = 0f;
            TrimSkillProjectiles();
        }
    }

    private void TrimSkillProjectiles()
    {
        // 收集候选列表（按 Tag 和 Projectile 组件）
        HashSet<GameObject> set = new HashSet<GameObject>();

        // 通过 Tag 收集（若标签未被使用则返回空）
        if (!string.IsNullOrEmpty(skillTag))
        {
            try
            {
                var byTag = GameObject.FindGameObjectsWithTag(skillTag);
                foreach (var g in byTag) set.Add(g);
            }
            catch
            {
                // 如果场景中不存在该 Tag，会抛异常，忽略
            }
        }

        // 通过 Projectile 组件收集（更稳健）
        var projs = FindObjectsOfType<Projectile>();
        foreach (var p in projs)
        {
            if (p != null && p.gameObject != null)
                set.Add(p.gameObject);
        }

        // 转为列表
        List<GameObject> list = set.Where(g => g != null).ToList();

        // 如果数量不超过限制，直接返回
        if (list.Count <= maxSkillProjectiles) return;

        // 尝试找到玩家，用距离作为保留优先级（保留靠近玩家的）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerPos = player != null ? (Vector2)player.transform.position : Vector2.zero;

        if (player != null)
        {
            // 按距离从近到远排序（近的排前面）
            list.Sort((a, b) =>
                Vector2.SqrMagnitude((Vector2)a.transform.position - playerPos)
                .CompareTo(Vector2.SqrMagnitude((Vector2)b.transform.position - playerPos)));
            // 销毁最远的那些（从列表末尾开始）
            int toRemove = list.Count - maxSkillProjectiles;
            for (int i = 0; i < toRemove; i++)
            {
                var idx = list.Count - 1 - i;
                if (idx >= 0 && list[idx] != null)
                {
                    Debug.Log($"DestorySkillProject: Destroying extra skill projectile '{list[idx].name}'");
                    Destroy(list[idx]);
                }
            }
        }
        else
        {
            // 没有玩家时，简单销毁多余项（从末尾）
            int toRemove = list.Count - maxSkillProjectiles;
            for (int i = 0; i < toRemove; i++)
            {
                int idx = list.Count - 1 - i;
                if (idx >= 0 && list[idx] != null)
                {
                    Debug.Log($"DestorySkillProject: Destroying extra skill projectile '{list[idx].name}' (no player found)");
                    Destroy(list[idx]);
                }
            }
        }
    }
}