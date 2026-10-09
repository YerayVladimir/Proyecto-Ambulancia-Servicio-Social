using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{
    public GameObject panelPausa, panelOpciones, puntero, canvaPanelInfo;
    public menuOpciones opciones; // Arrastra aquí el objeto que tiene el script menuOpciones
    public static bool estaPausado;

    void Start()
    {
        Time.timeScale = 1;
        estaPausado = false;
        panelPausa.SetActive(false);
        panelOpciones.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        // Si las opciones están abiertas, Escape solo las cierra
        // y regresa al menú de pausa (cerrarOpciones también guarda los PlayerPrefs)
        if (panelOpciones.activeSelf)
        {
            opciones.cerrarOpciones();
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

    public void pausa()
    {
        panelPausa.SetActive(true);
        estaPausado = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (inspectorObjetos.inspecting == true)
        {
            canvaPanelInfo.SetActive(false);
        }
    }

    public void continuar()
    {
        panelPausa.SetActive(false);
        estaPausado = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (inspectorObjetos.inspecting == true)
        {
            canvaPanelInfo.SetActive(true);
        }
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
        Time.timeScale = 1f; // Si se queda en 0, el menú principal arranca congelado
        estaPausado = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("menuPrincipal");
    }
}