using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormControl : MonoBehaviour
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


    public void WormAttack()
    {
        animator.SetTrigger("attack");
    }
    
}
