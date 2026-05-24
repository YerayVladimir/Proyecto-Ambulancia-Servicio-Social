using UnityEngine;
using UnityEngine.Audio;

public class menuOpciones : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelOpciones;

    private GameObject menuAnterior;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixerSonido;
    [SerializeField] private AudioMixer audioMixerMusica;
    public void abrirOpciones(GameObject menuQueAbre)
    {
        menuAnterior = menuQueAbre;

        menuQueAbre.SetActive(false);
        panelOpciones.SetActive(true);

        Debug.Log("Abriendo opciones desde: " + menuQueAbre.name);
    }
    public void cerrarOpciones()
    {
        panelOpciones.SetActive(false);

        if (menuAnterior != null)
        {
            menuAnterior.SetActive(true);

            Debug.Log("Regresando a: " + menuAnterior.name);
        }
    }
    public void pantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void volumenSonido(float sonido)
    {
        audioMixerSonido.SetFloat("VolumenSonido", sonido);
    }

    public void volumenMusica(float musica)
    {
        audioMixerMusica.SetFloat("VolumenMusica", musica);
    }

    public void resolucion(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}