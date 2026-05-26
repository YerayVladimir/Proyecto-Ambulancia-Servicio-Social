using System.Collections;
using TMPro;
using UnityEngine;

public class PlacementUI : MonoBehaviour
{
    public TextMeshProUGUI progressText;
    public GameObject errorTextObject;

    public int totalObjects = 2;

    private int placedObjects = 0;

    private void Start()
    {
        UpdateProgressText();

        if (errorTextObject != null)
        {
            errorTextObject.SetActive(false);
        }
    }

    public void AddCorrectObject()
    {
        placedObjects++;

        if (placedObjects > totalObjects)
        {
            placedObjects = totalObjects;
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
        if (errorTextObject != null)
        {
            errorTextObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        if (errorTextObject != null)
        {
            errorTextObject.SetActive(false);
        }
    }

    private void UpdateProgressText()
    {
        if (progressText != null)
        {
            progressText.text = "Coloca los objetos en su lugar: " + placedObjects + "/" + totalObjects;
        }
    }
}