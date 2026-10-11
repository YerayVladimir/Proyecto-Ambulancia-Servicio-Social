using System.Collections;
using UnityEngine;
using TMPro;

public class AyudaControles : MonoBehaviour
{
    public RectTransform panelControles;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI textoAyuda;

    public float velocidadAnimacion = 0.3f;
    public float distanciaDesplazamiento = 350f;

    private Vector2 posicionAbierta;
    private Vector2 posicionCerrada;

    private bool abierto = false;
    private bool animando = false;

    private Coroutine animacionActual;

    void Start()
    {
        posicionAbierta = panelControles.anchoredPosition;

        posicionCerrada = posicionAbierta +
                          new Vector2(distanciaDesplazamiento, 0f);

        panelControles.anchoredPosition = posicionCerrada;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        textoAyuda.text = "H para ver los controles";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !animando)
        {
            abierto = !abierto;

            if (animacionActual != null)
                StopCoroutine(animacionActual);

            animacionActual = StartCoroutine(AnimarPanel(abierto));
        }
    }

    IEnumerator AnimarPanel(bool mostrar)
    {
        animando = true;

        Vector2 posicionInicial = panelControles.anchoredPosition;
        Vector2 posicionFinal = mostrar
            ? posicionAbierta
            : posicionCerrada;

        float alphaInicial = canvasGroup.alpha;
        float alphaFinal = mostrar ? 1f : 0f;

        float tiempo = 0f;

        textoAyuda.text = mostrar
            ? "H para ocultar los controles"
            : "H para ver los controles";

        while (tiempo < velocidadAnimacion)
        {
            tiempo += Time.unscaledDeltaTime;

            float progreso = Mathf.Clamp01(tiempo / velocidadAnimacion);
            float suave = progreso * progreso * (3f - 2f * progreso);

            panelControles.anchoredPosition =
                Vector2.Lerp(posicionInicial, posicionFinal, suave);

            canvasGroup.alpha =
                Mathf.Lerp(alphaInicial, alphaFinal, suave);

            yield return null;
        }

        panelControles.anchoredPosition = posicionFinal;
        canvasGroup.alpha = alphaFinal;

        canvasGroup.interactable = mostrar;
        canvasGroup.blocksRaycasts = mostrar;

        animando = false;
        animacionActual = null;
    }
}