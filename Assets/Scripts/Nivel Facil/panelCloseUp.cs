using UnityEngine;

public class panelCloseUp : MonoBehaviour
{
    public Camera camaraPrincipal, camaraCabina;
    public GameObject Salir;
    public void CambiarCamara()
    {
        camaraPrincipal.gameObject.SetActive(false);
        camaraCabina.gameObject.SetActive(true);
        Salir.SetActive(true);
    }
    public void RegresarCamara()
    {
        camaraPrincipal.gameObject.SetActive(true);
        camaraCabina.gameObject.SetActive(false);
        Salir.SetActive(false);
    }
}
