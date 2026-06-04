using UnityEngine;

public class movimientoCamara : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2.5f;

    [Header("Mouse")]
    public float sensibilidadMouse = 200f;
    public Transform cuerpoJugador; // Objeto que rota en Y

    private float rotacionX = 0f;

    private void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        moverJugador();
        moverMouse();
    }

    private void moverJugador()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        Vector3 movimiento = cuerpoJugador.right * movimientoX + cuerpoJugador.forward * movimientoZ;

        cuerpoJugador.Translate(movimiento * velocidad * Time.deltaTime, Space.World);
    }

    private void moverMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        // Rotación vertical: mirar arriba y abajo
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Rotación horizontal: girar el cuerpo del jugador
        cuerpoJugador.Rotate(Vector3.up * mouseX);
    }
}