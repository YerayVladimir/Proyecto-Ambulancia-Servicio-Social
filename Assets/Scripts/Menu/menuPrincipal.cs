using UnityEngine;

public class menuPrincipal : MonoBehaviour
{
    public GameObject menuNiveles;

    public void jugar()
    {
        menuNiveles.SetActive(true);
        Debug.Log("Se inicio");
    }

    public void salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
    public void cerrarNiveles()
    {
        menuNiveles.SetActive(false);
        Debug.Log("Se cerro");
    }
}