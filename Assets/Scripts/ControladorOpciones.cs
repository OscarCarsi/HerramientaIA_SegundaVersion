using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ControladorOpciones : MonoBehaviour
{
    public Button botonContinuar;
    public List<DatosImagen> imagenes;
    public InputField sexoSeleccionado;
    public InputField opcionSeleccionada;
    public Image panelImage;
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
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarEntradaSexo();
        ProcesarEntradaOpcion();
    }
    void Continue()
    {
        currentImageIndex++;
        if (imagenes != null && currentImageIndex < imagenes.Count)
        {
            panelImage.sprite = imagenes[currentImageIndex].imagen;
        }
        else
        {
            GuardarRespuesta();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }

    public void SeleccionarSexo(string sexo)
    {
        if (sexo == "H" || sexo == "M")
        {
            imagenes[currentImageIndex].sexoSeleccionado = sexo;
            sexoSeleccionado.text = sexo;
            sexoSeSelecciono = true;
        }
    }

    public void SeleccionarOpcion(int numeroOpcion)
    {
        if (numeroOpcion >= 1 && numeroOpcion <= imagenes[currentImageIndex].opciones.Count)
        {
            imagenes[currentImageIndex].opcionSeleccionada = imagenes[currentImageIndex].opciones[numeroOpcion - 1];
            opcionSeleccionada.text = imagenes[currentImageIndex].opciones[numeroOpcion - 1];
            botonContinuar.interactable = true;
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
    public void GuardarRespuesta()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\respuestas.csv";
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            for (int i = 0; i < imagenes.Count; i++)
            {
                string pregunta = (i + 1).ToString();
                string sexo = imagenes[i].sexoSeleccionado;
                Debug.Log(sexo);
                string emocion = imagenes[i].opcionSeleccionada.ToLower().Normalize(NormalizationForm.FormD);
                Debug.Log(emocion);
                emocion = Regex.Replace(emocion, @"[^a-zA-z0-9\s]", "");

                writer.WriteLine($"{pregunta},{sexo},{emocion}");
            }
        }

    }
}

