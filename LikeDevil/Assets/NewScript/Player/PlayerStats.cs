
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private GameObject deathChunkPartical, deathBloodPartical;// 死亡时的粒子效果

    private float currentHealth;//玩家当前生命值

    private GameManager Gm;
    // 新增：当生命值改变时通知订阅者（传递归一化的血量 0..1）
    public event Action<float> OnHealthChanged;

    private void Start()
    {
        currentHealth = maxHealth;// 初始化当前生命值为最大生命值
        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 启动时通知 UI 当前血量（归一化）
        OnHealthChanged?.Invoke(GetCurrentHealthPercent());
    }
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        // 每次受到伤害后通知 UI（归一化）
        OnHealthChanged?.Invoke(GetCurrentHealthPercent());
    }

    // 新增：恢复到最大生命并通知 UI
    public void RestoreToMax()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(GetCurrentHealthPercent());
    }

    private void Die()
    {
        // 播放死亡粒子效果
        if (deathChunkPartical != null)
        {
            Instantiate(deathChunkPartical, transform.position, deathChunkPartical.transform.rotation);
        }
        if (deathBloodPartical != null)
        {
            Instantiate(deathBloodPartical, transform.position, deathBloodPartical.transform.rotation);
        }
        Gm.RespownPlayer();//通知游戏管理器进行重生
        Destroy(gameObject);// 销毁玩家对象

    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    // 新增：返回归一化的血量（0..1）
    public float GetCurrentHealthPercent()
    {
        if (maxHealth <= 0) return 0f;
        return Mathf.Clamp01(currentHealth / maxHealth);
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}