using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 1;
    public float actBeginTime = 0.2f;
    public float atkDisTime = 0.5f;

    private Animator animator;
    private PolygonCollider2D attackCollider;

    void Start()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
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
        if (other.CompareTag("Enemy"))
        {
            // 使用接口而不是具体的类
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }
}