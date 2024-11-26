using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 5f;
    public TextMeshProUGUI countText;
    private bool canMove = true;
    public float sprintSpeed = 8f;
    private bool isSprinting = false;
    public AudioSource audioSource;
    public AudioClip pickupSound;
    public bool isCameraLocked = false;

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; // Detener rotaciones
        movementX = 0;
        movementY = 0;
    }


    void Update()
    {
        if (!isCameraLocked) // Solo mover la c�mara si no est� bloqueada
        {
            // L�gica para rotar la c�mara
        }
    }

    void Start()
    {
        if (countText == null)
        {
            Debug.LogError("El TextMeshProUGUI no est� asignado en el PlayerController.");
            return; // Salimos para evitar que siga ejecut�ndose con errores.
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();

        // Inicializamos las restricciones del Rigidbody
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        SetCountText(); // Llamada a la actualizaci�n del contador.
    }

    void OnMove(InputValue movementValue)
    {
        if (canMove)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    void OnMoveCanceled(InputValue movementValue)
    {
        // Detenemos el movimiento al soltar las teclas
        movementX = 0;
        movementY = 0;
        rb.velocity = Vector3.zero; // Detenemos cualquier movimiento
    }
    void OnSprint(InputValue sprintValue) // Detecta Shift presionado o soltado
    {
        isSprinting = sprintValue.isPressed;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            float currentSpeed = isSprinting ? sprintSpeed : speed;
            Vector3 movement = transform.forward * movementY + transform.right * movementX;
            rb.velocity = movement * speed;
        }
    }

    void SetCountText()
    {
        countText.text = "Puntuación: " + GameManager.instance.count;
    }

    void PlayPickupSound()
    {
        if (pickupSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            GameManager.instance.IncreaseCount(); // Usa el contador global
            SetCountText();
            other.gameObject.SetActive(false);
            PlayPickupSound();
        }
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;

        if (!enable)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void IncreaseCount()
    {
        GameManager.instance.IncreaseCount(); // Usamos la funci�n del GameManager
        SetCountText();
    }

    public void DecreaseCount(int value)
    {
        GameManager.instance.DecreaseCount(value); // Disminuimos con GameManager
        SetCountText();
    }
}
