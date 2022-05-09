using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mode3E : MonoBehaviour
{

    /* Puntaje */
    public static float UScore = 0;

    public AudioClip Rigth;
    public AudioClip Wrong;
    public AudioSource audio;
    public static bool CarryOn;

    public bool Positivo;
    public bool Negativo;
    public bool Answer;
    //public bool valor;

    private void Start()
    {
        CarryOn = true;
    }

    private void Update()
    {

    }

    public void ValidarRespuesta()
    {
        /*
        if (this.Texto == this.Answer)
        {
            // Debug.Log("Right");
            audio.PlayOneShot(Rigth);
        }
        else
        {
            // Debug.Log("Wrong");
            audio.PlayOneShot(Wrong);
        }*/

        if (Positivo && Answer)
        {
            Debug.Log("Right");
            audio.volume = 0.3F;
            audio.PlayOneShot(Rigth);
            UScore += 10;
        }
        else if (Negativo && Answer)
        {
            Debug.Log("Right");
            audio.volume = 0.3F;
            audio.PlayOneShot(Rigth);
            UScore += 10;
        }
        else
        {
            Debug.Log("Wrong");
            audio.volume = 1;
            audio.PlayOneShot(Wrong);
            UScore += -15;
        }

    //    Answer = false;
        CarryOn = true;

        Debug.Log("**********************************************");
        //Reiciniar();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
