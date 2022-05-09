using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mode7E : MonoBehaviour
{
    /* Imputs */
    public SceneIndex __Index;
    public AudioClip Correcto,Incorrecto,clip;
    public AudioSource audio;
    public static bool Activador,Reinicio,Borrado;
    public static string PalabraCompleta;
    public static int indice;
    private Text __View;

    /* Variables */
    private Button Btn;
    private Text texto;
    string WordTem;

    void Start()
    {
        /* Variables */
        indice = 0;
        WordTem = "";
        Activador = false;
        Reinicio = false;

        /* Texto */
        texto = GetComponentInChildren<Text>();
        __View = GameObject.FindGameObjectWithTag("Player").GetComponent<Text>();

        /* Boton */
        Btn = GetComponent<Button>();
        Btn.onClick.AddListener(delegate { Validar(); });
    }

    private void Validar()
    {
        int c = 0;
        int aux = indice;
        string letra = texto.text;
        WordTem = __View.text;
        WordTem += letra;
        Activador = false;
    
        if (PalabraCompleta[indice] + "" == letra + "")
        {
            // __View.text = WordTem;
            indice++;
            __View.text += letra;
            Btn.interactable = false;
            clip = Correcto;
        }
        else
        {
            
            __View.text = "";
            indice = 0;
            Activador = true;
            clip = Incorrecto;
        }

        if(PalabraCompleta.Trim() == __View.text.Trim())
        {
            Debug.Log("Palabra completa");
            ElimarBotones();
            Reinicio = true;
            Activador = true;
            Borrado = true;
           
        }     
       audio.PlayOneShot(clip);
    }

    private void ElimarBotones()
    {
        //Btn.gameObject.active = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Activador)
        {
            Btn.interactable = true;
        }

        if (Reinicio)
        {
            __Index.ScoreM7++;
            //ElimarBotones();
            Reinicio = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
