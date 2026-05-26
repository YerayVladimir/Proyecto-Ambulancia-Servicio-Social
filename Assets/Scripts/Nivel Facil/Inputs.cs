using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public float velocidad = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal"); // A, D, Flechas izquierda/derecha
        float movimientoZ = Input.GetAxis("Vertical");   // W, S, Flechas arriba/abajo

        Vector3 movimiento = new Vector3(movimientoX, 0, movimientoZ);

        transform.Translate(movimiento.normalized * velocidad * Time.deltaTime);
    }
}
