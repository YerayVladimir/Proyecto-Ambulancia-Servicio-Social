using UnityEngine;

public class movimientoCamara : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2.5f;
    public float gravedad = -9.81f;

    [Header("Mouse")]
    public float sensibilidadMouse = 200f;
    public Transform cuerpoJugador; // Objeto que rota en Y y tiene el CharacterController

    private float rotacionX = 0f;
    private CharacterController controlador;
    private Vector3 velocidadVertical;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controlador = cuerpoJugador.GetComponent<CharacterController>();

        if (controlador == null)
        {
            Debug.LogError("El objeto asignado como cuerpoJugador no tiene CharacterController.");
        }
    }

    private void Update()
    {
        moverJugador();
        moverMouse();
    }

    private void moverJugador()
    {
        if (controlador == null) return;

        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        Vector3 movimiento = cuerpoJugador.right * movimientoX + cuerpoJugador.forward * movimientoZ;

        controlador.Move(movimiento * velocidad * Time.deltaTime);

        if (controlador.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;
        }

        velocidadVertical.y += gravedad * Time.deltaTime;
        controlador.Move(velocidadVertical * Time.deltaTime);
    }

    private void moverMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        // Rotación vertical: cámara arriba y abajo
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Rotación horizontal: gira el cuerpo del jugador
        cuerpoJugador.Rotate(Vector3.up * mouseX);
    }
}