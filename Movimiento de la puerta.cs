using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas.

public class MoverPuerta : MonoBehaviour
{
    [SerializeField]
    private GameObject Puerta;
    [SerializeField]
    private float minY = 0.0f; // Altura mínima a la que la puerta puede bajar
    [SerializeField]
    private float velocidad = 2.0f; // Velocidad de movimiento

    // Método público para cerrar la puerta
    public void AbrirPuerta()
    {
        StartCoroutine(MoverPuertaLentamente(minY));
    }

    private IEnumerator MoverPuertaLentamente(float objetivoY)
    {
        while (Puerta.transform.position.y > objetivoY)
        {
            Puerta.transform.position -= new Vector3(0, velocidad * Time.deltaTime, 0);

            if (Puerta.transform.position.y < objetivoY)
            {
                Puerta.transform.position = new Vector3(Puerta.transform.position.x, objetivoY, Puerta.transform.position.z);
                break;
            }

            yield return null;
        }
    }
}
