using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement; // Necesario para manejar escenas.

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    public Text questionText;
    public Button[] answerButtons;
    public TextMeshProUGUI countText;

    public PlayerController playerController; // Referencia al controlador del jugador
    public MoverPuerta moverPuerta; // Referencia al script que controla la puerta

    // Agrega los sonidos
    public AudioClip correctAnswerSound;
    public AudioClip wrongAnswerSound;
    private AudioSource audioSource;

    private bool playerInRange = false;
    private List<Question> questions = new List<Question>();
    private Question currentQuestion;

    public class Question
    {
        public string question;
        public string[] answers;
        public int correctAnswer;

        public Question(string q, string[] a, int correct)
        {
            question = q;
            answers = a;
            correctAnswer = correct;
        }
    }

    void Start()
    {
        quizPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        // Copia el archivo de Resources si no existe en la ruta persistente
        string filePath = Path.Combine(Application.persistentDataPath, "questions.txt");
        if (!File.Exists(filePath))
        {
            TextAsset textFile = Resources.Load<TextAsset>("questions");
            if (textFile != null)
            {
                File.WriteAllText(filePath, textFile.text);
                Debug.Log("Archivo questions.txt copiado a la ruta persistente.");
            }
            else
            {
                Debug.LogError("No se pudo encontrar el archivo questions en Resources.");
            }
        }

        LoadQuestionsFromFile();
    }

    void LoadQuestionsFromFile()
    {
        // Usa Application.persistentDataPath para cargar el archivo de texto
        string filePath = Path.Combine(Application.persistentDataPath, "questions.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] parts = line.Split(';');
                    if (parts.Length >= 5)
                    {
                        string questionText = parts[0];
                        string[] answers = { parts[1], parts[2], parts[3] };
                        int correctAnswerIndex = int.Parse(parts[4]);
                        questions.Add(new Question(questionText, answers, correctAnswerIndex));
                    }
                }
            }
        }
        else
        {
            Debug.LogError("No se pudo cargar el archivo de texto en la ruta persistente: " + filePath);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
            Cursor.visible = true; // Hace visible el cursor

            playerInRange = true;
            playerController.isCameraLocked = true; // Bloquea la cámara

            ChooseRandomQuestion();
            questionText.text = currentQuestion.question;
            quizPanel.SetActive(true);

            playerController.EnableMovement(false); // Deshabilita el movimiento mientras responde

            foreach (Button btn in answerButtons)
            {
                btn.onClick.RemoveAllListeners();
            }

            // Asigna las respuestas a los botones
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
                int index = i;
                answerButtons[i].onClick.AddListener(() => AnswerSelected(index == currentQuestion.correctAnswer));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor de nuevo
            Cursor.visible = false; // Oculta el cursor

            quizPanel.SetActive(false); // Desactiva el panel de la pregunta
            playerInRange = false;
            playerController.EnableMovement(true); // Reactiva el movimiento del jugador
        }
    }

    void ChooseRandomQuestion()
    {
        if (questions.Count > 0)
        {
            int randomIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[randomIndex];
        }
    }

    void AnswerSelected(bool isCorrect)
    {
        // Respuesta correcta o incorrecta
        if (isCorrect)
        {
            GameManager.instance.IncreaseCount();
            audioSource.PlayOneShot(correctAnswerSound);
        }
        else
        {
            GameManager.instance.DecreaseCount(2);
            audioSource.PlayOneShot(wrongAnswerSound);
        }

        moverPuerta.AbrirPuerta(); // Abre la puerta
        quizPanel.SetActive(false); // Desactiva el panel de la pregunta

        // Detiene el movimiento del jugador
        playerController.StopMovement();
        playerController.isCameraLocked = false; // Desbloquea la cámara

        // Elimina los listeners de los botones
        foreach (Button btn in answerButtons)
        {
            btn.onClick.RemoveAllListeners();
        }

        // Habilita el movimiento del jugador
        playerController.EnableMovement(true);

        // Ocultar el cursor y bloquearlo después de responder
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor nuevamente
        Cursor.visible = false; // Oculta el cursor

        // Destruye el objeto QuizTrigger después de que se haya reproducido el sonido de la respuesta
        float soundDuration = isCorrect ? correctAnswerSound.length : wrongAnswerSound.length;
        Destroy(this.gameObject, soundDuration); // Destruye el objeto después de que suene la respuesta
    }
}
