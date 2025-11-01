using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxTouch : MonoBehaviour
{
    private BoxCollider2D box2D;
    private Animator animator;
    public bool isOpened = false;//宝箱是否被打开
    public bool isRange = false;//玩家是否在触发区域内


    // Start is called before the first frame update
    void Start()
    {
        box2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        if (box2D == null)
        {
            Debug.Log("BoxCollider2D组件为空");
            return;
        }
        if( animator == null)
        {
            Debug.Log("Animator组件为空");
            return;
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (isRange && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            isOpened = true;
            animator.SetTrigger("beOpened");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     isRange = true;    //玩家进入触发区域
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isRange = false; //玩家离开触发区域
    }

}
