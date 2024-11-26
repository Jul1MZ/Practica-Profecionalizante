using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;


public class ChangeSceneOnTrigger : MonoBehaviour
{
    public string Nivel_2 ; // Nombre de la escena que quieres cargar

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador")) // Aseg�rate de que el jugador tenga este tag
        {
            GameManager.instance.UpdateScoreText();
            SceneManager.LoadScene(Nivel_2);
        }
    }
}

