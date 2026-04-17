using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Configuração do item")]
    public string itemName = "Fragmento da Floresta";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.hasForestFragment = true;

                Debug.Log("Item coletado: " + itemName);

                Destroy(gameObject);
            }
        }
    }
}