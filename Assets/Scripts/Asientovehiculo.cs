using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Ponlo en cada puerta (piloto y copiloto). Cada puerta lleva su propia copia.
public class AsientoVehiculo : MonoBehaviour
{
    [Header("Jugador")]
    public movimientoCamara jugador; // el script movimientoCamara (esta en la camara)
    public Camera camara;
    public FootstepSound pasos; // opcional: se apaga mientras estas sentado

    [Tooltip("Otros scripts que muevan al jugador (opcional).")]
    public MonoBehaviour[] scriptsMovimientoADesactivar;

    [Header("Posiciones")]
    public Transform puntoAsiento; // objeto vacio donde queda el jugador sentado
    public Transform puntoSalida;  // objeto vacio afuera de la puerta

    [Header("Interaccion")]
    public Key teclaInteraccion = Key.G;
    public float distanciaInteraccion = 3f;
    public LayerMask capasInteraccion = ~0; // recomendado: una capa "Interactuable"

    [Header("Mensaje en pantalla")]
    public GameObject panelMensaje;
    public TextMeshProUGUI textoMensaje;
    public string textoEntrar = "Presiona {0} para entrar";
    public string textoSalir = "Presiona {0} para salir";

    // Asiento que esta ocupando el jugador ahora mismo (null = ninguno)
    private static AsientoVehiculo asientoActual;

    private bool mostrandoMensaje = false;

    void Start()
    {
        OcultarMensaje();
    }

    void Update()
    {
        if (jugador == null || camara == null) return;

        bool sentadoAqui = asientoActual == this;

        // Si esta sentado en la otra puerta, esta puerta no hace nada
        if (asientoActual != null && !sentadoAqui)
        {
            OcultarMensaje();
            return;
        }

        if (MirandoPuerta())
        {
            MostrarMensaje(sentadoAqui ? textoSalir : textoEntrar);

            if (Keyboard.current != null && Keyboard.current[teclaInteraccion].wasPressedThisFrame)
            {
                if (sentadoAqui) Salir();
                else Entrar();
            }
        }
        else
        {
            OcultarMensaje();
        }
    }

    bool MirandoPuerta()
    {
        Ray rayo = new Ray(camara.transform.position, camara.transform.forward);

        if (Physics.Raycast(rayo, out RaycastHit hit, distanciaInteraccion, capasInteraccion, QueryTriggerInteraction.Collide))
        {
            return hit.collider.transform == transform || hit.collider.transform.IsChildOf(transform);
        }
        return false;
    }

    void Entrar()
    {
        if (puntoAsiento == null)
        {
            Debug.LogError("AsientoVehiculo en '" + name + "': falta asignar Punto Asiento en el Inspector.", this);
            return;
        }

        Transform cuerpo = jugador.cuerpoJugador;
        if (cuerpo == null)
        {
            Debug.LogError("movimientoCamara no tiene asignado Cuerpo Jugador.", this);
            return;
        }

        asientoActual = this;
        jugador.sentado = true;

        // El CharacterController hay que apagarlo para poder teletransportar al jugador
        CharacterController cc = cuerpo.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        cuerpo.position = puntoAsiento.position;
        cuerpo.rotation = Quaternion.Euler(0f, puntoAsiento.eulerAngles.y, 0f);

        if (pasos != null) pasos.enabled = false;
        ActivarScriptsMovimiento(false);
    }

    void Salir()
    {
        Transform cuerpo = jugador.cuerpoJugador;
        CharacterController cc = cuerpo.GetComponent<CharacterController>();

        if (puntoSalida != null) cuerpo.position = puntoSalida.position;
        if (cc != null) cc.enabled = true;

        jugador.sentado = false;
        asientoActual = null;

        if (pasos != null) pasos.enabled = true;
        ActivarScriptsMovimiento(true);
    }

    void ActivarScriptsMovimiento(bool activar)
    {
        if (scriptsMovimientoADesactivar == null) return;

        foreach (MonoBehaviour s in scriptsMovimientoADesactivar)
        {
            if (s != null) s.enabled = activar;
        }
    }

    void MostrarMensaje(string plantilla)
    {
        if (textoMensaje != null)
            textoMensaje.text = string.Format(plantilla, teclaInteraccion.ToString().ToUpper());

        if (panelMensaje != null) panelMensaje.SetActive(true);
        mostrandoMensaje = true;
    }

    void OcultarMensaje()
    {
        // Solo oculta si este script era quien lo estaba mostrando,
        // asi las dos puertas no se pelean por el mismo texto
        if (!mostrandoMensaje) return;

        if (panelMensaje != null) panelMensaje.SetActive(false);
        mostrandoMensaje = false;
    }
}