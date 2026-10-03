
using UnityEngine;

public class CamaraCabina : MonoBehaviour
{
    public float sensibilidad = 2f;
    private float rotacionY = 0f, rotacionX = 0f;

    private Quaternion rotacionBase;
    public panelCloseUp panelCloseUp;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rotacionBase = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;

        rotacionY += mouseX;
        rotacionX -= mouseY;

        rotacionX = Mathf.Clamp(rotacionX, -20f, 20f);
        rotacionY = Mathf.Clamp(rotacionY, -50f, 50f);
        transform.localRotation = rotacionBase * Quaternion.Euler(rotacionX, rotacionY, 0f);
        if (Input.GetKeyDown(KeyCode.E))
        {
            panelCloseUp.SalirCabina();
            return;
        }
    }
}