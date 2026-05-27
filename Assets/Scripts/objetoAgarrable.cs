using UnityEngine;

public class objetoAgarrable : MonoBehaviour
{
    public string objetosColocados;
    public bool colocadoCorrectamente = false;

    [HideInInspector]
    public Vector3 posicionInicial;

    [HideInInspector]
    public Quaternion rotacionInicial;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }
}