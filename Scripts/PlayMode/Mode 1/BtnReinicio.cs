using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnReinicio : MonoBehaviour
{
    private AudioSource audio;

    public void Reincio()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
        Mode1E.UScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void atras()
    {
        SceneManager.LoadScene(5);
    }

    public void AtrasMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
