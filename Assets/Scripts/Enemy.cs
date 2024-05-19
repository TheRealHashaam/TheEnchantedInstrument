using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackRadius = 5f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    private SnakeCharmer player;
    private bool canAttack = true;
    Animator animator;
    public bool Sleeping = false;
    void Start()
    {
        player = FindObjectOfType<SnakeCharmer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!player._dead)
        {
            if(!Sleeping)
            {
                if(!player._isplayng)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) <= attackRadius)
                    {
                        if (canAttack)
                        {
                            Attack();
                            StartCoroutine(AttackCooldown());
                        }
                    }
                }
            }
        }
    }
    public void Sleep()
    {
        Sleeping = true;
        animator.SetTrigger("Sleep");
        animator.SetBool("Sleeping", true);
    }
    public void WakeUp()
    {
        Sleeping = false;
        animator.SetBool("Sleeping", false);
    }
    void Attack()
    {
        animator.SetBool("Attack", true);
        int r = Random.Range(1, 3);
        animator.SetInteger("AttackNo", r);
    }
    public void Damage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRadius)
        {
            player.TakeDamage(10);
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Attack", false);
        animator.SetInteger("AttackNo", 0);
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown - 0.1f);
        canAttack = true;
    }
}
