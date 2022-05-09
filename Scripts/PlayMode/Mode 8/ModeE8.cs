using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeE8 : MonoBehaviour
{
    public SceneIndex __Index;

    public static string ValorBotones;
    public static bool activado;

    public AudioSource __ASource;
    public AudioClip __RigthClip;

    private Button Btn;
    private Image Img;

    public string palabra;
    public string valor;

    // Start is called before the first frame update
    void Start()
    {

        ValorBotones = "000";
        Btn = GetComponent<Button>();
        Btn.onClick.AddListener(Validar);
        Img = GetComponent<Image>();
    }

    private void Validar()
    {


        ValorBotones = valor;
        activado = true;

    }

    // Update is called once per frame
    void Update()
    {
        ColorBlock cblock = Btn.colors;


        //Solo botones
        if (ValorBotones == valor)
        {
            cblock.normalColor = Convertidor(new Color(82, 219, 108, 255));
            cblock.selectedColor = Convertidor(new Color(82, 219, 108, 255));
        }
        else
        {
           cblock.normalColor = Convertidor(new Color(255, 255, 255, 255));
            cblock.selectedColor = Convertidor(new Color(255, 255, 255, 255));
        }



        // Botones e imagenes
        if ((valor == ModeImg8.ValorImagen) && ModeE8.ValorBotones == valor)
        {

            Mode8Manger.Contador++;
            __ASource.PlayOneShot(__RigthClip);
            activado = false;
            Img.color = Convertidor(new Color(164, 164, 164, 255));
            Btn.enabled = false;
            valor = "000";

           
            __Index.ScoreM8 += 4;
            Debug.Log(__Index.ScoreM8 + "#");

        }

        Btn.colors = cblock;

    }

    Color Convertidor(Vector4 v)
    {
        v /= 255;
        Color color = new Color(v.x, v.y, v.z, v.w);
        return color;
    }
}
