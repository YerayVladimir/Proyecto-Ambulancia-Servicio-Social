using System.Collections.Generic;
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

    [Header("Título (usa UNA de las dos opciones)")]
    [Tooltip("Opción A: el título es un TextMeshPro")]
    public TextMeshProUGUI textoTitulo;

    [Tooltip("Opción B: el título es una imagen. Imagen que dice TIEMPO AGOTADO")]
    public GameObject imagenTiempoAgotado;

    [Tooltip("Opción B: Imagen que dice SERVICIO COMPLETADO")]
    public GameObject imagenServicioCompletado;

    [Header("Escena del menú principal")]
    public string nombreEscenaMenu = "menuPrincipal";

    // ---------- Listas de consejos ----------

    // 0 errores
    private readonly string[] tipsSinErrores =
    {
        "Excelente trabajo. No cometiste errores.",
        "Inicia con los objetos más grandes, te puede ayudar a memorizar y recordar los objetos pequeños a su alrededor.",
        "Memoriza zonas específicas para tener mayor control de los objetos."
    };

    // 1 a 3 errores
    private readonly string[] tipsPocosErrores =
    {
        "Procura revisar la zona correcta antes de colocar cada objeto.",
        "Memoriza zonas específicas para tener mayor control de los objetos.",
        "Intenta iniciar de izquierda a derecha para tener un mayor orden."
    };

    // 4 o más errores
    private readonly string[] tipsMuchosErrores =
    {
        "Observa las indicaciones y verifica la zona antes de soltar el objeto.",
        "Intenta iniciar de izquierda a derecha para tener un mayor orden.",
        "Inicia con los objetos más grandes, te puede ayudar a memorizar y recordar los objetos pequeños a su alrededor.",
        "Memoriza zonas específicas para tener mayor control de los objetos."
    };

    // Solo cuando se acaba el tiempo
    private readonly string tipTiempo =
        "Intenta mirar más tu temporizador, recuerda que estás en una emergencia.";

    public void MostrarRetroalimentacion(bool gano, int numeroErrores)
    {
        panelRetroalimentacion.SetActive(true);

        // ---------- Título ----------
        if (textoTitulo != null)
        {
            textoTitulo.text = gano ? "SERVICIO COMPLETADO" : "TIEMPO AGOTADO";
        }

        if (imagenTiempoAgotado != null)
        {
            imagenTiempoAgotado.SetActive(!gano);
        }

        if (imagenServicioCompletado != null)
        {
            imagenServicioCompletado.SetActive(gano);
        }

        // ---------- Número de errores y mensaje ----------
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

        // ---------- Consejo aleatorio ----------
        textoTip.text = "TIP:\n" + ObtenerTipAleatorio(gano, numeroErrores);

        // Mostrar el cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pausar el juego
        Time.timeScale = 0f;
    }

    private string ObtenerTipAleatorio(bool gano, int numeroErrores)
    {
        List<string> opciones = new List<string>();

        if (numeroErrores == 0)
        {
            opciones.AddRange(tipsSinErrores);
        }
        else if (numeroErrores <= 3)
        {
            opciones.AddRange(tipsPocosErrores);
        }
        else
        {
            opciones.AddRange(tipsMuchosErrores);
        }

        // Si perdió por tiempo, también puede salir el consejo del temporizador
        if (!gano)
        {
            opciones.Add(tipTiempo);
        }

        return opciones[Random.Range(0, opciones.Count)];
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;

        // Reinicia también el estado de pausa
        menuPausa.estaPausado = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;

        // Evita que el estado de pausa se quede activado
        menuPausa.estaPausado = false;

        // Libera el cursor para poder usar el menú
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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