using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeImg8 : MonoBehaviour
{
    public static string ValorImagen;
    public static bool activado;


    private ModeImg8 model;
    private Button Btn;
    public Image Img;
    public Text Texto;
    public string valor;

    public AudioSource audio;
    public AudioClip Sonido;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Btn = GetComponent<Button>();
        Btn.onClick.AddListener(Validar);
        model = GetComponent<ModeImg8>();
        Texto = GetComponentInChildren<Text>();

        Texto.text = valor;

    }

    private void Validar()
    {
        ValorImagen = valor;
        Img.color = Convertidor(new Color(99f,99f,99f,110f));
        activado = true;

        //Sonido
        audio.PlayOneShot(Sonido);

        if (ModeE8.ValorBotones == ModeImg8.ValorImagen)
        {
            ModeImg8.activado = false;
            ModeE8.activado = false;
        }


    }



    private void Update()
    {


        if (ValorImagen != valor)
        {
            Img.color = Convertidor(new Color(255, 255, 255, 255));
        }

      


        //Botones e imagenes
        if ((valor == ModeImg8.ValorImagen) && ModeE8.ValorBotones == valor)
        {
            activado = false;
            Btn.enabled = false;
            Texto.enabled = true;
            Img.color = Convertidor(new Color(255f, 225f, 255f, 89f));
            valor = "111";
            model.enabled = false;

        }




    }

    Color Convertidor(Vector4 v)
    {
        v /= 255;
        Color color = new Color(v.x, v.y, v.z, v.w);
        return color;
    }
}
