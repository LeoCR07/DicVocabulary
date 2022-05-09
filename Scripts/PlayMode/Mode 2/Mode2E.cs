using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mode2E : MonoBehaviour
{

    /* Puntaje */
    public static float UScore = 0;

    public AudioClip Rigth;
    public AudioClip Wrong;
    public AudioSource audio;
    public static bool CarryOn;

    static bool activado;
    public string Answer;
    private Text __Texto;
    private Button __Btn;
    ColorBlock color;


    private void Start()
    {
        CarryOn = true;
        __Btn = gameObject.GetComponent<Button>();
        __Texto = gameObject.GetComponentInChildren<Text>();
        __Btn.onClick.AddListener(ValidarRespuesta);
    }

    private void Update()
    {
        color = __Btn.colors;

        //Debug.Log("Update: " + __Texto.text);
        if (__Texto.text == Answer)
        {
            color.pressedColor = Color.green; ;
        }
        else
        {
            color.pressedColor = Color.red;
        }
        __Btn.colors = color;
    }

    public void ValidarRespuesta()
    {
        if (__Texto.text == Answer)
        {
            // Debug.Log("Right");
            audio.PlayOneShot(Rigth);
            UScore += 10;

        }
        else
        {
            // Debug.Log("Wrong");
            audio.PlayOneShot(Wrong);
            UScore += -15;
        }
        CarryOn = true;
        Debug.Log("**********************************************");
        //Reiciniar();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
