using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Dano do inimigo")]
    public int damage = 1;
    public float damageCooldown = 1f;

    private float lastDamageTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }
}