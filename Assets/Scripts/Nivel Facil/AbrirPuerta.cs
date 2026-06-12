using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
    public Animator animador;
    public float distanciaActivacion = 3f;
    public Transform jugador;
    private bool yaAbierta = false;

    void Update()
    {
        if (yaAbierta) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaActivacion)
        {
            animador.SetBool("abierta", true);
            yaAbierta = true;
        }
    }
}
