using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnCategoria : MonoBehaviour
{
    /* Imput */
    public GameObject __ImgCandado;
    public SceneIndex _Index;
    public TextAsset _dbSubCat;

    /* Propiedad */
    public string ID;

    /* Componentes */
    private Button Btn;


    // Start is called before the first frame update
    void Start()
    {
        Btn = GetComponent<Button>();
        Btn.onClick.AddListener(Validar);
    }

    private void Validar()
    {
        _Index.Categoria = ID;
        Debug.Log("El id: " + ID);

        int id = Int32.Parse(ID);

        if (ValidarID())
        {
            //Cargar SubCategoria
            //Debug.Log("Yes");
             // SceneManager.LoadScene(2); //Original
            SceneManager.LoadScene(1);
        }
        else
        {
            //Cargar Elementos
            //Debug.Log("No");
            _Index.SubCategoria = "0";
            //SceneManager.LoadScene(3);  //Original
        


            if(id >= 26)
            {
                if (_Index.GetComprar())
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    SceneManager.LoadScene(4);
                }
            }
            else
            {
                SceneManager.LoadScene(2);
            }


        }


      
    }

    private bool ValidarID()
    {
        bool aux = true;

        List<string> lst = new List<string>();
        String[] data = Encoding.UTF7.GetString(_dbSubCat.bytes).Split(new char[] { '\n' });

        for (int j = 1; j < data.Length; j++)
        {
            string[] e = data[j].Split(new char[] { ';' });

            if (ID == e[0])
            {
                if (e[1] == "0")
                {
                    aux = false;
                }
            }
        }

        return aux;

    }

    public void ActivarCandado()
    {
        __ImgCandado.gameObject.SetActive(true);
    }
    public void DesactivarCandado()
    {
        __ImgCandado.gameObject.SetActive(false);
    }
}
