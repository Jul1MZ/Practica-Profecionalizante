using System.IO;
using UnityEngine;

public class FileEditor : MonoBehaviour
{
    // Ruta dinámica del archivo
    private string filePath;

    void Start()
    {
        // Crear la subcarpeta si no existe
        string folderPath = Path.Combine(Application.persistentDataPath, "Todo Lo Importante");
        Directory.CreateDirectory(folderPath);

        // Ruta completa al archivo
        filePath = Path.Combine(folderPath, "questions.txt");

        // Crear el archivo si no existe
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Texto inicial.\n");
        }

        // Leer el contenido del archivo
        string content = File.ReadAllText(filePath);
        Debug.Log("Contenido actual del archivo:\n" + content);

        // Modificar el archivo agregando texto
        AddTextToFile("Texto agregado desde Unity ejecutable.");
    }

    void AddTextToFile(string text)
    {
        // Agregar texto al final del archivo
        File.AppendAllText(filePath, text + "\n");
        Debug.Log("Texto agregado al archivo.");
    }
}
