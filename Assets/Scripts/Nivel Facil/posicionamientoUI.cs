using System.Collections;
using TMPro;
using UnityEngine;

public class posicionamientoUI : MonoBehaviour
{
    public TextMeshProUGUI textoProgreso;
    public GameObject objetoTextoError;

    public int totalObjetos = 47;

    private int objetosColocados = 0;

    public float tiempoRestante = 60.0f;
    public TextMeshProUGUI textoTemporizador;
    private bool juegoTerminado = false;

    private Coroutine mensajeErrorCoroutine;


    private void Start()
    {
        UpdateProgressText();
        ActualizarTextoTemporizador(tiempoRestante);

        if (objetoTextoError != null)
        {
            objetoTextoError.SetActive(false);
        }

        StartCoroutine(inicioTemporizador());

    }

    private IEnumerator inicioTemporizador()
    {
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
            FinalizarJuego(false); // Se acabó el tiempo (Derrota)
        }
    }

    public void AddCorrectObject()
    {
        if (juegoTerminado) return; // Evita seguir sumando si ya terminó

        objetosColocados++;

        if (objetosColocados > totalObjetos)
        {
            objetosColocados = totalObjetos;
        }

        UpdateProgressText();

        if (objetosColocados >= totalObjetos)
        {
            FinalizarJuego(true); // Terminó porque completó el objetivo (Victoria)
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

    // Método centralizado para manejar el fin de la partida
    private void FinalizarJuego(bool gano)
    {
        juegoTerminado = true;

        if (gano)
        {
            Debug.Log("¡Ganaste! Todos los objetos colocados a tiempo.");
            // Aquí activamos la pantalla de resultados 
        }
        else
        {
            Debug.Log("¡Perdiste! Se agotó el tiempo.");
            // Aquí activariamos la pantalla de los resultados igualmente
        }
    }
}