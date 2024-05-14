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

    private void Start()
    {
        currentHealth = maxHealth;
        enemyCollider = GetComponent<Collider2D>();
    }

    public void EnemyTakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
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

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
