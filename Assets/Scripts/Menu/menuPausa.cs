using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public GameObject panelPausa;
    public GameObject panelOpciones;
    public GameObject puntero;
    public static bool estaPausado;

    void Start()
    {
        Time.timeScale = 1;
        panelPausa.SetActive(false);
        Debug.Log("menuPausa iniciado");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            puntero.SetActive(false);
            // Hace que el cursor sea visible
            Cursor.visible = true;

            // Libera el ratón para que pueda moverse por toda la pantalla
            Cursor.lockState = CursorLockMode.None;

            if (panelOpciones.activeSelf)
            {
                return;
            }

            if (estaPausado)
            {
                continuar();
            }
            else
            {
                pausa();
            }
        }
    }
    public void pausa()
    {
        panelPausa.SetActive(true);

        Time.timeScale = 0;
        estaPausado = true;

        Debug.Log("Simulación en pausa");
        Debug.Log(panelPausa.name);
    }

    public void continuar()
    {
        panelPausa.SetActive(false);
        puntero.SetActive(true);
        // Hace que el cursor sea visible
        Cursor.visible = false;
        // Libera el ratón para que pueda moverse por toda la pantalla
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        estaPausado = false;

        Debug.Log("Continúa la simulación");
    }

    public void reiniciar()
    {
        Time.timeScale = 1;
        estaPausado = false;
        puntero.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Reiniciando simulación");
    }

    public void menuPrincipal()
    {
        panelPausa.SetActive(false);
        puntero.SetActive(false);
        Time.timeScale = 0f;
        estaPausado = false;

        SceneManager.LoadScene("menuPrincipal");

        Debug.Log("Volviste al menú");
    }
}