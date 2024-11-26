using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int count = 0; // Puntuación
    public TextMeshProUGUI countText; // Referencia al texto del contador en UI
    public string playerName = ""; // Nombre del jugador

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evitar destrucción al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Asegurar que solo haya una instancia
        }

        // Resetear la puntuación cuando se carga una nueva escena de juego
        SceneManager.sceneLoaded += OnSceneLoaded; // Escuchar cuando se carga una nueva escena
    }

    // Esta función se llama cada vez que se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Si la escena es la del juego, reinicia el contador y el nombre
        if (scene.buildIndex == 1) // Asumiendo que la escena de juego tiene índice 1
        {
            count = 0; // Reinicia la puntuación
            playerName = ""; // Reinicia el nombre del jugador

            if (countText != null)
            {
                countText.text = "Puntuación: " + count; // Actualiza el texto de la UI
            }
        }
    }

    // Mostrar la puntuación con el nombre
    // Mostrar la puntuación con el nombre
    public void UpdateScoreText()
    {
        if (countText != null)
        {
            // Asegúrate de que siempre muestre 'Puntuación: {puntos}'
            countText.text = "Puntuación: " + count;
        }
    }


    // Incrementar puntuación
    public void IncreaseCount(int amount = 1)
    {
        count += amount; // Incrementa la puntuación
        UpdateScoreText(); // Actualiza el texto con la nueva puntuación
    }
    public void DecreaseCount(int amount = 1)
    {
        count -= amount;
        if (count < 0) count = 0;
        UpdateScoreText();
    }

    // Método para finalizar el juego
    public void EndGame()
    {
        // Guardar la puntuación en PlayerPrefs
        PlayerPrefs.SetInt("PlayerScore", count);

        // Cambiar al menú principal
        SceneManager.LoadScene(0); // Carga el menú (escena con índice 0)
    }
}