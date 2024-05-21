using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    public int maxHealth = 3;
    int currentHealth;
    public MonoBehaviour EnemyScase;
    private Collider2D enemyCollider;
    public float stunDuration = 1f; // Duration to be stunned

    private void Start()
    {
        currentHealth = maxHealth;
        enemyCollider = GetComponent<Collider2D>();
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(StunEnemy());
        }
    }

    void Die()
    {
        this.enabled = false;
        if (EnemyScase != null)
        {
            EnemyScase.enabled = false;
        }
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false; // Disabling the collider
        }
    }

    IEnumerator StunEnemy()
    {
        if (EnemyScase != null)
        {
            EnemyScase.GetComponent<EnemyScase>().isStunned = true;
            yield return new WaitForSeconds(stunDuration);
            EnemyScase.GetComponent<EnemyScase>().isStunned = false;
        }
    }

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}