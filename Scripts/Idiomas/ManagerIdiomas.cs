using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerIdiomas : MonoBehaviour
{
    /* Componentes */
    public SceneIndex Index;
    public Text __txtNatal;
    public Text __txtNuevo;
    public List<Button> __BtnNatal;
    public List<Button> __BtnAprender;
    public GridLayoutGroup __GridNuevo;
    public GridLayoutGroup __GridNatal;

    /* Variable */
    List<string> lstTexto = new List<string>();

    void Start()
    {
        AprenderTexto();
        PrimeraValidacion(Index.GetNuevo(),__BtnAprender,__txtNuevo);
        NatalTextos();
        PrimeraValidacion(Index.GetNatal(), __BtnNatal,__txtNatal);

        size(__GridNatal);
        size(__GridNuevo);
    }



    private void size(GridLayoutGroup grid)
    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * 0.018);
        float x = (float)(with * 0.18);
        float y = (float)(high / 17);


        //Vector2 valor = gridLayout.cellSize;
        grid.cellSize = new Vector2(x, y);
        grid.spacing = new Vector2(space * 2,0);
    }

    private void PrimeraValidacion(int n, List<Button>lst,Text txt)
    {   
        Outline outline;
       
       for(int i = 0; i < lst.Count; i++)
        {
            outline = lst[i].GetComponent<Outline>();
            if (i == n)
            {
                outline.enabled = true;
                ActualizarTxt(txt, n);
            }
            else
            {
                outline.enabled = false;
            }
        }
    }

    public void ValidacionNatal(int idioma)
    {
        NatalTextos();
        Index.SetNatal(idioma);
        PrimeraValidacion(idioma, __BtnNatal, __txtNatal);
    }

    public void ValidacionNuevo(int idioma)
    {
        AprenderTexto();
        Index.SetNuevo(idioma);
        PrimeraValidacion(idioma, __BtnAprender, __txtNuevo);
    }

    void ActualizarTxt(Text txt,int i)
    {
        txt.text = lstTexto[i];
    }

    private void AprenderTexto()
    {
        lstTexto = new List<string>();
        lstTexto.Add("Idioma a aprender");  //0
        lstTexto.Add("language to learn");  //1
    }

    private void NatalTextos()
    {
        lstTexto = new List<string>();
        lstTexto.Add("Idioma natal");  //Español
        lstTexto.Add("Native language");  //Ingles
        lstTexto.Add("Muttersprache");  //Aleman
        lstTexto.Add("Moedertaal");  //Holandes
        lstTexto.Add("langue maternelle");  //Frances
        lstTexto.Add("lingua nativa");  //Italiano
        lstTexto.Add("língua nativa");  //portugues
    }

}
