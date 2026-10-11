using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ConfiguracionesGlobal : MonoBehaviour
{
    // El mixer debe estar en Assets/Resources
    private const string NOMBRE_MIXER = "MixerPrincipal";
    private const string PARAM_SONIDO = "VolumenSonido";
    private const string PARAM_MUSICA = "VolumenMusica";

    // Volumen en escala lineal (0.0001 a 1)
    public const float DEF_VOLUMEN = 0.5f;
    public const float DEF_SENSIBILIDAD = 425f;

    private static AudioMixer mixer;
    private static float sensibilidad = DEF_SENSIBILIDAD; // en memoria: la c�mara la lee cada frame

    // ---------- Lectura ----------
    public static float VolumenSonido => PlayerPrefs.GetFloat("volumenSonido", DEF_VOLUMEN);
    public static float VolumenMusica => PlayerPrefs.GetFloat("volumenMusica", DEF_VOLUMEN);
    public static float Sensibilidad => sensibilidad;
    public static int Calidad => PlayerPrefs.GetInt("calidad", QualitySettings.GetQualityLevel());
    public static bool PantallaCompleta => PlayerPrefs.GetInt("pantallaCompleta", Screen.fullScreen ? 1 : 0) == 1;

    // ---------- Escritura (guarda y aplica) ----------
    public static void SetVolumenSonido(float valor)
    {
        PlayerPrefs.SetFloat("volumenSonido", valor);
        AplicarVolumen(PARAM_SONIDO, valor);
    }

    public static void SetVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("volumenMusica", valor);
        AplicarVolumen(PARAM_MUSICA, valor);
    }

    public static void SetSensibilidad(float valor)
    {
        sensibilidad = valor;
        PlayerPrefs.SetFloat("sensibilidad", valor);
    }

    public static void SetCalidad(int indice)
    {
        PlayerPrefs.SetInt("calidad", indice);
        QualitySettings.SetQualityLevel(indice);
    }

    public static void SetPantallaCompleta(bool activa)
    {
        PlayerPrefs.SetInt("pantallaCompleta", activa ? 1 : 0);
        Screen.fullScreen = activa;
    }

    // ---------- Aplicaci�n ----------
    private static void AplicarVolumen(string parametro, float lineal)
    {
        if (mixer == null) return;

        float db = Mathf.Log10(Mathf.Clamp(lineal, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(parametro, db);
    }

    private static void AplicarTodo()
    {
        AplicarVolumen(PARAM_SONIDO, VolumenSonido);
        AplicarVolumen(PARAM_MUSICA, VolumenMusica);
        QualitySettings.SetQualityLevel(Calidad);
        Screen.fullScreen = PantallaCompleta;
    }

    // ---------- Arranque ----------
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Crear()
    {
        mixer = Resources.Load<AudioMixer>(NOMBRE_MIXER);


        sensibilidad = PlayerPrefs.GetFloat("sensibilidad", DEF_SENSIBILIDAD);

        GameObject go = new GameObject("ConfiguracionesGlobal");
        DontDestroyOnLoad(go);
        go.AddComponent<ConfiguracionesGlobal>();
    }

    private void Awake()
    {
        // Se dispara tambi�n para la primera escena, porque nos suscribimos antes de que cargue
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        StartCoroutine(AplicarTrasUnFrame());
    }

    // Esperar un frame evita que el mixer ignore SetFloat justo al cargar la escena
    private IEnumerator AplicarTrasUnFrame()
    {
        yield return null;
        AplicarTodo();
    }
}