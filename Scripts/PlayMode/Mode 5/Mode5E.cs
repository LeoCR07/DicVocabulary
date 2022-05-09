using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mode5E : MonoBehaviour
{
    private AudioSource audio;
    private Button Btn;
    private AudioClip Clip;
    public int ID;
    public string Direccion;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Btn = GetComponent<Button>();
        Btn.onClick.AddListener(delegate { Sonar(); });
    }

    private void Sonar()
    {
        this.Clip = Resources.Load<AudioClip>(Direccion + ID);
        audio.PlayOneShot(this.Clip);
        Mode5Manager.Answer = ID;
        Debug.Log(Mode5Manager.Answer);
    }
}
