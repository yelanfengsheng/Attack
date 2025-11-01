using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSickle : MonoBehaviour
{
    private Rigidbody2D rb2;
    private Transform playerTransform;
    private Transform sickleTransform;
    public float speed = 5f;
    public float rotationSpeed = 20f;
    public int damage = 1;
    private Vector2 startVelocity;//初始速度
    [Header("飞镖状态")]
    public bool isThrown = false;//是否已经投掷
    public bool isReturning = false;//是否正在返回
    public bool isAtPlayer = true;//是否在玩家手中
    public bool canBeThrown = true;//是否可以被投掷
    [Header("速度衰减参数")]
    public float decreaseRate = 0.1f;//速度衰减率
    public float minVelocity = 0.1f;//最小速度
    public Vector3 offset;//偏移量

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        sickleTransform = this.transform;
        startVelocity = rb2.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAtPlayer)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
      
        if (Input.GetMouseButtonDown(0)&&canBeThrown)
        {
            Debug.Log("鼠标左键按下");
            OnLeftMouseClick();
            isAtPlayer = false;
        }
        if(isThrown)
        {
            DecreaseVelocity();
            canBeThrown = false;
        }
        if(isReturning)
        {
            ReturnToPlayer();
        }
        if(!isThrown && !isReturning)
        {
            //保持飞镰位置在玩家位置
            sickleTransform.position = playerTransform.position+offset;
        }
    }

    private void ReturnToPlayer()
    {
        
        Vector3 direction = (playerTransform.position- sickleTransform.position ).normalized;//计算方向向量
        rb2.velocity = direction * speed;//设置速度
        //当飞镰接近玩家时，停止移动并重置状态
        if(Vector3.Distance(sickleTransform.position,playerTransform.position)<0.5f)
        {
            rb2.velocity = Vector2.zero;
            isThrown = false;
            isReturning = false;
            //重置位置到玩家位置
            sickleTransform.position = playerTransform.position+offset;
            isAtPlayer = true;
            canBeThrown = true;
        }
    }

    private void DecreaseVelocity()
    {
        // 每帧减少速度
        if (rb2.velocity.magnitude>minVelocity)
        {
            rb2.velocity *= 1 - decreaseRate; // 每帧减少%的速度
        }
        else
        {
            rb2.velocity = Vector2.zero; // 速度小于最小值时，停止移动
           isReturning = true;//标记为正在返回
        }
       
        
    }

    public void OnLeftMouseClick()
    {
        // 获取鼠标点击位置的世界坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // 确保z轴为0
        // 计算方向向量
        Vector3 direction = (mouseWorldPosition - playerTransform.position).normalized;
        // 设置飞镰的速度
        rb2.velocity = direction * speed;
        //标记为已经投掷
        isThrown = true;    
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && isThrown)
        {
            //Debug.Log("飞镰击中敌人");
            //对敌人造成伤害
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            //飞镰开始返回
            isReturning = true;
            //isThrown = false;
        }
        
    }
}
