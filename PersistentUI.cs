using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersistentUI : MonoBehaviour
{
    private static PersistentUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cambiar escena.
        }
        else
        {
            Destroy(gameObject); // Asegura que haya solo una instancia.
        }
    }
}

