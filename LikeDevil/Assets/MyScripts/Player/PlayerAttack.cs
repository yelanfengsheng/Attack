using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 1;
    
    public float actBeginTime = 0.2f;
    public float atkDisTime = 0.5f;


    private Animator animator;
    private PolygonCollider2D attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
       
    }
    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {  
            animator.SetTrigger("Atk");
          
            StartCoroutine(StartAttackCoroutine());
           
        }

        
       
    }

     IEnumerator StartAttackCoroutine()
    {
        yield return new WaitForSeconds(actBeginTime);
        attackCollider.enabled = true;
        StartCoroutine(DisAttackCoroution());
    }
    IEnumerator DisAttackCoroution()
    {
        yield return new WaitForSeconds(atkDisTime);
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBat>().TakeDamage(attackDamage);
        }
    }

}
