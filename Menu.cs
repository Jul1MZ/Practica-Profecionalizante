using TMPro; // Asegúrate de importar TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia al texto de puntuación en el menú

    void Start()
    {
        // Mostrar la puntuación actual del GameManager al inicio del juego
        if (scoreText != null)
        {
            scoreText.text = "Puntuación: " + GameManager.instance.count.ToString();
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
