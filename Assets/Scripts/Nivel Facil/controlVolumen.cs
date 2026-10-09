using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class controlVolumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sliderSonido;
    public Slider sliderMusica;

    private const string paramSonido = "VolumenSonido";
    private const string paramMusica = "VolumenMusica";

    void Start()
    {
        float sonido = PlayerPrefs.GetFloat(paramSonido, 1f);
        float musica = PlayerPrefs.GetFloat(paramMusica, 1f);

        sliderSonido.value = sonido;
        sliderMusica.value = musica;

        aplicar(paramSonido, sonido);
        aplicar(paramMusica, musica);

        sliderSonido.onValueChanged.AddListener(v => cambiar(paramSonido, v));
        sliderMusica.onValueChanged.AddListener(v => cambiar(paramMusica, v));
    }

    void cambiar(string parametro, float valor)
    {
        aplicar(parametro, valor);
        PlayerPrefs.SetFloat(parametro, valor);
    }

    void aplicar(string parametro, float valor)
    {
        float db = Mathf.Log10(Mathf.Clamp(valor, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(parametro, db);
    }
}