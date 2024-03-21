using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorOpciones : MonoBehaviour
{
    public Button botonContinuar;
    public List<DatosImagen> imagenes;
    public InputField sexoSeleccionado;
    public InputField opcionSeleccionada;
    public Image panelImage;
    public GameObject panelMensaje;
    public Text mensaje;
    private int currentImageIndex = 0;
    private bool sexoSeSelecciono = false;

    // Start is called before the first frame update
    void Start()
    {
        sexoSeleccionado.interactable = false;
        opcionSeleccionada.interactable = false;
        botonContinuar.interactable = false;
        botonContinuar.onClick.AddListener(Continue);
        panelMensaje.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarEntradaSexo();
        ProcesarEntradaOpcion();
    }
    public void Continue()
    {
        currentImageIndex++;
        if (currentImageIndex < imagenes.Count)
        {
            panelImage.sprite = imagenes[currentImageIndex].imagen;
        }
    }

    public void SeleccionarOpcion(int numeroOpcion)
    {
        if (numeroOpcion >= 1 && numeroOpcion <= imagenes[currentImageIndex].opciones.Count)
        {
            imagenes[currentImageIndex].opcionSeleccionada = imagenes[currentImageIndex].opciones[numeroOpcion - 1];
        }
    }
    public void SeleccionarSexo(string sexo)
    {
        if (sexo == "H" || sexo == "M")
        {
            imagenes[currentImageIndex].opcionSeleccionada = sexo;
        }
    }
    void ProcesarEntradaSexo()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SeleccionarSexo("H");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SeleccionarSexo("M");
        }
    }

    void ProcesarEntradaOpcion()
    {

        if (!sexoSeSelecciono) 
        {
            return;
        }

        for (int i = 1; i <= 4; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                SeleccionarOpcion(i);
                 botonContinuar.interactable = true;
            }
        }
    }
}

