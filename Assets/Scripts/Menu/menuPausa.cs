using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public GameObject panelPausa;
    public GameObject panelOpciones;
    public static bool estaPausado;

    void Start()
    {
        panelPausa.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escape detectado");

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

        Time.timeScale = 1;
        estaPausado = false;

        Debug.Log("Continúa la simulación");
    }

    public void reiniciar()
    {
        Time.timeScale = 1;
        estaPausado = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Reiniciando simulación");
    }

    public void menuPrincipal()
    {
        panelPausa.SetActive(false);

        Time.timeScale = 0f;
        estaPausado = false;

        SceneManager.LoadScene("menuPrincipal");

        Debug.Log("Volviste al menú");
    }
}