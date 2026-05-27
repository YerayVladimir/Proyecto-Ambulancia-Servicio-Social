using UnityEngine;

public class agarrar : MonoBehaviour
{
    public float distaciaAgarre = 3f;
    public float velociadadMovimiento = 15f;
    public Transform puntoDeSujecion;

    public posicionamientoUI interfazPosicionamiento;

    private GameObject objetoAgarrado;
    private Rigidbody rigidbodyAgarrado;

    void Update()
    {
        // Click izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            if (objetoAgarrado == null)
            {
                intentarAgarrar();
            }
            else
            {
                soltarObjeto();
            }
        }
    }
    private void FixedUpdate()
    {
        // Mover objeto
        if (objetoAgarrado != null)
        {
            moverObjeto();
        }
    }

    void intentarAgarrar()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distaciaAgarre))
        {
            if (hit.collider.CompareTag("drag"))
            {
                objetoAgarrado = hit.collider.gameObject;
                rigidbodyAgarrado = objetoAgarrado.GetComponent<Rigidbody>();

                objetoAgarrable objetoAgarrableActual = objetoAgarrado.GetComponent<objetoAgarrable>();

                if (objetoAgarrableActual != null)
                {
                    mostrarSoloZonaCorrecta(objetoAgarrableActual.objetosColocados);
                }

                if (rigidbodyAgarrado == null)
                {
                    objetoAgarrado = null;
                    return;
                }

                rigidbodyAgarrado.useGravity = false;
                rigidbodyAgarrado.isKinematic = true;
                rigidbodyAgarrado.drag = 10;
                rigidbodyAgarrado.freezeRotation = true;
                rigidbodyAgarrado.WakeUp();
            }
        }
    }

    void mostrarSoloZonaCorrecta(string idObjeto)
    {
        ubicacionZona[] zonas = FindObjectsByType<ubicacionZona>(FindObjectsSortMode.None);

        foreach (ubicacionZona zona in zonas)
        {
            if (zona.objetoVisual != null)
            {
                zona.objetoVisual.SetActive(zona.idZona == idObjeto);
            }
        }
    }

    void ocultarTodasLasZonas()
    {
        ubicacionZona[] zonas = FindObjectsByType<ubicacionZona>(FindObjectsSortMode.None);

        foreach (ubicacionZona zona in zonas)
        {
            if (zona.objetoVisual != null)
            {
                zona.objetoVisual.SetActive(false);
            }
        }
    }

    void moverObjeto()
    {
        rigidbodyAgarrado.MovePosition(Vector3.Lerp(objetoAgarrado.transform.position, puntoDeSujecion.position, Time.fixedDeltaTime * velociadadMovimiento));
    }

    void soltarObjeto()
    {
        bool zonaCorrecta = false;

        objetoAgarrable objetoAgarrableActual = objetoAgarrado.GetComponent<objetoAgarrable>();

        Collider[] colliders = Physics.OverlapSphere(objetoAgarrado.transform.position,0.5f);

        foreach (Collider colliderEncontrado in colliders)
        {
            ubicacionZona zona = colliderEncontrado.GetComponent<ubicacionZona>();

            if (zona != null)
            {
                if (zona.idZona == objetoAgarrableActual.objetosColocados)
                {
                    zonaCorrecta = true;

                    objetoAgarrableActual.colocadoCorrectamente = true;

                    objetoAgarrado.transform.position = zona.transform.position;

                    objetoAgarrado.transform.rotation = zona.transform.rotation;

                    if (zona.objetoVisual != null)
                    {
                        zona.objetoVisual.SetActive(false);
                    }

                    if (interfazPosicionamiento != null)
                    {
                        interfazPosicionamiento.AddCorrectObject();
                    }

                    break;
                }
            }
        }

        if (!zonaCorrecta)
        {
            objetoAgarrado.transform.position = objetoAgarrableActual.posicionInicial;

            objetoAgarrado.transform.rotation = objetoAgarrableActual.rotacionInicial;

            if (interfazPosicionamiento != null)
            {
                interfazPosicionamiento.ShowIncorrectMessage();
            }
        }

        rigidbodyAgarrado.useGravity = true;
        rigidbodyAgarrado.isKinematic = false;
        rigidbodyAgarrado.drag = 1;
        rigidbodyAgarrado.freezeRotation = false;

        rigidbodyAgarrado.velocity = Vector3.zero;
        rigidbodyAgarrado.angularVelocity = Vector3.zero;

        ocultarTodasLasZonas();

        objetoAgarrado = null;
        rigidbodyAgarrado = null;
    }
}