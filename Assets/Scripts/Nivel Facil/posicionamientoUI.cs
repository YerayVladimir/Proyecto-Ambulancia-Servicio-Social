using System.Collections;
using TMPro;
using UnityEngine;

public class posicionamientoUI : MonoBehaviour
{
    public TextMeshProUGUI textoProgreso;
    public GameObject objetoTextoError;

    public int totalObjetos = 48;

    private int objetosColocados = 0;

    public float tiempoRestante = 60.0f;
    public TextMeshProUGUI textoTemporizador;
    public PanelRetroalimentacion panelRetroalimentacion;
    private int errores = 0;

public int Errores
{
    get { return errores; }
}
    private bool juegoTerminado = false;

    private Coroutine mensajeErrorCoroutine;

    [Header("Configuraci�n del Temporizador")]
    public bool usarTemporizador = true; 

    private void Start()
    {
        UpdateProgressText();
        ActualizarTextoTemporizador(tiempoRestante);

        if (objetoTextoError != null)
        {
            objetoTextoError.SetActive(false);
        }

        if (textoTemporizador != null)
        {
            textoTemporizador.gameObject.SetActive(usarTemporizador);
        }

        StartCoroutine(inicioTemporizador());

    }

    private IEnumerator inicioTemporizador()
    {
        if (!usarTemporizador)
        {
            yield break; // No inicia el temporizador, sale de la corrutina de inmediato
        }

        yield return new WaitForSeconds(5f);

        while (tiempoRestante > 0 && !juegoTerminado)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTextoTemporizador(Mathf.Max(tiempoRestante, 0));
            yield return null;
        }

        if (!juegoTerminado)
        {
            tiempoRestante = 0;
            ActualizarTextoTemporizador(tiempoRestante);
            FinalizarJuego(false); // Se acab� el tiempo (Derrota)
        }
    }

    public void AddCorrectObject()
    {
        if (juegoTerminado) return; // Evita seguir sumando si ya termin�

        objetosColocados++;

        if (objetosColocados > totalObjetos)
        {
            objetosColocados = totalObjetos;
        }

        UpdateProgressText();

        if (objetosColocados >= totalObjetos)
        {
            FinalizarJuego(true); // Termin� porque complet� el objetivo (Victoria)
        }
    }

    public void ShowIncorrectMessage()
    {
        if (mensajeErrorCoroutine != null)
        {
            StopCoroutine(mensajeErrorCoroutine);
        }
        mensajeErrorCoroutine = StartCoroutine(ShowIncorrectMessageRoutine());
    }

    private IEnumerator ShowIncorrectMessageRoutine()
    {
        if (objetoTextoError != null)
        {
            objetoTextoError.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        if (objetoTextoError != null)
        {
            objetoTextoError.SetActive(false);
        }
    }

    private void UpdateProgressText()
    {
        if (textoProgreso != null)
        {
            textoProgreso.text = "Coloca los objetos en su lugar: " + objetosColocados + "/" + totalObjetos;
        }
    }

    // Formatea el tiempo flotante a un formato legible MM:SS en el UI
    private void ActualizarTextoTemporizador(float tiempo)
    {
        if (textoTemporizador != null)
        {
            int minutos = Mathf.FloorToInt(tiempo / 60);
            int segundos = Mathf.FloorToInt(tiempo % 60);
            textoTemporizador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }
    public void RegistrarError()
{
    if (juegoTerminado) return;

    errores++;

    Debug.Log("Errores: " + errores);

    ShowIncorrectMessage();
}
    
    // M�todo centralizado para manejar el fin de la partida
    private void FinalizarJuego(bool gano)
    {
       juegoTerminado = true;

    if (gano)
    {
        Debug.Log("¡Ganaste! Todos los objetos colocados a tiempo.");
    }
    else
    {
        Debug.Log("¡Perdiste! Se agotó el tiempo.");
    }

    if (panelRetroalimentacion != null)
    {
       panelRetroalimentacion.MostrarRetroalimentacion(gano, errores);
    }
    }
}