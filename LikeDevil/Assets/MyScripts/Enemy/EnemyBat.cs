using System.Collections;
using UnityEngine;

public class EnemyBat : Enemy
{
    [Header("Bat位置设置")]
    public Vector2 canMoveAre;
    public Vector2 StartMovePosition;
    public Vector2 nowMovePosition;
    public Vector2 nextMovePosition;
    public MoveAreaVisualizer moveAreaVisualizer;

    [Header("Bat移动设置")]
    public float moveSpeed = 2f;
    public float waitTime = 1f;
    public float currentWaitTime = 0f;

    protected override void Start()
    {
        base.Start(); // 必须调用基类的Start!

        StartMovePosition = transform.position;
        nextMovePosition = GetRandomPosition();
    }

    public  void Update()
    {
     

        if (moveAreaVisualizer != null)
        {
            canMoveAre = moveAreaVisualizer.canMoveArea;
        }

        // 移动逻辑
        if (Vector2.Distance(transform.position, nextMovePosition) < 0.1f)
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= waitTime)
            {
                currentWaitTime = 0f;
                nextMovePosition = GetRandomPosition();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            nextMovePosition,
            moveSpeed * Time.fixedDeltaTime
        );
        nowMovePosition = transform.position;
    }

    public Vector2 GetRandomPosition()
    {
        if (moveAreaVisualizer == null) return transform.position;

        float randomX = Random.Range(-canMoveAre.x / 2, canMoveAre.x / 2);
        float randomY = Random.Range(-canMoveAre.y / 2, canMoveAre.y / 2);
        return new Vector2(randomX, randomY);
    }
}