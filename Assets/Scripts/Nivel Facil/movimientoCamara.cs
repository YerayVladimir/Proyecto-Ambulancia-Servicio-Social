using UnityEngine;

public class movimientoCamara : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2.5f;
    public float gravedad = -9.81f;

    [Header("Mouse")]
    public float sensibilidadMouse = 200f;
    public Transform cuerpoJugador;

    [Header("Agacharse")]
    public float alturaAgachado = 0.7f;
    public float velocidadAgacharse = 8f;

    [HideInInspector] public bool sentado = false;

    private float rotacionX = 0f;
    private CharacterController controlador;
    private Vector3 velocidadVertical;

    private Vector3 posicionCamaraNormal;
    private Vector3 posicionCamaraAgachado;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        controlador = cuerpoJugador.GetComponent<CharacterController>();

        

        // Guardamos la posici�n original de la c�mara
        posicionCamaraNormal = transform.localPosition;

        // Creamos la posici�n agachada
        posicionCamaraAgachado = posicionCamaraNormal;
        posicionCamaraAgachado.y -= alturaAgachado;
    }

    private void Update()
    {
        if (!sentado)
        {
            moverJugador();
        }

        moverMouse();
        agacharse();
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
        sensibilidadMouse = ConfiguracionesGlobal.Sensibilidad;

        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -45f, 90f);

        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        cuerpoJugador.Rotate(Vector3.up * mouseX);
    }

    private void agacharse()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                posicionCamaraAgachado,
                velocidadAgacharse * Time.deltaTime
            );
        }
        else
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                posicionCamaraNormal,
                velocidadAgacharse * Time.deltaTime
            );
        }
    }
}