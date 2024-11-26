using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas.

public class Rotacion : MonoBehaviour
{
    public float rotationSpeed = 100.0f;  // Velocidad de rotación en el eje Z
    public float speed = 10f;  // Velocidad del movimiento vertical
    public float height = 0.3f; // Altura total del movimiento (maxY - minY)
    public float baseY = 0.55f; // Posición central del movimiento (mitad entre maxY y minY)

    void Update()
    {
        // Rotación en el eje Z
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        // Calcula la nueva posición en Y usando Mathf.Sin para un movimiento más suave
        float newY = baseY + Mathf.Sin(Time.time * speed) * (height / 2);

        // Asigna la nueva posición al objeto
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
