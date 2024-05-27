using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int enemyAttackDamage = 1;

    public float attackRate;
    private float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        bool canAttack = Time.time >= nextAttackTime;
        if (!canAttack){
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attack");
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }

    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy_Damage>().EnemyTakeDamage(enemyAttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
