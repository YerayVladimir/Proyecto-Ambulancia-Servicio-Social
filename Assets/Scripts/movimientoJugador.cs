using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoJugador : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal"); // A, D, Flechas izquierda/derecha
        float movimientoZ = Input.GetAxis("Vertical");   // W, S, Flechas arriba/abajo

        Vector3 movimiento = new Vector3(movimientoX, 0, movimientoZ);

        transform.Translate(movimiento.normalized * velocidad * Time.deltaTime);
    }
}
