using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoriaManager : MonoBehaviour
{
    /* inputs */
    public GameObject _Prefab;
    public SceneIndex _Index;
    public TextAsset _dbIdiomas;
   // public List<Sprite> _ImgCat = new List<Sprite>();
    public TextMeshProUGUI _PrimerTxt;
    public List<GameObject> _LstCategoria;

    /* Componentes */
    private TextMeshProUGUI CatName;
    public GridLayoutGroup gridLayout;

    private Text SubCatName; // 2°
    private Image CatImg;
    private BtnCategoria CatID;

    /* variables */
    List<string> lstPrincipal = new List<string>();
    List<string> lstSegunda = new List<string>();
    private List<string> lstID = new List<string>();


    void Start()
    {   
        size();
        ValidarIdioma();
    }

    private void size()
    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * 0.018);
        float x = (float)(with * 0.48);
        float y = (float)(high / 4.8);


        //Vector2 valor = gridLayout.cellSize;
        gridLayout.cellSize = new Vector2(x, y);
        gridLayout.spacing = new Vector2(space,space*2);
    }

    private void ValidarIdioma()
    {
        int IIdioma = 1;  //indice de idioma de la tabla categoria
       

        /** Idioma Nuevo **/
        for( int i = 0; i < _Index.CINuevo; i++)
        {
            if (_Index.GetNuevo() == i)
            {
                lstPrincipal = FindList(IIdioma+i);
                _PrimerTxt.text = FindTitulo(IIdioma+i);
            }
        }


        /** Idioma natal **/
        for (int i = 0; i < _Index.CINatal; i++)
        {
            if (_Index.GetNatal() == i)
            {
                lstSegunda = FindList(IIdioma+i);
            }
        }

        //lstID = FindList(0);
        Instaciar2();
        //Instaciar();
    }

    
    private void Instaciar()
    {
        CatName = _Prefab.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        SubCatName = _Prefab.gameObject.GetComponentInChildren<Text>();
        CatID = _Prefab.gameObject.GetComponent<BtnCategoria>();
        CatImg = _Prefab.gameObject.GetComponentInChildren<Image>();
        

        for (int i = 0; i < lstPrincipal.Count; i++)
        {

            CatID.ID = lstID[i];
            CatName.text = lstPrincipal[i];
            SubCatName.text = lstSegunda[i];
           // CatImg.sprite = _ImgCat[i];
            CatID.DesactivarCandado();
            CatImg.color = Color.white;
            /* Contenido Exclusivo del 26 al 33 */
            if (i >= 26)
            {
                if (_Index.GetComprar())
                {

                }
                else
                {
                    CatImg.color = Color.grey;
                    CatID.ActivarCandado();
                }
             
            }

            
            GameObject aux = Instantiate(_Prefab);
            aux.transform.SetParent(GameObject.FindGameObjectWithTag("GameController").transform, false);
            
        }
    }


    private List<string> FindList(int i)
    {

        List<string> lst = new List<string>();
        String[] data = Encoding.UTF7.GetString(_dbIdiomas.bytes).Split(new char[] { '\n' });

        for (int j = 1; j < data.Length; j++)
        {
            //Aqui se obtiene los nombres
            string[] e = data[j].Split(new char[] { ';' });
            lst.Add(e[i]);
        }

        return lst;

    }

    private string FindTitulo(int i1)
    {
        string valor = "";
        String[] data = Encoding.UTF7.GetString(_dbIdiomas.bytes).Split(new char[] { '\n' });
        string[] e = data[0].Split(new char[] { ';' });

        valor = e[i1];
        return valor;
    }

    private void Instaciar2()
    {

        for (int j = 0; j < _LstCategoria.Count; j++)
        {
            CatName = _LstCategoria[j].GetComponentInChildren<TextMeshProUGUI>();
            SubCatName = _LstCategoria[j].GetComponentInChildren<Text>();
            CatID = _LstCategoria[j].GetComponent<BtnCategoria>();
            CatImg = _LstCategoria[j].GetComponentInChildren<Image>();


           // CatID.ID = lstID[j];
            CatName.text = lstPrincipal[j];
            SubCatName.text = lstSegunda[j];
            // CatImg.sprite = _ImgCat[i];
            CatID.DesactivarCandado();
            CatImg.color = Color.white;
            /* Contenido Exclusivo del 26 al 33 */
            if (j >= 26)
            {
                if (_Index.GetComprar())
                {
                    CatImg.color = Color.white;
                }
                else
                {
                    CatImg.color = Color.grey;
                    CatID.ActivarCandado();
                }
            }
        }
    }
}
