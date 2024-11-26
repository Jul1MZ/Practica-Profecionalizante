using TMPro;
using UnityEngine;

public class MenuScoreDisplay : MonoBehaviour
{
    public TMP_Text finalScoreText;

    void Start()
    {
        // Recuperar y mostrar el nombre y puntuación del jugador
        string finalScore = PlayerPrefs.GetString("FinalScore", "Nadie: 0");
        finalScoreText.text = finalScore;
    }
}
