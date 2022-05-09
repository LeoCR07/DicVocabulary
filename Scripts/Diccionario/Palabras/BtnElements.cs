using System;
using UnityEngine;
using UnityEngine.UI;


public class BtnElements : MonoBehaviour
{
    /* Globales */
    public SceneIndex _Index;
    public Button __Btn;

    /* Componentes */
    private AudioSource audio;
    private Button Btn;


    /* Variables */
    public string ID;


    void Start()
    {
        Btn = GetComponentInChildren <Button>();
        audio = GetComponent<AudioSource>();
      //  Btn.onClick.AddListener(ValidarElement);
        __Btn.onClick.AddListener(ValidarElement);
    }

    public void ValidarElement()
    {
       // audio.PlayOneShot(audio.clip);
       // Debug.Log("El ID: " + ID);

         int ID1 = Int32.Parse(_Index.Categoria);
        int Idioma = _Index.GetNuevo();
        AudioClip clip = audio.clip;

       if (ID1 == 0)
       {
          clip = Resources.Load<AudioClip>("Sonido 1/Vegetales/" + Idioma + "/" + ID);
       }
        else if (ID1 == 1)
       {
            clip = Resources.Load<AudioClip>("Sonido 1/Animales/" + Idioma + "/" + ID);
        }

        else if (ID1 == 2)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Frutas/" + Idioma + "/" + ID);
        }
        else if (ID1 == 3)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Trabajos/" + Idioma + "/" + ID);
        }
        else if (ID1 == 4)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Ropa/" + Idioma + "/" + ID);
        }
        else if (ID1 == 5)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Casa/" + Idioma + "/" + ID);

        }
        else if (ID1 == 6)
        {

            clip = Resources.Load<AudioClip>("Sonido 1/Transportes/" + Idioma + "/"  + ID);
        }
        else if ((ID1 == 7))
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Familia/" + Idioma + "/" + ID);
        }
        else if ((ID1 == 8))
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Dias/" + Idioma + "/" + ID);
        }
        else if (ID1 == 9)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Meses/" + Idioma + "/" + ID);
        }
        else if (ID1 == 10)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Cuerpo/" + Idioma + "/" + ID);
        }
        else if (ID1 == 11)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Paises/" + Idioma + "/" + ID);
        }
        else if (ID1 == 12)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Bebidas/" + Idioma + "/" + ID);
        }
        else if (ID1 == 13)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Comidas/" + Idioma + "/" + ID);
        }
        else if (ID1 == 14)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Nacionalidades/" + Idioma + "/" + ID);
        }
        else if (ID1 == 15)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Tecnologia/" + Idioma + "/" + ID);
        }
        else if (ID1 == 16)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Numeros/" + Idioma + "/" + ID);
        }
        else if (ID1 == 17)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Deportes/" + Idioma + "/" + ID);
        }
        else if (ID1 == 18)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Color/" + Idioma + "/" + ID);
        }
        else if (ID1 == 19)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Formas/" + Idioma + "/" + ID);
        }
        else if (ID1 == 20)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Herramientas/" + Idioma + "/" + ID);
        }
        else if (ID1 == 21)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Edificios/" + Idioma + "/" + ID);
        }
        else if (ID1 == 22)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Clima/" + Idioma + "/" + ID);
        }
        else if (ID1 == 23)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Naturaleza/" + Idioma + "/" + ID);
        }
        else if (ID1 == 24)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Espacio/" + Idioma + "/" + ID);
        }
        else if (ID1 == 25)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Abecedario/" + Idioma + "/" + ID);
        }
        else if (ID1 == 26)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Apariencias/" + Idioma + "/" + ID);
        }
        else if (ID1 == 27)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Personalidad/" + Idioma + "/" + ID);
        }
        else if (ID1 == 28)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Expresiones/" + Idioma + "/" + ID);
        }
        else if (ID1 == 29)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Cosas/" + Idioma + "/" + ID);
        }
        else if (ID1 == 30)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Verbos/" + Idioma + "/" + ID);
        }
        else if (ID1 == 31)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Viajes/" + Idioma + "/" + ID);
        }
        else if (ID1 == 32)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/SYD/" + Idioma + "/" + ID);
        }
        else if (ID1 == 33)
        {
            clip = Resources.Load<AudioClip>("Sonido 1/Preguntas/" + Idioma + "/" + ID);
        }


            audio.PlayOneShot(clip);
    }
}
