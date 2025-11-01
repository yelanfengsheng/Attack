using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private float resetDelay = 0.3f;
    private CapsuleCollider2D currentPlayerCollider;

    private void OnTriggerStay2D(Collider2D other)
    {
        // 检查是否是玩家的脚（trigger）
        if (other.isTrigger && other.transform.parent != null && other.transform.parent.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.S) && Input.GetButton("Jump"))
            {
                CapsuleCollider2D playerCollider = other.transform.parent.GetComponent<CapsuleCollider2D>();
                if (playerCollider != null)
                {
                    playerCollider.isTrigger = true;
                    currentPlayerCollider = playerCollider;
                    print("允许穿过平台");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 检查离开的是否是玩家的脚
        if (other.isTrigger && other.transform.parent != null && other.transform.parent.CompareTag("Player"))
        {
            // 延迟恢复碰撞体，确保玩家完全离开
            StartCoroutine(ResetPlayerCollider());
        }
    }

    private IEnumerator ResetPlayerCollider()
    {
        yield return new WaitForSeconds(resetDelay);

        if (currentPlayerCollider != null)
        {
            currentPlayerCollider.isTrigger = false;
            currentPlayerCollider = null;
            print("恢复碰撞体");
        }
    }
}