using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoint : MonoBehaviour
{
    // 不再使用 static 共享值，改为根据事件更新 UI
    private float targetFill = 1f;
    public Image healthImage;
    public Image fadeImage;

    // 渐变参数：fade_duration 表示补血/掉血淡化动画的持续时间（秒）
    public float fade_duration = 0.5f;
    private PlayerStats playerHealth;

    private void Awake()
    {
        TrySubscribeToPlayer();
    }

    private void OnDestroy()
    {
        UnsubscribeFromPlayer();
    }

    private void TrySubscribeToPlayer()
    {
        // 如果已有引用且不是 null（未被销毁），则无需重新订阅
        if (playerHealth != null) return;

        playerHealth = FindObjectOfType<PlayerStats>();
        if (playerHealth != null)
        {
            // 初始化显示
            float pct = playerHealth.GetCurrentHealthPercent();
            targetFill = pct;
            if (healthImage != null) healthImage.fillAmount = pct;
            if (fadeImage != null) fadeImage.fillAmount = pct;

            // 订阅生命值变化事件
            playerHealth.OnHealthChanged += OnHealthChanged;
        }
    }

    private void UnsubscribeFromPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= OnHealthChanged;
            playerHealth = null;
        }
    }

    // 当 PlayerStats 通知血量改变时调用（value 为 0..1）
    private void OnHealthChanged(float normalizedValue)
    {
        // 立即更新主血条显示值（前端显示）
        targetFill = Mathf.Clamp01(normalizedValue);
        if (healthImage != null) healthImage.fillAmount = targetFill;

        // fadeImage 将在 Update 中平滑过渡到 healthImage
    }

    // Update is called once per frame
    void Update()
    {
        // 如果玩家对象被销毁（例如死亡并等待重生），尝试重新订阅新实例
        if (playerHealth == null)
        {
            TrySubscribeToPlayer();
        }

        if (fadeImage == null || healthImage == null) return;

        // 平滑地将 fadeImage 过渡到当前 healthImage.fillAmount
        if (Mathf.Abs(fadeImage.fillAmount - healthImage.fillAmount) > 0.001f)
        {
            fadeImage.fillAmount = Mathf.Lerp(fadeImage.fillAmount, healthImage.fillAmount, Time.deltaTime / Mathf.Max(0.0001f, fade_duration));
        }
    }
}