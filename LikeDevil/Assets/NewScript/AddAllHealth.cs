using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAllHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered health pickup area.");
            PlayerStats playerHealth = collision.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                // 直接恢复到最大生命并触发 UI 更新
                playerHealth.RestoreToMax();
            }
        }
    }
}
