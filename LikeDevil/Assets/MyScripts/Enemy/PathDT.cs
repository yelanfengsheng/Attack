using DG.Tweening;
using UnityEngine;

/// <summary>
/// 此脚本适用于 规定敌人的移动 并朝着一个方
/// </summary>
public class PathDT : MonoBehaviour
{
    public Transform[] path;
    public Transform role;
    public float nextPointTime = 1f;
    private SpriteRenderer sprite;
    private Vector3 front;
    public bool isFlip;
    [Header("射线检测的距离")]
    public float rayDistance = 5f;
    private void Awake()
    {
        sprite = role.GetComponentInChildren<SpriteRenderer>(); 
        // 如果 role 是子物体，用 GetComponentInChildren，但通常直接挂在 role 上
        if (sprite == null )
            Debug.LogError("SpriteRenderer not found on role: " + role.name);
       front = new Vector3(-1, 0, 0);//默认图片向左
    }

    void Start()
    {
        UsePath();
    }

    void UsePath()
    {
        Vector3[] pathPoints = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            pathPoints[i] = path[i].position;
        }

        role.DOPath(pathPoints, nextPointTime * path.Length, PathType.Linear)
            .SetEase(Ease.Linear)
           
            .OnUpdate(() =>

            {
                //// 根据 flipX 确定射线方向
                //Vector2 rayDirection = sprite.flipX ? Vector2.left : Vector2.right;
                ////Debug.Log($"角色位置: {role.position}, 朝向: {(sprite.flipX ? "左" : "右")}, 射线方向: {rayDirection}");
                //Vector2 rayOrigin = (Vector2)role.position + rayDirection * 0.5f;
                //Ray2D ray = new Ray2D(rayOrigin, rayDirection);
                //RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance);
                //Debug.DrawRay(rayOrigin, ray.direction * rayDistance, Color.red);
                //if (hit.collider != null)
                //{
                //    if (hit.collider.gameObject.CompareTag("Player"))
                //    {
                //        print("射线检测到玩家");
                //    }
                //}

                //// 可视化射线
                //Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

               
              
                if(transform.localScale.x < 0)
                {
                    front = new Vector3(1, 0, 0);
                }
                else
                {

                }

            })
            .OnWaypointChange((waypointIndex) =>
            {
                int nextIndex = (waypointIndex + 1) % path.Length;
                float currentX = pathPoints[waypointIndex].x;
                float nextX = pathPoints[nextIndex].x;

                //// 调整翻转逻辑
                //sprite.flipX = nextX < currentX; // 向左走时翻转
            })
            .SetLoops(-1);
    }
}