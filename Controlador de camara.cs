using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float sensitivity = 2500f; // Sensibilidad de rotación
    public float maxYAngle = 80f; // Límite superior e inferior de rotación vertical
    private Vector2 currentRotation;
    public bool isCameraLocked = false; // Variable pública para permitir control externo

    void Update()
    {
        // Evita que la cámara se mueva si está bloqueada
        if (isCameraLocked)
            return;

        // Código de movimiento de la cámara
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        currentRotation += mouseInput * sensitivity * Time.deltaTime;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        // Rotación de la cámara
        transform.localRotation = Quaternion.Euler(currentRotation.y, 0, 0);

        // Rotación del jugador (horizontal)
        player.Rotate(Vector3.up * mouseInput.x * sensitivity * Time.deltaTime);
    }
}
