using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInteraction : MonoBehaviour
{
    [Header("Mensagens da loja")]
    public string lockedMessage = "A oficina ainda está fechada. Prove seu valor na floresta.";
    public string unlockedMessage = "A oficina foi liberada. Seu machado pode ser aprimorado.";
    public string upgradeMessage = "Seu machado foi aprimorado! Dano aumentado.";
    public string alreadyUpgradedMessage = "Seu primeiro aprimoramento já foi feito.";

    private bool playerInRange = false;
    private PlayerStats playerStats;

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (playerStats == null) return;

        if (!playerStats.hasForestFragment)
        {
            Debug.Log(lockedMessage);
            return;
        }

        if (!playerStats.firstUpgradeUnlocked)
        {
            playerStats.hasForestFragment = false;
            playerStats.firstUpgradeUnlocked = true;
            playerStats.attackDamage += 1;

            Debug.Log(upgradeMessage);
            return;
        }

        Debug.Log(alreadyUpgradedMessage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerStats = other.GetComponentInParent<PlayerStats>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerStats = null;
        }
    }
}