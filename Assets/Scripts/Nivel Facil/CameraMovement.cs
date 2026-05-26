using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 200f;

    [Header("Mouse")]
    public float sensibilidadMouse = 200f;
    public Transform playerBody;   // objeto que rota en Y

    float xRotation = 0f;

    void Start()
    {
        // Bloquea el cursor al centro
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovimientoJugador();
        MovimientoMouse();
    }

    void MovimientoJugador()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        Vector3 movimiento = playerBody.right * movimientoX +
                             playerBody.forward * movimientoZ;

        playerBody.Translate(movimiento * velocidad * Time.deltaTime, Space.World);
    }

    void MovimientoMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotación vertical (mirar arriba/abajo)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (girar cuerpo)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}