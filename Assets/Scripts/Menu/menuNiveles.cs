using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelesMenu : MonoBehaviour
{
    public void cambiarNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void cambiarNivel(int numeroNivel)
    {
        SceneManager.LoadScene(numeroNivel);
    }
}

