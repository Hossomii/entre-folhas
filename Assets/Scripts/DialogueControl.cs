using UnityEngine;
using UnityEngine.UI; // Usado para elementos de UI (Text, Button, etc.)
using UnityEngine.InputSystem; // Usado para capturar teclas (novo Input System)
using System.Collections;
using System;

public class DialogueControl : MonoBehaviour
{
    public GameObject dialogueObj; // Objeto do painel de diálogo (Canvas)
    public Text actorNameText;     // Texto que mostra o nome do personagem
    public Text speechText;        // Texto que mostra a fala
    public float typingSpeed;      // Velocidade de digitação (tempo entre letras)

    private string[] sentences;    // Guarda todas as frases do diálogo
    private int index;             // Controla qual frase está sendo exibida
    private Coroutine typingCoroutine;
    // Coroutine = função que roda ao longo do tempo (usada para efeito de digitação)

    void Update()
    {
        // Se o jogador apertar TAB, tenta avançar o diálogo
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            NextSentence();
        }
    }

    public void Speech(string[] txt, string actorName)
    {
        // Se já estiver digitando algo, para antes de começar outro diálogo
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueObj.SetActive(true);   // Mostra o painel de diálogo
        speechText.text = "";          // Limpa o texto da fala
        actorNameText.text = actorName; // Define o nome do personagem

        sentences = txt; // Recebe todas as frases
        index = 0;       // Começa da primeira frase

        // Inicia o efeito de digitação
        typingCoroutine = StartCoroutine(TypeSentence());
    }

    public void NextSentence()
    {
        // Só avança se a frase já terminou de ser digitada
        if (speechText.text == sentences[index])
        {
            // Se ainda tiver mais frases
            if (index < sentences.Length - 1)
            {
                index++; // Vai para a próxima frase

                speechText.text = ""; // Limpa o texto

                // Para a digitação anterior (se existir)
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }

                // Começa a digitar a próxima frase
                typingCoroutine = StartCoroutine(TypeSentence());
            }
            else
            {
                // Se não tiver mais frases, encerra o diálogo
                EndDialogue();
            }
        }
    }

    public void HidePanel()
    {
        // Para a digitação, se estiver acontecendo
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        speechText.text = "";      // Limpa fala
        actorNameText.text = "";   // Limpa nome
        index = 0;                 // Reseta o índice

        dialogueObj.SetActive(false); // Esconde o painel
    }

    public void EndDialogue()
    {
        speechText.text = ""; // Limpa fala
        index = 0;            // Reseta índice

        dialogueObj.SetActive(false); // Esconde painel
    }

    IEnumerator TypeSentence()
    {
        speechText.text = ""; // Garante que começa vazio

        // Percorre cada letra da frase atual
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter; // Adiciona uma letra

            yield return new WaitForSeconds(typingSpeed);
            // yield = pausa o código por um tempo antes de continuar
            // Aqui cria o efeito de "texto sendo digitado"
        }

        typingCoroutine = null; // Marca que terminou de digitar
    }

    internal void Speech(string[] speechText, string[] actorName)
    {
        throw new NotImplementedException();
    }
}