using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuOpciones : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelOpciones;

    private GameObject menuAnterior;

    [Header("Controles de UI")]
    public Slider sliderSonido;
    public Slider sliderMusica;
    public Slider sliderSensibilidad;
    public Toggle togglePantallaCompleta;
    public TMP_Dropdown dropdownCalidad;

    void Start()
    {
        RefrescarUI();
    }

    // La UI siempre muestra lo guardado, sin disparar los eventos
    private void RefrescarUI()
    {
        if (sliderSonido != null)
            sliderSonido.SetValueWithoutNotify(ConfiguracionesGlobal.VolumenSonido);

        if (sliderMusica != null)
            sliderMusica.SetValueWithoutNotify(ConfiguracionesGlobal.VolumenMusica);

        if (sliderSensibilidad != null)
            sliderSensibilidad.SetValueWithoutNotify(ConfiguracionesGlobal.Sensibilidad);

        if (togglePantallaCompleta != null)
            togglePantallaCompleta.SetIsOnWithoutNotify(ConfiguracionesGlobal.PantallaCompleta);

        if (dropdownCalidad != null)
            dropdownCalidad.SetValueWithoutNotify(ConfiguracionesGlobal.Calidad);
    }

    public void abrirOpciones(GameObject menuQueAbre)
    {
        menuAnterior = menuQueAbre;

        menuQueAbre.SetActive(false);
        panelOpciones.SetActive(true);
        RefrescarUI();
    }

    public void cerrarOpciones()
    {
        PlayerPrefs.Save();
        panelOpciones.SetActive(false);

        if (menuAnterior != null)
        {
            menuAnterior.SetActive(true);
        }
    }

    public void volumenSonido(float valor) { ConfiguracionesGlobal.SetVolumenSonido(valor); }
    public void volumenMusica(float valor) { ConfiguracionesGlobal.SetVolumenMusica(valor); }
    public void sensibilidad(float valor) { ConfiguracionesGlobal.SetSensibilidad(valor); }
    public void pantallaCompleta(bool activa) { ConfiguracionesGlobal.SetPantallaCompleta(activa); }

    // Antes se llamaba "resolucion", pero cambia la CALIDAD gráfica
    public void calidad(int index) { ConfiguracionesGlobal.SetCalidad(index); }
}