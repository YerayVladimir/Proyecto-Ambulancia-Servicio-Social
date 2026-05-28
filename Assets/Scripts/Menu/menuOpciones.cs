using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class menuOpciones : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelOpciones;

    private GameObject menuAnterior;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixerSonido;
    [SerializeField] private AudioMixer audioMixerMusica;

    public Slider sliderSonido;
    public Slider sliderMusica;

    public Slider sliderSensibilidad;
    /*
    // LO DEJÉ IGUAL COMO LO TENÍAS
    void Start()
    {
        float sensibilidad = PlayerPrefs.GetFloat("sensibilidad", 2f);

        sliderSensibilidad.value = sensibilidad;
    }
    */
    void Start()
    {
        float sonidoGuardado = PlayerPrefs.GetFloat("volumenSonido", 0f);

        sliderSonido.value = sonidoGuardado;
        audioMixerSonido.SetFloat("VolumenSonido", sonidoGuardado);

        float musicaGuardada = PlayerPrefs.GetFloat("volumenMusica", 0f);

        sliderMusica.value = musicaGuardada;
        audioMixerMusica.SetFloat("VolumenMusica", musicaGuardada);
    }
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
        PlayerPrefs.SetFloat("volumenSonido", sonido);
    }
    public void volumenMusica(float musica)
    {
        audioMixerMusica.SetFloat("VolumenMusica", musica);
        PlayerPrefs.SetFloat("volumenMusica", musica);
    }
    public void resolucion(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    public void sensibilidad(float valor)
    {
        PlayerPrefs.SetFloat("sensibilidad", valor);
    }
}