using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonSubCat : MonoBehaviour
{
    /* Imputs */
    public Button _btn;
    public SceneIndex _Index;
    public TextAsset _SubCategorias;

    /* Componentes */
    private TextMeshProUGUI name;


    // Start is called before the first frame update
    void Start()
    {
      //  audio = GetComponent<AudioSource>();
        name = GetComponentInChildren<TextMeshProUGUI>();
        _btn.onClick.AddListener(ValidarIdioma);

    }

    void ValidarIdioma()
    {
        //{0}{1} Español,Ingles - Index
        //{2}{3} Español,Ingles - Tabla SubCategorias
        //{1}{2} Español,Ingles - Tabla Categorias

        /*
        if (_ControlIndex.IdiomaNext == 0)
        {
            ValidarID(2);
        }
        else if (_ControlIndex.IdiomaNext == 1)
        {
            ValidarID(3);
        }

        */

        ValidarID((_Index.GetNuevo()+2) );
        SceneManager.LoadScene(2);
    }

    void ValidarID(int i)
    {
        String[] data = Encoding.UTF7.GetString(_SubCategorias.bytes).Split(new char[] { '\n' });

        for (int j = 1; j < data.Length; j++)
        {
            string[] e = data[j].Split(new char[] { ';' });

            if (name.text == e[i])
            {
                _Index.SubCategoria = e[1];
               // Debug.Log("ID:: "+e[1]);
            }
        }
    }

}
