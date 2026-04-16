using UnityEngine;
using UnityEngine.SceneManagement; // Biblioteca para gerenciamento de cenas

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Carrega a cena com índice 1 (Pode colocar o nome da cena no lugar do índice, ex: "Game")
    }

    public void QuitGame()
    {
// Esse código não conversa diretamente com o jogo, mas é uma prática comum para garantir que o jogo seja encerrado corretamente em diferentes plataformas.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para o jogo no editor
#elif UNITY_WEBGL
        Application.OpenURL("about:blank"); // Redireciona para uma URL (opcional para WebGL)
#else
        Application.Quit(); // Encerra o jogo
#endif
    }
}
