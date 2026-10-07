using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inspectorObjetos : MonoBehaviour
{
    [Header("Panel de inspecci n")]
    public GameObject inspectionPanel;
    public Image objectImage;
    public TMP_Text objectName;
    public TMP_Text objectDescription;

    [Header("Mensaje de interacci n")]
    public GameObject inspectPrompt;

    [Header("Raycast")]
    public Camera playerCamera;

    [Header("Configuraci n")]
    public float maxDistance = 5f;

    private objetoInspeccion currentObject;
    private bool inspecting = false;

    void Start()
    {
        inspectionPanel.SetActive(false);
        inspectPrompt.SetActive(false);
    }

    void Update()
    {
        DetectObject();

        // Presionar F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentObject != null && !inspecting)
            {
                StartInspection();
            }
            else if (inspecting)
            {
                StopInspection();
            }
        }
    }

    void DetectObject()
    {
        // Si estamos inspeccionando, no cambiamos el objeto
        if (inspecting)
            return;

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            objetoInspeccion inspectable =
                hit.collider.GetComponent<objetoInspeccion>();

            if (inspectable != null)
            {
                currentObject = inspectable;

                inspectPrompt.SetActive(true);

                TMP_Text promptText =
                    inspectPrompt.GetComponentInChildren<TMP_Text>();

                if (promptText != null)
                {
                    promptText.text = "Presiona \"F\" para inspeccionar";
                }

                return;
            }
        }

        currentObject = null;
        inspectPrompt.SetActive(false);
    }

    void StartInspection()
    {
        inspecting = true;

        // Mostrar panel de informaci n
        inspectionPanel.SetActive(true);

        // Mostrar informaci n del objeto
        objectName.text = currentObject.nombreObj;
        objectDescription.text = currentObject.descripcion;
        objectImage.sprite = currentObject.imagenObjeto;

        // Mantener la proporci n de la imagen
        objectImage.preserveAspect = true;

        // Cambiar el mensaje
        inspectPrompt.SetActive(true);

        TMP_Text promptText = inspectPrompt.GetComponentInChildren<TMP_Text>();

        if (promptText != null)
        {
            promptText.text = "Presiona \"F\" para dejar de inspeccionar";
        }
    }

    void StopInspection()
    {
        inspecting = false;

        // Ocultar panel de informaci n
        inspectionPanel.SetActive(false);

        // Comprobar nuevamente el objeto debajo del mouse
        DetectObject();
    }
    public void CloseInspection()
    {
        if (inspecting)
        {
            inspecting = false;
            inspectionPanel.SetActive(false);
        }

        currentObject = null;
        inspectPrompt.SetActive(false);
    }
}