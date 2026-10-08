using UnityEngine;

public class controladorSirena : MonoBehaviour
{
    public AudioClip sirenaCorta;
    public AudioClip sirenaMedia;
    public AudioClip sirenaLarga;

    public AudioClip ModoActual { get; private set; }

    private enum ModoSirena { Corto, Medio, Largo }
    private ModoSirena modo = ModoSirena.Corto;

    public botonON botonSirena;

    void Start()
    {
        ModoActual = sirenaCorta;
    }

    void OnMouseDown()
    {
        avanzarModo();
    }

    void avanzarModo()
    {
        switch (modo)
        {
            case ModoSirena.Corto:
                modo = ModoSirena.Medio;
                ModoActual = sirenaMedia;
                break;

            case ModoSirena.Medio:
                modo = ModoSirena.Largo;
                ModoActual = sirenaLarga;
                break;

            case ModoSirena.Largo:
                modo = ModoSirena.Corto;
                ModoActual = sirenaCorta;
                break;
        }

        if (botonSirena != null)
        {
            botonSirena.ActualizarSiEstaEncendida(ModoActual);
        }
    }
}