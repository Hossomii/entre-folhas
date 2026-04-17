using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Vida do inimigo")]
    public int maxHealth = 3;
    private int currentHealth;

    [Header("Drop do inimigo")]
    public GameObject dropPrefab;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(gameObject.name + " recebeu " + damage + " de dano. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " morreu.");

        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}