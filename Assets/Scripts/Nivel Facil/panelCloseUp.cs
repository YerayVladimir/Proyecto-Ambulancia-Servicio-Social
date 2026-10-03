using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelCloseUp : MonoBehaviour
{
    public Camera camaraPrincipal, camaraCabina;
    public GameObject mensaje;
    public void EntrarCabina()
    {
        camaraPrincipal.gameObject.SetActive(false);
        camaraCabina.gameObject.SetActive(true);
        mensaje.SetActive(true);
    }

    public void SalirCabina()
    {
        camaraCabina.gameObject.SetActive(false);
        camaraPrincipal.gameObject.SetActive(true);
        mensaje.SetActive(false);
    }
}
