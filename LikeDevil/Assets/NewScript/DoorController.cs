using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject openDoor;   // 拖入 Open Door
    public GameObject closeDoor;  // 拖入 Closed Door

    private bool isPlayerInOpenZone = false;

    // 注意：这里不再依赖 Door 自身的 Collider，而是通过 DoorTriggerZone 的事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInOpenZone = true;
            Debug.Log("✅ 玩家进入可开门区域！");
            openDoor.SetActive(true);
            closeDoor.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInOpenZone = false;
            Debug.Log("❌ 玩家离开可开门区域！");
            closeDoor.SetActive(true);
            openDoor.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInOpenZone)
        {
            openDoor.SetActive(true);
            closeDoor.SetActive(false);
            // 如果需要阻挡，启用 Open Door 的 Collider
            if (openDoor.TryGetComponent<Collider2D>(out var col))
                col.enabled = true;
            if (closeDoor.TryGetComponent<Collider2D>(out var col2))
                col2.enabled = false;
        }
        else
        {
            openDoor.SetActive(false);
            closeDoor.SetActive(true);
            // 关闭 Open Door 的 Collider，启用 Closed Door 的 Collider
            if (openDoor.TryGetComponent<Collider2D>(out var col))
                col.enabled = false;
            if (closeDoor.TryGetComponent<Collider2D>(out var col2))
                col2.enabled = true;
        }
    }
}