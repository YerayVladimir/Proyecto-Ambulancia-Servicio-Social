using System.Collections;
using TMPro;
using UnityEngine;

public class posicionamientoUI : MonoBehaviour
{
    public TextMeshProUGUI textoProgreso;
    public GameObject objetoTextoError;

    public int totalObjetos = 20;

    private int objetosColocados = 0;

    private void Start()
    {
        UpdateProgressText();

        if (objetoTextoError != null)
        {
            objetoTextoError.SetActive(false);
        }
    }

    public void AddCorrectObject()
    {
        objetosColocados++;

        if (objetosColocados > totalObjetos)
        {
            objetosColocados = totalObjetos;
        }

        UpdateProgressText();
    }

    public void ShowIncorrectMessage()
    {
        StopAllCoroutines();
        StartCoroutine(ShowIncorrectMessageRoutine());
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
}