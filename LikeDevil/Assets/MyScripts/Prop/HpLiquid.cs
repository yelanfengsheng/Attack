using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpLiquid : MonoBehaviour
{
    // 静态实例，全局可访问
    public static HpLiquid Instance;

    void Awake()
    {
        // 单例模式设置
        if (Instance == null)
        {
            Instance = this;
            // 如果需要在场景切换时保留，取消下面注释
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static int HpValue = 1;
    public HealthBarUI healthBarUI;

    private void Start()
    {
        // 可选：初始化时同步一次血量
        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar();
        }
    }

    public void AddHealth(int amount)
    {
        // 增加玩家生命值的逻辑
        Debug.Log($"Player health increased by {amount}!");
        PlayerHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth += amount;

            // 同步到 HealthBarUI 的静态变量
            HealthBarUI.nowHealth = playerHealth.currentHealth;

            Debug.Log($"Current Health: {playerHealth.currentHealth}");

            // 更新 UI
            if (healthBarUI != null)
            {
                healthBarUI.UpdateHealthBar();
            }
            else
            {
                Debug.LogWarning("HealthBarUI reference is not set in HpLiquid!");
            }
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found on the Player!");
        }
    }
}