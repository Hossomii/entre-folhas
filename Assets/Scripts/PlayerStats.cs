using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Vida do jogador")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Progressão")]
    public bool hasForestFragment = false;

    [Header("Arma")]
    public int attackDamage = 1;
    public bool firstUpgradeUnlocked = false;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log("Player recebeu " + damage + " de dano. Vida restante: " + currentHealth);

        StartCoroutine(DamageFeedback());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        currentHealth = 0;

        Debug.Log("Player morreu.");
    }

    IEnumerator DamageFeedback()
    {
        Debug.Log("Player tomou dano!");

        yield return null;
    }
}