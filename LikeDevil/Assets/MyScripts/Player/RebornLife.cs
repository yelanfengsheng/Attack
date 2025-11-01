using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornLife : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned in RebornLife script.");
            return;
        }
        animator = player.GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("Animator component not found on the player GameObject.");
        }   
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RebornPlayer();
        }
    }
    public void RebornPlayer()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.OnableAllPlayerScripts();
        ResetAnimationState();
        animator.SetBool("isDead", false);
        playerHealth.currentHealth = playerHealth.maxHealth; // 重置血量
        HealthBarUI.nowHealth = playerHealth.currentHealth;

    }
    private void ResetAnimationState()
    {
        Debug.Log("Resetting animation state...");
        if (animator == null) return;

        // 重置所有参数
        animator.ResetTrigger("Atk");
        animator.SetBool("isRun", false);
        animator.SetBool("isJump", false);
        animator.SetBool("isFall", false);
        animator.SetBool("IsDead", false);

        // 播放空闲动画
        animator.Play("Idle", 0, 0f);

        // 强制更新动画状态机
        animator.Update(0f);
    }
}
