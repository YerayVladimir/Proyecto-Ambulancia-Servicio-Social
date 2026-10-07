using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public GameObject panelPausa, panelOpciones, puntero;
    public static bool estaPausado;

    void Start()
    {
        Time.timeScale = 1;
        panelPausa.SetActive(false);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado == false)
            {
                pausa();
            }
            else if (estaPausado)
            {
                continuar();
            }
            if (panelOpciones.activeSelf)
            {
                return;
            }
            /*puntero.SetActive(false);
            // Hace que el cursor sea visible
            Cursor.visible = true;

            // Libera el rat�n para que pueda moverse por toda la pantalla
            Cursor.lockState = CursorLockMode.None;


            if (estaPausado)
            {
                continuar();
            }
            */
        }
    }
    public void pausa()
    {
        panelPausa.SetActive(true);
        estaPausado = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void continuar()
    {
        panelPausa.SetActive(false);
        estaPausado = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void reiniciar()
    {
        Time.timeScale = 1;
        estaPausado = false;
        puntero.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menuPrincipal()
    {
        panelPausa.SetActive(false);
        puntero.SetActive(false);
        Time.timeScale = 0f;
        estaPausado = false;

        SceneManager.LoadScene("menuPrincipal");
    }
}