using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}
