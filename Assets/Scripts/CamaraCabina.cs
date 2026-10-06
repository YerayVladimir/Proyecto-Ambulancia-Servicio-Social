
using UnityEngine;

public class CamaraCabina : MonoBehaviour
{
    public float sensibilidad = 2f, velocidad = 5f;
    public float minZ, maxZ;
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
        float moveZ = Input.GetAxis("Horizontal");
        rotacionY += mouseX;
        rotacionX -= mouseY;

        rotacionX = Mathf.Clamp(rotacionX, -20f, 20f);
        rotacionY = Mathf.Clamp(rotacionY, -50f, 50f);
        transform.localRotation = rotacionBase * Quaternion.Euler(rotacionX, rotacionY, 0f);
        
        Vector3 posicion = transform.localPosition;
        posicion.z += moveZ * velocidad * Time.deltaTime;
        posicion.z = Mathf.Clamp(posicion.z, minZ, maxZ);
        transform.localPosition = posicion;

        if (Input.GetKeyDown(KeyCode.G))
        {
            panelCloseUp.SalirCabina();
            return;
        }
    }
}