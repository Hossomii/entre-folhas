using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject img; // Referência a imagem preta que cobre a tela no fundo
    void Start()
    {
        if(Time.timeScale == 0f)
        {
            Resume(); // Garante que o jogo não comece pausado
        }
    }

    void Update()
    {
        // Aqui a gente usa update porque estamos usando teclas para controlar
        // o retorno ao menu e a pausa do jogo, então precisamos verificar isso a cada frame
        ReturnGame();
        CallPauseResume();
    }

    public void ReturnGame()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0); // Pode ser ou por índice ou por nome da cena
        }
    }

    public void CallPauseResume()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0; // Para o tempo de jogo
        isPaused = true;
        img.SetActive(true); // Ativa a imagem preta para cobrir a tela
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Retoma o tempo de jogo
        isPaused = false;
        img.SetActive(false);
    }
}
