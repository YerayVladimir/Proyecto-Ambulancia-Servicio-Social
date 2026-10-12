using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controles : MonoBehaviour
{
    public GameObject letreroSalir, letreroEntrar, letreroControles;
    private bool estaAbierto;
    void Start()
    {
        estaAbierto = false;
        letreroControles.SetActive(false);
        letreroSalir.SetActive(false);
        letreroEntrar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(estaAbierto == false)
            {
                letreroControles.SetActive(true);
                letreroSalir.SetActive(true);
                letreroEntrar.SetActive(false);
                estaAbierto = true;
            }
            else
            {
                letreroControles.SetActive(false);
                letreroSalir.SetActive(false);
                letreroEntrar.SetActive(true);
                estaAbierto = false;
            }
            
        }   
    }
}
