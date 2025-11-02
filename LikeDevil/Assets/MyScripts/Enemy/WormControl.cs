using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormControl : Enemy
{
   
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogWarning("Anim not found!");
        }
        print(animator.name);
       
    }
    public void Start()
    {
        base.Start();
    }
    private void Update()
    {
       
    }
    private void FixedUpdate()
    {
        
    }

    public void WormAttack()
    {
        animator.SetTrigger("attack");

    }
    public override void  TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }


}
