using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class ManagerSub : MonoBehaviour
{

    /* Inputs */
    public SceneIndex _Index;
    public TextMeshProUGUI _Titulo;
    //public Text _SubTitulo;
    public GameObject _Prefab;
    public TextAsset _Categorias;
    public TextAsset _SubCategorias;

    /*  Componentes */
    private Image SubImagen;
    private TextMeshProUGUI SubName;
    private Text SubSecondName;
    public GridLayoutGroup gridLayout;
    private AudioSource SubAudio;

    /* Variables */
    List<string> lstSub = new List<string>();
    List<string> lstSecond = new List<string>();
    List<AudioClip> lstAudio = new List<AudioClip>();

    void Start()
    {

        size();
        //Si se agrega contenido modicar PrecargarLayout y SelectCat
        PreCargarLayout();
   
    }

    private void PreCargarLayout()
    {
         
        PrimerIdioma();
        SegundoIdioma();
        
        InstanciarLayout();
    }


    private void PrimerIdioma()
    {
        //{0}{1} Español,Ingles... - Index
        //{2}{3} Español,Ingles... - Tabla SubCategorias
        //{1}{2} Español,Ingles... Tabla Categorias

        for (int i = 0; i < _Index.CINuevo; i++)
        {
            if (_Index.GetNuevo() == i)
            {
                lstSub = FindSubCategorias(2 + i);
                _Titulo.text = FindTitulo(1 + i);
            }
        }
    }

    private void SegundoIdioma()
    {
        //{0}{1} Español,Ingles - Index
        //{2}{3} Español,Ingles - Tabla SubCategorias
        //{1}{2} Español,Ingles - Tabla Categorias

        for (int i = 0; i < _Index.CINatal; i++)
        {
            if (_Index.GetNatal() == i)
            {
                lstSecond = FindSubCategorias(2 + i);
            }
        }
    }

   

    private List<string>FindSubCategorias(int i)
    {
        List<string> lst = new List<string>();

        String[] data = Encoding.UTF7.GetString(_SubCategorias.bytes).Split(new char[] { '\n' });

        for (int j = 1; j < data.Length; j++)
        {
            string[] e = data[j].Split(new char[] { ';' });

            if (_Index.Categoria == e[0])
            {
                lst.Add(e[i]);
            }
        }

        return lst;
    }

    private void size()
    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * 0.018);
        float x = (float)(with * 0.48);
        float y = (float)(high / 4.5);


        //Vector2 valor = gridLayout.cellSize;
        gridLayout.cellSize = new Vector2(x, y);
        gridLayout.spacing = new Vector2(space, space * 2);
    }

    string FindTitulo(int i)
    {
        string valor = "";
        String[] data = Encoding.UTF7.GetString(_Categorias.bytes).Split(new char[] { '\n' });

        for (int j = 1; j < data.Length; j++)
        {
            string[] e = data[j].Split(new char[] { ';' });

            if (_Index.Categoria == e[0])
            {
                valor = e[i];
            }
        }

        return valor;
    }



    private void InstanciarLayout()
    {
        SubName = _Prefab.GetComponentInChildren<TextMeshProUGUI>();
        SubImagen = _Prefab.GetComponentInChildren<Image>();
        SubSecondName = _Prefab.GetComponentInChildren<Text>();
        SubAudio = _Prefab.GetComponent<AudioSource>();

        for (int i = 0; i < lstSub.Count; i++)
        {
            SubName.text = lstSub[i];
            SubSecondName.text = lstSecond[i];
            GameObject aux = Instantiate(_Prefab);
            aux.transform.SetParent(GameObject.FindGameObjectWithTag("GameController").transform, false);
        }

    }

}
