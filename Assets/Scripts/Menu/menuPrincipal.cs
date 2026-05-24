using UnityEngine;

public class menuPrincipal : MonoBehaviour
{
    public GameObject menuNiveles;

    public void jugar()
    {
        menuNiveles.SetActive(true);
    }

    public void salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}