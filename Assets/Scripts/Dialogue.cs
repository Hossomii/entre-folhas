using UnityEngine;
using UnityEngine.InputSystem; // Para detectar teclas

public class Dialogue : MonoBehaviour
{
    public string[] speechText; // Frases que o NPC vai falar
    public string actorName;    // Nome do NPC

    private DialogueControl dc; // Referência ao controlador do diálogo
    private bool onRadius;      // Verifica se o jogador está perto
    private bool isDialogueActive; // Verifica se o diálogo está ativo

    public LayerMask playerLayer; // Define qual layer é o jogador
    public float radius;          // Distância para interação

    public GameObject interactionHint; // Dica visual para interação (opcional)

    void Start()
    {
        // Procura automaticamente o DialogueControl na cena
        dc = FindObjectOfType<DialogueControl>();
    }

    private void FixedUpdate()
    {
        // Verifica constantemente se o jogador está no raio
        Interact();
    }

    void Update()
    {
        // Se apertar E e estiver perto do NPC, inicia o diálogo
        if (Keyboard.current.eKey.wasPressedThisFrame && onRadius && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true; // Marca que o diálogo começou

        interactionHint.SetActive(false); // Esconde a dica de interação

        // Envia as falas e o nome para o controlador
        dc.Speech(speechText, actorName);
    }

    private void EndDialogue()
    {
        isDialogueActive = false; // Marca que o diálogo terminou

        // Esconde o painel de diálogo
        dc.HidePanel();

        if (onRadius) 
        { 
            // Se o jogador ainda estiver perto, mostra a dica de interação novamente
            interactionHint.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera no editor para visualizar o alcance
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Interact()
    {
        // Cria dois pontos para formar uma cápsula de detecção
        Vector3 point1 = transform.position + Vector3.up * radius;
        Vector3 point2 = transform.position - Vector3.up * radius;

        // Detecta se o jogador está dentro da área
        Collider[] hits = Physics.OverlapCapsule(point1, point2, radius, playerLayer);

        // Em 2D seria:
        // Collider2D[] hits = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hits.Length > 0)
        {
            onRadius = true; // Jogador está próximo

            if(!isDialogueActive)
            {
                // Mostra a dica de interação se o diálogo não estiver ativo
                interactionHint.SetActive(true);
            }
        }
        else
        {
            onRadius = false; // Jogador saiu da área
            interactionHint.SetActive(false); // Esconde a dica de interação

            // Se sair durante o diálogo, ele é encerrado
            if (isDialogueActive)
            {
                EndDialogue();
            }
        }
    }
}