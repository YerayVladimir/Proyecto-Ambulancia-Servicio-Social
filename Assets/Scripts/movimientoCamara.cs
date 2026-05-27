using UnityEngine;

public class movimientoCamara : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 200f;

    [Header("Mouse")]
    public float sensibilidadMouse = 200f;
    public Transform cuerpoJugador;   // objeto que rota en Y

    float rotacionX = 0f;

    void Start()
    {
        // Bloquea el cursor al centro
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        movimientoJugador();
        movimientoMouse();
    }

    void movimientoJugador()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        Vector3 movimiento = cuerpoJugador.right * movimientoX + cuerpoJugador.forward * movimientoZ;

        cuerpoJugador.Translate(movimiento * velocidad * Time.deltaTime, Space.World);
    }

    void movimientoMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        // Rotación vertical (mirar arriba/abajo)
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Rotación horizontal (girar cuerpo)
        cuerpoJugador.Rotate(Vector3.up * mouseX);
    }
}