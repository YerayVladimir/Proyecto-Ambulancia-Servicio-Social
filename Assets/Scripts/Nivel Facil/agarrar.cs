using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agarrar : MonoBehaviour
{
    public float distaciaAgarre = 3f;
    public float velociadadMovimiento = 15f;
    public float tiempoMovimientoZona = 2f;
    public float tiempoRegreso = 2f;

    public Transform puntoDeSujecion;

    public posicionamientoUI interfazPosicionamiento;

    public AudioSource audioSource;
    public AudioClip agarrarSound;
    public AudioClip soltarSound;

    public zonasManger zonasManger;
    public panelCloseUp panelCloseUp;

    private GameObject objetoAgarrado;
    private Rigidbody rigidbodyAgarrado;
    private Collider[] collidersObjeto;

    private readonly Dictionary<GameObject, Coroutine> contadoresRegreso =
        new Dictionary<GameObject, Coroutine>();

    void Update()
    {
        if (menuPausa.estaPausado)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (objetoAgarrado == null)
                intentarAgarrar();
            else
                soltarObjeto();
        }
    }

    void FixedUpdate()
    {
        if (objetoAgarrado != null && rigidbodyAgarrado != null)
            moverObjeto();
    }

    void intentarAgarrar()
    {
        if (Camera.main == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, distaciaAgarre))
            return;

        if (hit.collider.CompareTag("panel"))
        {
            if (panelCloseUp != null)
                panelCloseUp.EntrarCabina();

            return;
        }

        if (!hit.collider.CompareTag("drag"))
            return;

        GameObject objeto = hit.collider.gameObject;

        objetoAgarrable datos = objeto.GetComponent<objetoAgarrable>();

        if (datos == null)
            datos = objeto.GetComponentInParent<objetoAgarrable>();

        if (datos != null && datos.colocadoCorrectamente)
            return;

        Rigidbody rb = objeto.GetComponent<Rigidbody>();

        if (rb == null)
            rb = objeto.GetComponentInParent<Rigidbody>();

        if (rb == null)
            return;

        // Cancelar el regreso pendiente.
        if (contadoresRegreso.TryGetValue(objeto, out Coroutine contador))
        {
            if (contador != null)
                StopCoroutine(contador);

            contadoresRegreso.Remove(objeto);
        }

        objetoAgarrado = objeto;
        rigidbodyAgarrado = rb;

        if (datos != null)
            mostrarSoloZonaCorrecta(datos.objetosColocados);

        rb.useGravity = false;
        rb.isKinematic = true;
        rb.drag = 10f;
        rb.freezeRotation = true;

        if (audioSource != null && agarrarSound != null)
            audioSource.PlayOneShot(agarrarSound);

        collidersObjeto = objeto.GetComponentsInChildren<Collider>();

        foreach (Collider c in collidersObjeto)
        {
            if (c != null)
                c.enabled = false;
        }

        congelarOtrosObjetos(true);
    }

    void mostrarSoloZonaCorrecta(string idObjeto)
    {
        ubicacionZona[] zonas =
            FindObjectsByType<ubicacionZona>(FindObjectsSortMode.None);

        bool ayudaPermitida =
            zonasManger == null || zonasManger.ZonaHabilitada(idObjeto);

        foreach (ubicacionZona zona in zonas)
        {
            if (zona.visual != null)
                zona.visual.SetActive(
                    zona.zonaID == idObjeto && ayudaPermitida);
        }
    }

    void ocultarTodasLasZonas()
    {
        ubicacionZona[] zonas =
            FindObjectsByType<ubicacionZona>(FindObjectsSortMode.None);

        foreach (ubicacionZona zona in zonas)
        {
            if (zona.visual != null)
                zona.visual.SetActive(false);
        }
    }

    void moverObjeto()
    {
        if (puntoDeSujecion == null)
            return;

        rigidbodyAgarrado.MovePosition(
            Vector3.Lerp(
                objetoAgarrado.transform.position,
                puntoDeSujecion.position,
                Time.fixedDeltaTime * velociadadMovimiento));
    }

    void soltarObjeto()
    {
        if (objetoAgarrado == null || rigidbodyAgarrado == null)
            return;

        GameObject objeto = objetoAgarrado;
        Rigidbody rb = rigidbodyAgarrado;

        objetoAgarrable datos = objeto.GetComponent<objetoAgarrable>();

        if (datos == null)
            datos = objeto.GetComponentInParent<objetoAgarrable>();

        if (audioSource != null && soltarSound != null)
            audioSource.PlayOneShot(soltarSound);

        if (collidersObjeto != null)
        {
            foreach (Collider c in collidersObjeto)
            {
                if (c != null)
                    c.enabled = true;
            }
        }

        rb.freezeRotation = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.drag = 1f;

        objetoAgarrado = null;
        rigidbodyAgarrado = null;
        collidersObjeto = null;

        congelarOtrosObjetos(false);
        ocultarTodasLasZonas();

        if (datos == null)
            return;

        ubicacionZona zonaDestino = null;

        Collider[] colliders =
            Physics.OverlapSphere(objeto.transform.position, 0.5f);

        foreach (Collider col in colliders)
        {
            ubicacionZona zona = col.GetComponent<ubicacionZona>();

            if (zona == null)
                zona = col.GetComponentInParent<ubicacionZona>();

            if (zona != null && zona.zonaID == datos.objetosColocados)
            {
                zonaDestino = zona;
                break;
            }
        }

        // Si está en su zona correcta, conservarlo ahí.
        if (zonaDestino != null)
        {
            if (!datos.colocadoCorrectamente)
            {
                datos.colocadoCorrectamente = true;

                if (interfazPosicionamiento != null)
                    interfazPosicionamiento.AddCorrectObject();
            }

            objeto.tag = "Untagged";

            if (zonaDestino.visual != null)
                zonaDestino.visual.SetActive(false);

            StartCoroutine(
                MoverAZona(
                    objeto,
                    zonaDestino.transform.position,
                    zonaDestino.transform.rotation));

            return;
        }

        bool enPosicionInicial =
            Vector3.Distance(objeto.transform.position, datos.posicionInicial) < 0.01f &&
            Quaternion.Angle(objeto.transform.rotation, datos.rotacionInicial) < 1f;

        if (enPosicionInicial)
            return;

        if (interfazPosicionamiento != null)
            interfazPosicionamiento.RegistrarError();

        // Evitar dos contadores para el mismo objeto.
        if (contadoresRegreso.TryGetValue(objeto, out Coroutine anterior))
        {
            if (anterior != null)
                StopCoroutine(anterior);

            contadoresRegreso.Remove(objeto);
        }

        Coroutine nuevaCorrutina =
            StartCoroutine(ContadorRegreso(objeto, datos, rb));

        contadoresRegreso[objeto] = nuevaCorrutina;
    }

    IEnumerator ContadorRegreso(
        GameObject objeto,
        objetoAgarrable datos,
        Rigidbody rb)
    {
        float tiempo = 0f;

        while (tiempo < tiempoRegreso)
        {
            if (objeto == null || datos == null)
            {
                contadoresRegreso.Remove(objeto);
                yield break;
            }

            if (objetoAgarrado == objeto || datos.colocadoCorrectamente)
            {
                contadoresRegreso.Remove(objeto);
                yield break;
            }

            tiempo += Time.deltaTime;
            yield return null;
        }

        if (objeto == null || datos == null)
        {
            contadoresRegreso.Remove(objeto);
            yield break;
        }

        if (objetoAgarrado == objeto || datos.colocadoCorrectamente)
        {
            contadoresRegreso.Remove(objeto);
            yield break;
        }

        bool enPosicionInicial =
            Vector3.Distance(objeto.transform.position, datos.posicionInicial) < 0.01f &&
            Quaternion.Angle(objeto.transform.rotation, datos.rotacionInicial) < 1f;

        if (!enPosicionInicial)
        {
            // Detener el movimiento físico antes de cambiar la posición.
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.Sleep();
            }

            objeto.transform.SetPositionAndRotation(
                datos.posicionInicial,
                datos.rotacionInicial);

            Physics.SyncTransforms();

            // El objeto vuelve a tener su física normal.
            if (rb != null)
                rb.WakeUp();
        }

        Collider[] colliders =
            objeto.GetComponentsInChildren<Collider>(true);

        foreach (Collider c in colliders)
        {
            if (c != null)
                c.enabled = true;
        }

        contadoresRegreso.Remove(objeto);
    }

    IEnumerator MoverAZona(
        GameObject objeto,
        Vector3 posicionDestino,
        Quaternion rotacionDestino)
    {
        if (objeto == null)
            yield break;

        Vector3 posicionInicial = objeto.transform.position;
        Quaternion rotacionInicial = objeto.transform.rotation;

        float tiempo = 0f;

        while (tiempo < tiempoMovimientoZona)
        {
            if (objeto == null)
                yield break;

            tiempo += Time.deltaTime;

            float t = tiempoMovimientoZona > 0f
                ? Mathf.Clamp01(tiempo / tiempoMovimientoZona)
                : 1f;

            objeto.transform.position = Vector3.Lerp(
                posicionInicial, posicionDestino, t);

            objeto.transform.rotation = Quaternion.Lerp(
                rotacionInicial, rotacionDestino, t);

            yield return null;
        }

        if (objeto != null)
        {
            objeto.transform.SetPositionAndRotation(
                posicionDestino,
                rotacionDestino);

            Physics.SyncTransforms();
        }
    }

    void congelarOtrosObjetos(bool congelar)
    {
        GameObject[] objetos =
            GameObject.FindGameObjectsWithTag("drag");

        foreach (GameObject obj in objetos)
        {
            if (obj != objetoAgarrado)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.isKinematic = congelar;
            }
        }
    }
}