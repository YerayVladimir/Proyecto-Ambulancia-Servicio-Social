using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PanelRetroalimentacion : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelRetroalimentacion;

    [Header("Textos")]
    public TextMeshProUGUI textoNumeroErrores;
    public TextMeshProUGUI textoTip;

    [Header("Escena del menú principal")]
    public string nombreEscenaMenu = "menuPrincipal";

    public void MostrarRetroalimentacion(bool gano, int numeroErrores)
    {
        panelRetroalimentacion.SetActive(true);

        // Aquí aparecerá el número de errores
        // y la retroalimentación correspondiente
        if (gano)
        {
            textoNumeroErrores.text =
                "NUMERO DE ERRORES: " + numeroErrores +
                "\n\n¡Buen trabajo! Completaste el servicio correctamente.";
        }
        else
        {
            textoNumeroErrores.text =
                "NUMERO DE ERRORES: " + numeroErrores +
                "\n\nEl tiempo se agotó antes de completar el servicio.";
        }

        // Consejos
        if (numeroErrores == 0)
        {
            textoTip.text =
                "TIP:\nExcelente trabajo. No cometiste errores.";
        }
        else if (numeroErrores <= 3)
        {
            textoTip.text =
                "TIP:\nProcura revisar la zona correcta antes de colocar cada objeto.";
        }
        else
        {
            textoTip.text =
                "TIP:\nObserva las indicaciones y verifica la zona antes de soltar el objeto.";
        }
        Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(nombreEscenaMenu);
    }

    public void SalirDelJuego()
    {
        Time.timeScale = 1f;

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}