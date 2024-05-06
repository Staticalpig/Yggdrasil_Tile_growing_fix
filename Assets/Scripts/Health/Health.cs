using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] public float currentHealth { get; private set; }

    private bool dead;
    private MainMenu mainMenu;

    private void Awake()
    {
        currentHealth = startingHealth;
        mainMenu = FindObjectOfType<MainMenu>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth >  0)
        {
            //Player hurt
            
        }
        else
        {
            //Player dead
            if (!dead)
            {
                //player dead
                GetComponent<Player>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                // show game over screen
               

                dead = true;

            }
        }
    }
    


}
